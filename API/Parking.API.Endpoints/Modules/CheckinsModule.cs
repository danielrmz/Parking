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
using Nancy.ModelBinding;
using Nancy.ViewEngines.Razor;
using Nancy.Serializers.Json;

namespace Sieena.Parking.API.Modules
{
    using Classes;
    using APISession = Sieena.Parking.API.Models.Session;
    using Sieena.Parking.API.Models;
    using Sieena.Parking.API.Models.Views;

    public class CheckinsModule : AbstractBaseModule
    {
        public CheckinsModule()
            : base("checkins")
        {
          
        }

        [Api("/GetAll", ApiMethod.GET, true, AccessLevel.Admin)]
        public List<Checkin> GetAll(User u, APISession session, DynamicDictionary parameters)
        { 
            return Checkin.GetAll();
        }


        [Api("/current", ApiMethod.GET, true)]
        public List<Checkin> GetCurrent(User u, APISession session, DynamicDictionary parameters)
        {
            return Checkin.GetCurrent();
        }

        [Api("/history/{amount}", ApiMethod.GET, true)]
        public List<CheckinNotification> GetLast(User u, APISession session, DynamicDictionary parameters)
        {
            return Checkin.GetNotificationStream(parameters["amount"]);
        }

        [Api("/", ApiMethod.POST, true)]
        public Checkin CheckOutCurrent(User u, APISession session, DynamicDictionary parms)
        {
            return Checkin.CheckOutByUserId(u.UserId);
        }

        [Api("/current", ApiMethod.POST, true)]
        public Checkin CheckInPUT(User u, APISession session, DynamicDictionary parms)
        {
            Checkin t = this.Bind(); 
            t.RegisteredBy = u.UserId;

            // Check roles to see if the user can post
            if (!u.IsAdmin() || t.UserId == 0)
            {
                t.UserId = u.UserId;
            }
             
            return Checkin.CheckIn(t);
        }

        [Api("/{id}", ApiMethod.DELETE, true)]
        public Checkin CheckOut(User u, APISession session, DynamicDictionary parms)
        {
            if (u.IsAdmin())
            {
                return Checkin.CheckOut(parms["id"]);
            }
            else
            {
                Checkin cin = Checkin.GetLastForUser(u.UserId);
                if (cin.EndTime.HasValue)
                {
                    return cin;
                }
                return Checkin.CheckOut(cin.CheckInId);
            }
        }


        [Api("/user/{id}/", ApiMethod.GET, true)]
        public Checkin GetCurrentForUser(User u, APISession session, DynamicDictionary parms)
        {
            return Checkin.GetLastForUser(parms["id"]);
        }
    }
}