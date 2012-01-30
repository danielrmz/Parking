using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Parking.UI.Controllers
{
    public class StaticController : Controller
    {

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
