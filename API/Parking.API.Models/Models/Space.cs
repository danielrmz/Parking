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
    public partial class Space : ISpace
    {
        
        /// <summary>
        /// Gets all the parking spaces registered in a place
        /// </summary>
        /// <returns></returns>
        public static List<Space> GetAllByPlaceId(int id)
        {
            using (EntityContext ctx = new EntityContext())
            {
                return ctx.Spaces.Where(s => s.PlaceId.Equals(id) && s.Deleted == false).ToList();
            }
        }

        /// <summary>
        /// Gets a specific instance 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Space Get(int id)
        {
            using (EntityContext ctx = new EntityContext())
            {
                return ctx.Spaces.Where(s => s.SpaceId.Equals(id)).FirstOrDefault();
            }
        }

        /// <summary>
        /// Saves or updates a space
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static Space Save(Space s)
        {
            using (EntityContext ctx = new EntityContext())
            {
                s.ValidateAndRaise();

                if (s.SpaceId == 0)
                {
                    ctx.Spaces.AddObject(s);
                }

                ctx.SaveChanges();

                return s;
            }
        }

        /// <summary>
        /// Deletes a space
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Space Delete(int id)
        {
            using (EntityContext ctx = new EntityContext())
            {
                Space s = ctx.Spaces.Where(sx => sx.SpaceId == id).FirstOrDefault();
                ctx.Spaces.DeleteObject(s);
                ctx.SaveChanges();
                return s;
            }
        }

    }
}
