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
using Nancy.ViewEngines.Razor;
using Nancy.Serializers.Json;
using Sieena.Parking.API.Models;

namespace Sieena.Parking.API.Modules
{
    using Classes;

    public class UsersModule : AbstractBaseModule
    {
        public UsersModule()
            : base("users")
        {
        }

        [Api("/{id}", ApiMethod.GET)]
        public User GetUser(DynamicDictionary parameters)
        {
            return null;
        }

        [Api("/", ApiMethod.POST)]
        public User AddUser(DynamicDictionary parameters)
        {
            return null;
        }

        [Api("/{id}", ApiMethod.PUT, true)]
        public User UpdateUser(DynamicDictionary parameters)
        {
            return null;
        }

        [Api("/{id}", ApiMethod.DELETE, true, AccessLevel.Admin)]
        public User DeleteUser(DynamicDictionary parameters)
        {
            return null;
        }
    }
}