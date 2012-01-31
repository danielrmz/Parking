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


        /// <summary>
        /// Gets a specific instance 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Checkin Get(int id)
        {
            using (DataStoreDataContext ctx = new DataStoreDataContext())
            {
                return ctx.Checkins.Where(c => c.CheckInId.Equals(id)).FirstOrDefault();
            }
        }

        /// <summary>
        /// Saves or updates a checkin
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static Checkin Save(Checkin c)
        {
            using (DataStoreDataContext ctx = new DataStoreDataContext())
            {
                if (c.CheckInId == 0)
                {
                    ctx.Checkins.InsertOnSubmit(c);
                }

                ctx.SubmitChanges();
            }

            return c;
        }

        /// <summary>
        /// Deletes a checkin
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Checkin Delete(int id)
        {
            using (DataStoreDataContext ctx = new DataStoreDataContext())
            {
                Checkin c = Get(id);
                ctx.Checkins.DeleteOnSubmit(c);
                ctx.SubmitChanges();
                return c;
            }
        }
    }
}
