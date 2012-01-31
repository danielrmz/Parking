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
    public class ApplicationsModule : AbstractBaseModule
    {
        public ApplicationsModule()
            : base("Applications")
        {
        }

        [Api("/GetAll", ApiMethod.GET)]
        public List<Application> GetAll(dynamic parameters)
        {
            return Application.GetAll();
        }
    }
}