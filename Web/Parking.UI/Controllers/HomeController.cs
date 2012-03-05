using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using Sieena.Parking.Common.Utils;
using Sieena.Parking.API.Models;

namespace Parking.UI.Controllers
{
    /// <summary>
    /// Home controller
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// Displays the blank base page
        /// </summary>
        /// <returns></returns>
        public ActionResult Blank()
        {
            return View();
        } 
    }
}
