using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Nancy;
using Nancy.ViewEngines.Razor;
using Nancy.Serializers.Json;

namespace Parking.API.Modules
{
    public class PlacesModule : AbstractBaseModule
    {
        public PlacesModule()
            : base("Places")
        {
            Get["/"] = parameters =>
            {
                return Response.AsJson(new { Time = DateTime.Now });
            };
        }
    }
}