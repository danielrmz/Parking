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
    [AttributeUsage(AttributeTargets.Method,AllowMultiple=true)]
    internal class ApiAttribute : System.Attribute
    {
        private string _route;
        private ApiMethod _method;
        private bool _isSecure = false;
        private int _roleLevel = 0;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="route"></param>
        /// <param name="method"></param>
        public ApiAttribute(string route, ApiMethod method)
        {
            this._route = route;
            this._method = method;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="route"></param>
        /// <param name="method"></param>
        /// <param name="isSecure"></param>
        public ApiAttribute(string route, ApiMethod method, bool isSecure)
        {
            this._route = route;
            this._method = method;
            this._isSecure = isSecure;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="route"></param>
        /// <param name="method"></param>
        /// <param name="isSecure"></param>
        /// <param name="roleLevel"></param>
        public ApiAttribute(string route, ApiMethod method, bool isSecure, AccessLevel roleLevel)
        {
            this._route = route;
            this._method = method;
            this._isSecure = isSecure;
            this._roleLevel = (int)roleLevel;
        }

        /// <summary>
        /// Gets the role level
        /// </summary>
        /// <returns></returns>
        public int GetRoleLevel()
        {
            return this._roleLevel;
        }

        /// <summary>
        /// Indicates if the api action is secure or not
        /// </summary>
        /// <returns></returns>
        public bool IsSecure()
        {
            return _isSecure;
        }

        /// <summary>
        /// Returns the route regex
        /// </summary>
        /// <returns></returns>
        public string GetRoute()
        {
            return _route;
        }

        /// <summary>
        /// Returns the method by which this route belongs to
        /// </summary>
        /// <returns></returns>
        public ApiMethod GetMethod()
        {
            return _method;
        }

    }

}
