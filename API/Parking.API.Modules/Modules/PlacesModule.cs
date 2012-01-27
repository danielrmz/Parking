using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Nancy;
using Nancy.ViewEngines.Razor;
using Nancy.Serializers.Json;

namespace Sieena.Parking.API.Modules
{
    using Sieena.Parking.API.Models;

    public class PlacesModule : AbstractBaseModule
    {
        public PlacesModule()
            : base("Places")
        {
        }

        [Api("/GetAll", ApiMethod.GET)]
        public Response GetAll(dynamic parameters)
        {
            return Envelope(Place.GetAll());
        }
    }
}