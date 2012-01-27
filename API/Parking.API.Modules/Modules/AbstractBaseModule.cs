using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Nancy;
using Nancy.ViewEngines.Razor;
using Nancy.Serializers.Json;

namespace Parking.API.Modules
{
    public abstract class AbstractBaseModule : NancyModule
    {
        public AbstractBaseModule(string modulePath)
            : base(modulePath)
        {
            Get["/ping"] = parameters =>
            {
                return Response.AsJson(new { Time = ConvertToUnixTime(DateTime.Now), Response = "pong" });
            };
        }

        public double ConvertToUnixTime(DateTime date)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            TimeSpan diff = date - origin;
            return Math.Floor(diff.TotalSeconds);
        }
    }
}