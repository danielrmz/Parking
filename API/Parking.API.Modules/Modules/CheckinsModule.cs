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
    public class CheckinsModule : AbstractBaseModule
    {
        public CheckinsModule()
            : base("Checkins")
        {
          
        }

        [Api("/GetAll", ApiMethod.GET)]
        public List<Checkin> GetAll(dynamic parameters)
        {
            return Checkin.GetAll();
        }
    }
}