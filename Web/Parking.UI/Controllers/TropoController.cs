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
    public class TropoController : Controller
    { 
        /// <summary>
        /// Parses the Tropo Session request.
        /// </summary>
        /// <returns></returns>
        public ActionResult Execute()
        {
            using (StreamReader reader = new StreamReader(Request.InputStream))
            {
                List<MessageQueue> pending = MessageQueue.GetPending();

                List<int> ids = pending.Select(m => m.MessageId).ToList();

                string result = TropoFactory.ExecuteSession(reader, MessageQueue.GetPendingAsDictionary(ids));

                MessageQueue.ClearIds(ids);

                return Content(result, "application/json");
            }

        }
    }
}
