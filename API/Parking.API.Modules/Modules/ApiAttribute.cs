using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sieena.Parking.API.Modules
{
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
