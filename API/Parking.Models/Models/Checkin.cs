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
    /// Represents the checkin from a person to a specified place.
    /// </summary>
    public partial class Checkin :  ICheckin
    {
        /// <summary>
        /// Gets all the checkins in the system.
        /// </summary>
        /// <returns></returns>
        public static List<Checkin> GetAll()
        {
            using (EntityContext ctx = new EntityContext())
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
            using (EntityContext ctx = new EntityContext())
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
            using (EntityContext ctx = new EntityContext())
            {
                c.ValidateAndRaise();

                if (c.CheckInId == 0)
                {
                    ctx.Checkins.AddObject(c);
                }

                ctx.SaveChanges();

                return c;
            }
        }

        /// <summary>
        /// Deletes a checkin
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Checkin Delete(int id)
        {
            using (EntityContext ctx = new EntityContext())
            {
                Checkin c = ctx.Checkins.Where(cx => cx.CheckInId == id).FirstOrDefault();
                ctx.Checkins.DeleteObject(c);
                ctx.SaveChanges();
                return c;
            }
            
        }
    }
}
