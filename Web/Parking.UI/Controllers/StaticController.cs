using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using Sieena.Parking.Common.Resources;
using System.Threading;

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
         
        public ActionResult i18n(string cultureName)
        {
            cultureName = string.IsNullOrEmpty(cultureName) ? Thread.CurrentThread.CurrentCulture.Name : cultureName;
            
            return new JsonResult() { Data = Utilities.GetResources(cultureName), JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}
