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
    public class UsersModule : AbstractBaseModule
    {
        public UsersModule()
            : base("Users")
        {
        }

        [Api("/{id}", ApiMethod.GET)]
        public User GetUser(dynamic parameters)
        {
            return null;
        }

        [Api("/", ApiMethod.PUT)]
        public User AddUser(dynamic parameters)
        {
            return null;
        }

        [Api("/{id}", ApiMethod.POST)]
        public User UpdateUser(dynamic parameters)
        {
            return null;
        }

        [Api("/{id}", ApiMethod.DELETE)]
        public User DeleteUser(dynamic parameters)
        {
            return null;
        }
    }
}