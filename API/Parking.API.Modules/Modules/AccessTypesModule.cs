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
    public class AccessTypesModule : AbstractBaseModule
    {
        public AccessTypesModule()
            : base("AccessTypes")
        {
        }

        [Api("/GetAll", ApiMethod.GET)]
        public List<AccessType> GetAll(dynamic parameters)
        {
            return AccessType.GetAll();
        }
    }
}