using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Nancy;
using Nancy.ViewEngines.Razor;
using Nancy.Serializers.Json;

namespace Parking.API.Modules
{
    public class SpacesModule : AbstractBaseModule
    {
        public SpacesModule() : base("Spaces")
        {
            Get["/"] = parameters =>
            {
                return Response.AsJson(new { Time = DateTime.Now });
            };
        }
    }
}