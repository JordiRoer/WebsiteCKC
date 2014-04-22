using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteCKC.Models;

namespace WebsiteCKC.Controllers
{
    public class HomeController : Controller
    {
        CKCDatabase db = new CKCDatabase();
        DatabaseManager dbm = new DatabaseManager();

        public ActionResult Index()
        {
            Competition comp = dbm.GetCompetitionByID(8);
            List<Team> teams = dbm.GetTeamsByCompID(comp.ID);
            ViewData["Teams"] = teams;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult FeedDatabase()
        {
            dbm.ClearCompetitions();
            dbm.ClearTeams();
            int compID = dbm.AddCompetition(7, 08);
            List<Team> teams = new List<Team>{
                new Team{
                    ClubName = "CVV-Mercurius",
                    TeamNumber = 1,
                    CompID = compID
                },
                new Team{
                    ClubName = "SVS",
                    TeamNumber = 4,
                    CompID = compID
                },new Team{
                    ClubName = "Antibarbari",
                    TeamNumber = 11,
                    CompID = compID
                },new Team{
                    ClubName = "Blijdorp",
                    TeamNumber = 4,
                    CompID = compID
                },new Team{
                    ClubName = "VOB",
                    TeamNumber = 2,
                    CompID = compID
                },new Team{
                    ClubName = "Pretoria",
                    TeamNumber = 4,
                    CompID = compID
                },new Team{
                    ClubName = "CKC",
                    TeamNumber = 5,
                    CompID = compID
                },new Team{
                    ClubName = "CWO",
                    TeamNumber = 2,
                    CompID = compID
                },new Team{
                    ClubName = "TOGB",
                    TeamNumber = 11,
                    CompID = compID
                },new Team{
                    ClubName = "Sparta (AV)",
                    TeamNumber = 2,
                    CompID = compID
                }
            };

            foreach(Team team in teams)
            {
                int teamID = dbm.AddTeam(team.ClubName, team.TeamNumber, team.CompID);
            }

            return RedirectToAction("Index");
        }
    }
}