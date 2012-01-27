using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Nancy;
using Nancy.ViewEngines.Razor;
using Nancy.Serializers.Json;

namespace Sieena.Parking.API.Modules
{
    public class CheckinsModule : AbstractBaseModule
    {
        public CheckinsModule()
            : base("Checkins")
        {
          
        }
    }
}