using System;

using System.Web;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;

using Nancy;
using Nancy.ViewEngines.Razor;
using Nancy.Serializers.Json;
using System.Text.RegularExpressions;

namespace Sieena.Parking.API.Modules
{
    public abstract class AbstractBaseModule : NancyModule
    {
        public AbstractBaseModule(string modulePath)
            : base(modulePath)
        {
            Get["/"] = parameters =>
            {
                Type t = this.GetType();

                List<string> methods = t.GetMethods()
                                        .Where(mi => {
                                            return mi.GetCustomAttributes(typeof(ApiAttribute), true).Any();
                                        })
                                        .Select(mi => mi.Name)
                                        .ToList();

                // Get available public methods to display.
                return Envelope(methods);
            };

            Get["/ping"] = parameters =>
            {
                return Envelope("pong");
            };

            this.RegisterAPIMethods();
        }

        private void RegisterAPIMethods()
        {
            Type t = this.GetType();
            t.GetMethods()
             .Where(mi =>
             {
                return mi.GetCustomAttributes(typeof(ApiAttribute), true).Any();
             })
             .ToList()
             .ForEach( mi => {
                ApiAttribute tag = mi.GetCustomAttributes(typeof(ApiAttribute), true).First() as ApiAttribute;

                string route = tag.GetRoute();
                switch (tag.GetMethod())
                {
                    case ApiMethod.GET:
                        Get[route] = parameters => mi.Invoke(this, new object[] { parameters }) as Response;
                        break;
                    case ApiMethod.POST:
                        Post[route] = parameters => mi.Invoke(this, new object[] { parameters }) as Response;
                        break;
                    case ApiMethod.PUT:
                        Put[route] = parameters => mi.Invoke(this, new object[] { parameters }) as Response;
                        break;
                    case ApiMethod.DELETE:
                        Delete[route] = parameters => mi.Invoke(this, new object[] { parameters }) as Response;
                        break;
                    case ApiMethod.GETPOST: 
                        Get[route] = parameters => mi.Invoke(this, new object[] { parameters }) as Response;
                        Post[route] = parameters => mi.Invoke(this, new object[] { parameters }) as Response;
                        break;
                }
             });
                                        
        }

        protected Response Envelope(dynamic data)
        {
            return Response.AsJson(new {
                Time = ConvertToUnixTime(DateTime.Now),
                Response = data,
                IsError = false
            });
        }

        protected double ConvertToUnixTime(DateTime date)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            TimeSpan diff = date - origin;
            return Math.Floor(diff.TotalSeconds);
        }
    }
}