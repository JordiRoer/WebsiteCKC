using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
            Competition comp = dbm.GetCompetitionByID(8);
            List<Team> teams = dbm.GetTeamsByCompID(comp.ID);
            List<TeamStats> orderedTeams = statsCenter.SortTeamsByPoint(teams);

            ViewData["Teams"] = orderedTeams;
            ViewBag.Competition = comp;
            return View();
        }
    }
}