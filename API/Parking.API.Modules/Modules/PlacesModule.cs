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

namespace Sieena.Parking.API.Modules
{
    using Sieena.Parking.API.Models;
    using Classes;

    public class PlacesModule : AbstractBaseModule
    {
        public PlacesModule()
            : base("Places")
        {
        }

        [Api("/GetAll", ApiMethod.GET)]
        public List<Place> GetAll(dynamic parameters)
        {
            return Place.GetAll();
        }
    }
}