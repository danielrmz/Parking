/**
 *
 * @package     Parking.API.Modules
 * @author      The JSONs
 * @copyright   2012 -
 * @license     Propietary
 */
using System;
using System.Web;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;

using Nancy;
using Nancy.ViewEngines.Razor;
using Nancy.Serializers.Json;
using System.Text.RegularExpressions;

namespace Sieena.Parking.API.Modules.Classes
{
    /// <summary>
    /// Autoregisters the API Methods based on the attributes
    /// </summary>
    public abstract class AbstractBaseModule : NancyModule
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="modulePath"></param>
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
                                        .Select(mi => {
                                            var attr = mi.GetCustomAttributes(typeof(ApiAttribute), true).First() as ApiAttribute;
                                            return string.Format("{0}\t{1}\t- {2}", attr.GetMethod(), attr.GetRoute(), mi.Name);
                                        })
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

        /// <summary>
        /// Registers the API Methods
        /// </summary>
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
                
                Func<dynamic, Response> method = (parameters) => {
                    try
                    {
                        return Envelope(mi.Invoke(this, new object[] { parameters }));
                    }
                    catch (Exception e) {
                        return Envelope(e);
                    }
                };

                string route = tag.GetRoute();
                switch (tag.GetMethod())
                {
                    case ApiMethod.GET:
                        Get[route] = method;
                        break;
                    case ApiMethod.POST:
                        Post[route] = method;
                        break;
                    case ApiMethod.PUT:
                        Put[route] = method;
                        break;
                    case ApiMethod.DELETE:
                        Delete[route] = method;
                        break;
                    case ApiMethod.GETPOST:
                        Get[route] = method;
                        Post[route] = method;
                        break;
                }
             });
                                        
        }


        /// <summary>
        /// Wraps the data that will be returned into a standard message with additional fixed data.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        protected Response Envelope(dynamic data)
        {
            string type = string.Empty;
            try
            {
                Type t = data.GetType();
                type = t.Name;

                if (t.IsGenericType)
                {
                
                    type = t.GetGenericArguments()[0].Name;
                }
                
            }
            catch (Exception e)
            {
                data = e;
            }

            bool isError = false;
            if (data is Exception)
            {
                isError = true;
                type = data.InnerException != null ? data.InnerException.GetType().Name : type;
                data = data.InnerException != null ? data.InnerException.Message : data.Message;
            }

            return Response.AsJson(new {
                Time = ConvertToUnixTime(DateTime.Now),
                Response = data,
                Type = type,
                Error = isError
            });
        }

        /// <summary>
        /// Converts a datetime to unixtime.
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        protected double ConvertToUnixTime(DateTime date)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            TimeSpan diff = date - origin;
            return Math.Floor(diff.TotalSeconds);
        }
    }
}