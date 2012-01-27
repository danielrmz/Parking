using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Nancy;
using Nancy.ViewEngines.Razor;
using Nancy.Serializers.Json;

namespace Sieena.Parking.API.Modules
{
    public class SpacesModule : AbstractBaseModule
    {
        public SpacesModule() : base("Spaces")
        {
        }

        [Api("/GetAll", ApiMethod.GET)]
        public Response GetAll(dynamic parameters)
        {
            return Envelope(new string[]{ "Test1","Test2" });
        }
    }
}