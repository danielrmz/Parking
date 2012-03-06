using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using Sieena.Parking.Common.Utils;
using Sieena.Parking.API.Models;
using System.Dynamic; 

namespace Parking.UI.Controllers
{
    /// <summary>
    /// Endpoint to process external requests that have to be on the frontend server.
    /// </summary>
    public class ExternalController : Controller
    { 
        /// <summary>
        /// Parses the Tropo Session request.
        /// </summary>
        /// <returns></returns>
        public ActionResult Tropo()
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
