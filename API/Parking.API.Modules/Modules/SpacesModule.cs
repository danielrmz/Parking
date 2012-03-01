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


namespace Sieena.Parking.API.Modules
{
    using Classes;
    using APISession = Sieena.Parking.API.Models.Session;
    using Sieena.Parking.API.Models;
    using Sieena.Parking.Common.Utils;

    public class SpacesModule : AbstractBaseModule
    {
        public SpacesModule() : base("spaces")
        {
        }

        [Api("/GetAllByPlaceId/{placeid}", ApiMethod.GET)]
        public List<Space> GetAllByPlaceId(DynamicDictionary parameters)
        {
            return Space.GetAllByPlaceId(parameters["placeid"]);
        }

        [Api("/{spaceid}", ApiMethod.GET)]
        public Space GetSpace(DynamicDictionary parameters)
        {
            return Space.Get(parameters["spaceid"]);
        }

        [Api("/", ApiMethod.POST)]
        public Space AddSpace(DynamicDictionary parameters)
        {
            Space s = parameters.Fill<Space>();
            s.Deleted = false;
            s.CreatedAt = DateTime.Now.ToCommonTime();
            s.SpaceId = 0;
            return Space.Save(s);
        }

        [Api("/{placeid}", ApiMethod.PUT)]
        public Space UpdateSpace(DynamicDictionary parameters)
        {
            Space s = Space.Get(parameters["placeid"]);
            parameters.Fill(s);
            //Space s = parameters.Fill<Space>();
            if (s.SpaceId == 0)
            {
                throw new Exception("A space id must be specified");
            }
            return Space.Save(s);
        }

        public Space DeleteSpace(DynamicDictionary parameters)
        {
            return Space.Delete(parameters["spaceid"]);
        }

        [Api("/blockings", ApiMethod.GET, true)]
        public List<SpaceBlocking> GetAllBlockings(User u, APISession session, DynamicDictionary parameters)
        {
            return SpaceBlocking.GetAll();
        }
    }
}