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
    internal class ApiAttribute : System.Attribute
    {
        private string _route;
        private ApiMethod _method;
        private bool _isSecure = false;
        private int _roleLevel = 0;

        public ApiAttribute(string route, ApiMethod method)
        {
            this._route = route;
            this._method = method;
        }

        public ApiAttribute(string route, ApiMethod method, bool isSecure)
        {
            this._route = route;
            this._method = method;
            this._isSecure = isSecure;
        }

        public ApiAttribute(string route, ApiMethod method, bool isSecure, AccessLevel roleLevel)
        {
            this._route = route;
            this._method = method;
            this._isSecure = isSecure;
            this._roleLevel = (int)roleLevel;
        }

        public int GetRoleLevel()
        {
            return this._roleLevel;
        }

        public bool IsSecure()
        {
            return _isSecure;
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
