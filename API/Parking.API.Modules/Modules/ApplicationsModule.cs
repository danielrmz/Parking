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

    public class ApplicationsModule : AbstractBaseModule
    {
        public ApplicationsModule()
            : base("applications")
        {
        }

        [Api("/GetAll", ApiMethod.GET)]
        public List<Application> GetAll(dynamic parameters)
        {
            return Application.GetAll();
        }
    }
}