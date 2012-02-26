﻿/**
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
        public Checkin CheckIn(User u, APISession session, DynamicDictionary parms)
        {
            Checkin s = parms.Fill<Checkin>();
            return Checkin.CheckIn(s);
        }

        [Api("/current", ApiMethod.PUT, true)]
        public Checkin CheckInPUT(User u, APISession session, DynamicDictionary parms)
        {
            Checkin s = parms.Fill<Checkin>();
            return Checkin.CheckIn(s);
        }

        [Api("/{id}", ApiMethod.PUT, true)]
        public Checkin CheckOut(User u, APISession session, DynamicDictionary parms)
        {
            return Checkin.CheckOut(parms["id"]);
        }


        [Api("/user/{id}/", ApiMethod.GET, true)]
        public Checkin GetCurrentForUser(User u, APISession session, DynamicDictionary parms)
        {
            return Checkin.GetLastForUser(parms["id"]);
        }
    }
}