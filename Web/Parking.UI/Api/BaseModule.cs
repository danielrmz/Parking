using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Nancy;

namespace Parking.UI.Api
{
    public class BaseModule : NancyModule
    {
        public BaseModule() : base("/")
        {
            Get["/"] = parameters =>
            {
                return "API Endpoint";
            };
        }
    }
}