using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteCKC.Models;
using Microsoft.AspNet.Identity;

namespace WebsiteCKC.Controllers
{
    public class AdminController : Controller
    {
        DatabaseManager dbm = new DatabaseManager();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ControlPanel()
        {
            if (User.Identity.IsAuthenticated)
            {
                List<Team> teams = null;
                Team favTeam = null; ;
                Competition comp = dbm.GetCompetitionByUserID(User.Identity.GetUserId());
                if(comp != null)
                {
                    teams = new List<Team>();
                    teams = dbm.GetTeamsByCompID(comp.ID);
                    if(teams.Count > 0 )
                    {
                        favTeam = dbm.GetFavoriteTeamByUserID(User.Identity.GetUserId());
                    } 
                    else if(teams.Count == 0)
                    {
                        teams = null;
                    }
                }
                
                ViewData["Competition"] = comp;
                ViewData["ConfigTeams"] = null;
                ViewData["ConfigTeams"] = teams;
                ViewData["FavoriteTeam"] = favTeam;
                return View();
            }
            return RedirectToAction("Index", "Home");
        }

        // Config page - Competition
        public ActionResult Competition()
        {
            Competition comp = dbm.GetCompetitionByUserID(User.Identity.GetUserId());
            if (comp != null)
            {
                ViewData["ConfigTeams"] = dbm.GetTeamsByCompID(comp.ID);
                Team favTeam = dbm.GetFavoriteTeamByUserID(User.Identity.GetUserId());
                ViewBag.FavTeamID = favTeam;
            }
            ViewBag.Competition = comp;

            return View();
        }

        [HttpPost]
        public ActionResult Competition(CreateCompetitionView view)
        {
            if(ModelState.IsValid)
            {
                // Request is ok. First check if the records are not larger than 1 and 2 chars.
                if(view.Class.ToString().Length == 1)
                {
                    if ((view.Group.ToString().Length > 0) && (view.Group.ToString().Length <= 2))
                    {
                        dbm.AddCompetition(view.Class, view.Group, User.Identity.GetUserId());
                        return RedirectToAction("Competition");
                    }
                    else
                    {
                        ModelState.AddModelError("Group", "Groep invoer is niet correct. Invoer bestaat uit 1 of twee getallen. Bijv 01 of 1 ( Groep 01 )");
                    }
                }
                else
                {
                    ModelState.AddModelError("Class", "Klasse invoer is niet correct. Invoer bestaat uit 1 getal. Bijv: 1 ( 1e klasse )");
                }

            }

            ModelState.AddModelError("", "Invoer niet correct.");

            return View(view);
        }

        [HttpPost]
        public ActionResult AddTeam(int compID, string clubName, int teamNumber)
        {
            // Add this team to the db
            int teamID = dbm.AddTeam(clubName, teamNumber, compID);
            return Json(teamID);
        }

        [HttpPost]
        public ActionResult AdjustTeam(int teamID, string clubname, int teamNumber)
        {
            return Json("");
        }

        [HttpPost]
        public ActionResult DeleteTeam(int teamID)
        {
            // Find the team with this id and remove that sucka.
            bool success = dbm.RemoveTeam(teamID);
            return Json("");
        }

        [HttpPost]
        public ActionResult SetFavoriteTeam(int compID, int teamID)
        {
            // Set a favorite team

            dbm.SetFavoriteTeam(User.Identity.GetUserId(), compID, teamID);

            Team favTeam = dbm.GetFavoriteTeamByUserID(User.Identity.GetUserId());

            return Json(favTeam.ID);
        }
    }
}