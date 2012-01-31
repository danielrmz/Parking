using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sieena.Parking.API.Models
{
    /// <summary>
    /// Represents a parking space.
    /// </summary>
    public partial class Space
    {
        /// <summary>
        /// Gets all the parking spaces available 
        /// </summary>
        /// <returns></returns>
        public static List<Space> GetAll()
        {
            using (DataStoreDataContext ctx = new DataStoreDataContext())
            {
                return ctx.Spaces.ToList();
            }
        }

        /// <summary>
        /// Gets all the parking spaces available in a place
        /// </summary>
        /// <returns></returns>
        public static List<Space> GetAllByPlaceId(int id)
        {
            using (DataStoreDataContext ctx = new DataStoreDataContext())
            {
                return ctx.Spaces.Where( s => s.PlaceId.Equals(id) ).ToList();
            }
        }
    }
}
