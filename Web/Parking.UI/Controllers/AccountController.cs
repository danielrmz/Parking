using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using Sieena.Parking.API.Models.Views;
using APIUser = Sieena.Parking.API.Models.User;
using APISession = Sieena.Parking.API.Models.Session;
using Crypto = Sieena.Parking.Common.Utils.Crypto;
using i18n = Sieena.Parking.Common.Resources.UI;
using System.Configuration;

namespace Sieena.Parking.UI.Controllers
{
    public class AccountController : Controller
    {

        public ActionResult LogOn()
        {
            return View("Blank");
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult LogOn(LogOnModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                string userName = model.UserName.IndexOf('@') < 0 ? model.UserName + model.Domain : model.UserName;

                if (Membership.ValidateUser(userName, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                    APIUser user = APIUser.GetByEmail(model.UserName + "@sieena.com");
                    APISession.Set(user.UserId, FormsAuthentication.Timeout, model.RememberMe);

                    return Envelope(APIUser.GetUserInformation(user), false); 
                }
                else
                {
                    return Envelope(i18n.Login_ErrorValidation, true);  
                }
            }

            // If we got this far, something failed, redisplay form 
            return Envelope(i18n.Login_ErrorValidation, true);  
        }

        /// <summary>
        /// Wraps the JsonResult with the appropiate data.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="isError"></param>
        /// <returns></returns>
        protected JsonResult Envelope(dynamic data, bool isError)
        {
            string t = data.GetType().Name;
            return new JsonResult()
            {
                Data = new
                    {
                        Time = ConvertToUnixTime(DateTime.Now),
                        Response = data,
                        Type = t,
                        Error = isError
                    }
            };
        }

        /// <summary>
        /// Converts a datetime to unixtime.
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        protected double ConvertToUnixTime(DateTime date)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            TimeSpan diff = date - origin;
            return Math.Floor(diff.TotalSeconds);
        }
    }
}
