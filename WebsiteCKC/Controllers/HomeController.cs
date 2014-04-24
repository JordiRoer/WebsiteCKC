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
            Competition comp = dbm.GetCompetitionByID(8);
            List<Team> teamss = dbm.GetTeamsByCompID(comp.ID);
            List<TeamStats> teams = statsCenter.SortTeamsByPoint(teamss);
            
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

        
    }
}