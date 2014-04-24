using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteCKC.Models;

namespace WebsiteCKC.Controllers
{
    public class MatchController : Controller
    {
        DatabaseManager dbm = new DatabaseManager();
        // GET: Match
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(MatchViewModels model)
        {
            if(ModelState.IsValid)
            {
                TempData["MatchAdded"] = "Added the match";
                RedirectToAction("Table", "Statistics");
            }

            TempData["MatchAdded"] = "Error on adding the match.";

            ModelState.AddModelError("", "Niet alle gegevens zijn juist ingevuld.");
            return RedirectToAction("Table", "Statistics", model);
        }
    }
}