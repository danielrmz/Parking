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
    /// A whole container of spaces. i.e. Parking Lot
    /// </summary>
    public partial class Place :  IPlace
    {
        /// <summary>
        /// Gets all the available parking lots in the system.
        /// </summary>
        /// <returns></returns>
        public static List<Place> GetAll()
        {
            using (EntityContext ctx = new EntityContext())
            {
                return ctx.Places.ToList();
            }
        }

        /// <summary>
        /// Gets a specific place
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Place Get(int id)
        {
            using (EntityContext ctx = new EntityContext())
            {
                return ctx.Places.Where(p => p.PlaceId.Equals(id)).FirstOrDefault();
            }
        }

        /// <summary>
        /// Saves (insert or updates) a place
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static Place Save(Place p)
        {
            using (EntityContext ctx = new EntityContext())
            {
                p.ValidateAndRaise();

                if (p.PlaceId == 0)
                {
                    ctx.Places.AddObject(p);
                }

                ctx.SaveChanges();

                return p;
            }
        }

        /// <summary>
        /// Deletes a Place
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Place Delete(int id)
        {
            using (EntityContext ctx = new EntityContext())
            {
                Place p = ctx.Places.Where(px => px.PlaceId == id).FirstOrDefault();
                ctx.Places.DeleteObject(p); 
                ctx.SaveChanges();
                return p;
            }

        }
    }

}
