using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using Sieena.Parking.API.Models.Views;
using APISession = Sieena.Parking.API.Models.Session;
using Crypto = Sieena.Parking.Common.Utils.Crypto;
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
                    int userId = API.Models.User.GetByEmail(model.UserName + "@sieena.com").UserId;
                    API.Models.UserInfo ui = API.Models.UserInfo.GetByUserId(userId);

                    APISession.Expire(userId);
                    APISession s = APISession.Set(new APISession() { 
                        UserId = userId ,
                        CreatedAt = DateTime.Now,
                        ExpiresAt = DateTime.Now.Add(FormsAuthentication.Timeout),
                        LastAccess= DateTime.Now,
                        Data = string.Format("FormsCookieName={0},FormsCookieValue={1}", model.UserName, FormsAuthentication.GetAuthCookie(model.UserName, model.RememberMe).Value)
                    });

                    string returnU = string.Empty; 
                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                        && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                    {
                        returnU = returnUrl;
                    }
                    else
                    {
                        returnU = "Home/Index";
                    }

                    API.Models.Role role = API.Models.Role.GetRolesForUser(userName).First();

                    return Envelope(new UserInformation { 
                                SessionId = Crypto.EncryptStringAES(s.SessionId.ToString(), ConfigurationManager.AppSettings["Crypto.Secret"]),
                                Email = userName, 
                                UserName = model.UserName, 
                                IsAuthenticated = true, 
                                ProfilePictureUrl = "", 
                                FirstName = ui.FirstName, 
                                LastName = ui.LastName,
                                Role = role.RoleName,
                                RoleId = role.RoleId
                    }, false);

                    
                }
                else
                {
                    return Envelope(new { Sieena.Parking.Common.Resources.UI.Login_ErrorValidation }, true);  
                }
            }

            // If we got this far, something failed, redisplay form 
            return Envelope(new { Sieena.Parking.Common.Resources.UI.Login_ErrorValidation }, true);  
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
