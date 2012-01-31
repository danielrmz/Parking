using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sieena.Parking.API.Models
{
    /// <summary>
    /// Represents the checkin from a person to a specified place.
    /// </summary>
    public partial class Checkin
    {
        /// <summary>
        /// Gets all the checkins in the system.
        /// </summary>
        /// <returns></returns>
        public static List<Checkin> GetAll()
        {
            using (DataStoreDataContext ctx = new DataStoreDataContext())
            {
                return ctx.Checkins.OrderByDescending(c => c.CheckInId).ToList();
            }
        }
    }
}
