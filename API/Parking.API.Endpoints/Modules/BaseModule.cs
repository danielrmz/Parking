/**
 *
 * @package     Parking.API.Modules
 * @author      The JSONs
 * @copyright   2012 - 20XX
 * @license     Propietary
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Nancy;

namespace Sieena.Parking.API.Modules
{
    using Classes;
    using System.Reflection;

    public class BaseModule : NancyModule
    {
        public BaseModule() : base("/")
        {
            Get["/"] = parameters =>
            {
                Dictionary<string, object> result = new Dictionary<string, object>();
                
                result["title"] = "Parking's API Endpoint";
                result["version"] = this.GetType().Assembly.GetName().Version.ToString();
                List<string> endpoints = new List<string>();

                Type[] types = Assembly.GetExecutingAssembly().GetTypes();
                foreach (Type type in types)
                {
                    if (type.IsSubclassOf(typeof(AbstractBaseModule)))
                    {
                        endpoints.Add(string.Format("{0}", type.Name.Replace("Module", "").ToLower()));
                    }
                }

                result["endpoints"] = endpoints;

                return Response.AsJson(result);
            };
        }
    }
}