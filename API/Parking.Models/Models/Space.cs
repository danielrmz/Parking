/**
 *
 * @package     Parking.API.Models
 * @author      The JSONs
 * @copyright   2012 - 20XX
 * @license     Propietary
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sieena.Parking.API.Models
{
    using Interfaces;

    /// <summary>
    /// Represents a parking space.
    /// </summary>
    public partial class Space : ParkingModel, ISpace
    {
        
        /// <summary>
        /// Gets all the parking spaces registered in a place
        /// </summary>
        /// <returns></returns>
        public static List<Space> GetAllByPlaceId(int id)
        {
            return ctx.Spaces.Where( s => s.PlaceId.Equals(id) && s.Deleted == false ).ToList(); 
        }

        /// <summary>
        /// Gets a specific instance 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Space Get(int id)
        {
            return ctx.Spaces.Where(s => s.SpaceId.Equals(id)).FirstOrDefault(); 
        }

        /// <summary>
        /// Saves or updates a space
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static Space Save(Space s)
        {
            
            s.ValidateAndRaise();

            if (s.SpaceId == 0)
            {
                ctx.Spaces.InsertOnSubmit(s);
            }

            ctx.SubmitChanges();

            return s;
        }

        /// <summary>
        /// Deletes a space
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Space Delete(int id)
        {
            Space s = Get(id);
            ctx.Spaces.DeleteOnSubmit(s);
            ctx.SubmitChanges();
            return s;
        }

    }
}
