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
    public class HomeController : Controller
    {
        public ActionResult Blank()
        {
            return View();
        }

        public ActionResult IM()
        {
            using (StreamReader reader = new StreamReader(Request.InputStream))
            {
                List<MessageQueue> pending = MessageQueue.GetPending();

                List<int> ids = pending.Select( m =>m.MessageId).ToList();

                string result = TropoFactory.ParseSession(reader, MessageQueue.GetPendingAsDictionary(ids) );

                MessageQueue.ClearIds(ids);

                return Content(result, "application/json");
            }

           
        }
    }
}
