using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Nancy;
using Nancy.ViewEngines.Razor;
using Nancy.Serializers.Json;

namespace Parking.API.Modules
{
    public class AccessTypesModule : AbstractBaseModule
    {
        public AccessTypesModule()
            : base("AccessTypes")
        {
            Get["/"] = parameters =>
            {
                return Response.AsJson(new { Time = DateTime.Now });
            };
        }
    }
}