/**
 *
 * @package     Parking.API.Modules
 * @author      The JSONs
 * @copyright   2012 - 20XX
 * @license     Propietary
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Nancy;
using Nancy.ViewEngines.Razor;
using Nancy.Serializers.Json;
using Sieena.Parking.API.Models;

namespace Sieena.Parking.API.Modules
{
    using Classes;
    using Sieena.Parking.API.Models.Views;
    using APISession = Sieena.Parking.API.Models.Session;
    using System.Configuration;

    public class AuthModule : AbstractBaseModule
    {
        public AuthModule()
            : base("session")
        {
          
        }

        [Api("/ValidateUser", ApiMethod.GET)]
        public bool ValidateUser(DynamicDictionary parameters)
        {
            return User.VerifyCredentials(parameters["user"], parameters["password"]);
        }

        [Api("/", ApiMethod.GET, true)]
        public UserInformation Current(User u, APISession session, DynamicDictionary parameters)
        {
            return User.GetUserInformation(u);    
        }

        [Api("/", ApiMethod.DELETE, true)]
        public void DestroySession(User u, APISession session, DynamicDictionary parameters)
        {
            APISession.Expire(session.SessionId);
        }

    }
}