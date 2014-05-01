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
        StatisticsCenter statsCenter = new StatisticsCenter();

        public ActionResult Index()
        {
            Competition competition;
            List<Team> teams = null;
            List<TeamStats> sortedTeams = null;

            competition = dbm.GetCompetition();

            if (competition != null)
            {
                teams = dbm.GetTeamsByCompID(competition.ID);
                sortedTeams = statsCenter.SortTeamsByPoint(teams);
            }


            ViewData["Teams"] = sortedTeams;
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

        public ActionResult EmptyDatabase()
        {
            List<Team> teams = (from t in db.Teams select t).ToList();
            foreach(Team team in teams)
            {
                db.Teams.Remove(team);
                db.SaveChanges();
            }
            List<Competition> comps = (from c in db.Competitions select c).ToList();
            foreach(Competition comp in comps)
            {
                db.Competitions.Remove(comp);
                db.SaveChanges();
            }

            List<OwnedTeam> ownedteams = (from ot in db.OwnedTeams select ot).ToList();
            foreach (OwnedTeam oteam in ownedteams)
            {
                db.OwnedTeams.Remove(oteam);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}