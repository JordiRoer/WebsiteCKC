using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.WebPages.Html;
using WebsiteCKC.Models;

namespace WebsiteCKC.Controllers
{
    public class StatisticsController : Controller
    {
        DatabaseManager dbm = new DatabaseManager();
        StatisticsCenter statsCenter = new StatisticsCenter();

        // GET: Statistics
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Table()
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
            ViewBag.Competition = competition;
            return View();
        }
    }
}