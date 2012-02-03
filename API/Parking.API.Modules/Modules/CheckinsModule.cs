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
using System.Web;

using Nancy;
using Nancy.ViewEngines.Razor;
using Nancy.Serializers.Json;
using Sieena.Parking.API.Models;

namespace Sieena.Parking.API.Modules
{
    using Classes;

    public class CheckinsModule : AbstractBaseModule
    {
        public CheckinsModule()
            : base("Checkins")
        {
          
        }

        [Api("/GetAll", ApiMethod.GET)]
        public List<Checkin> GetAll(dynamic parameters)
        {
            return Checkin.GetAll();
        }
    }
}