using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace Parking.UI.Controllers
{
    public class StaticController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult About() {
            return View();
        }

        public ActionResult Help() {
            return View();
        }

        public ActionResult Privacy() {
            return View();
        }

        public ActionResult Terms() {
            return View();
        } 

    }
}
