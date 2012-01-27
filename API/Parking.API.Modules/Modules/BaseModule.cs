using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Nancy;

namespace Sieena.Parking.API.Modules
{
    public class BaseModule : NancyModule
    {
        public BaseModule() : base("/")
        {
            Get["/"] = parameters =>
            {
                return "Parking's API Endpoint, v.0.0.1";
            };
        }
    }
}