/**
 *
 * @package     Parking.API.Modules
 * @author      The JSONs
 * @copyright   2012 -
 * @license     Propietary
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sieena.Parking.API.Modules.Classes
{
    /// <summary>
    /// Provides a wrapper to specify the route and HTTP method
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class ApiAttribute : System.Attribute
    {
        private string _route;
        private ApiMethod _method;

        public ApiAttribute(string route, ApiMethod method)
        {
            this._route = route;
            this._method = method;
        }

        public string GetRoute()
        {
            return _route;
        }

        public ApiMethod GetMethod()
        {
            return _method;
        }

    }

    public enum ApiMethod {
        GET,
        PUT,
        POST,
        DELETE, 
        GETPOST
    }
}
