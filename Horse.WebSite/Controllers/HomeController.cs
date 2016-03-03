using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Horse.WebSite.Models;

namespace Horse.WebSite.Controllers
{
    public class HomeController : BaseUserController
    {
        private HorseData db = new HorseData();

        public ActionResult Index()
        {
            var machines = from u in db.Users
                           join um in db.UserMachines on u.Id equals um.Uid
                           join m in db.Machines on um.Mid equals m.Id
                           select m;
            var myMachines = machines.ToList();

            return View(myMachines);
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