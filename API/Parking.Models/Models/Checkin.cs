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
    using Sieena.Parking.API.Models.Views;

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
        /// Checks out the current active user if exists in that space
        /// </summary>
        /// <param name="spaceId"></param>
        /// <returns></returns>
        public static void ClearSpace(int spaceId)
        {
            using (EntityContext ctx = new EntityContext())
            {
                var chin = ctx.Checkins.Where(ch => !ch.EndTime.HasValue && ch.SpaceId == spaceId).FirstOrDefault();
                if (chin != null)
                {
                    CheckOut(chin.CheckInId);
                }
            }
        }

        /// <summary>
        /// Checks out the current active user if exists in that space
        /// </summary>
        /// <param name="spaceId"></param>
        /// <returns></returns>
        public static void ClearUser(int userId)
        {
            using (EntityContext ctx = new EntityContext())
            {
                var chin = ctx.Checkins.Where(ch => !ch.EndTime.HasValue && ch.UserId == userId).FirstOrDefault();
                if (chin != null)
                {
                    CheckOut(chin.CheckInId);
                }
            }
        }


        /// <summary>
        /// Saves or updates a checkin
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static Checkin CheckIn(Checkin c)
        {
            using (EntityContext ctx = new EntityContext())
            {
                c.ValidateAndRaise();

                // Validate user has only one active checkin. 

                c.CheckInId = 0;

                ctx.Checkins.AddObject(c);

                ctx.SaveChanges();

                // Notify users.
                Pubnub nub = PubnubFactory.GetInstance();
                nub.Publish(PubnubFactory.Channels.CheckinHistory, c);

                return c;
            }
        }

        /// <summary>
        /// Checks out a current checkin.
        /// </summary>
        /// <param name="checkinId"></param>
        /// <returns></returns>
        public static Checkin CheckOut(int checkinId)
        {
            using (EntityContext ctx = new EntityContext())
            {
                Checkin c = ctx.Checkins.Where(ch => ch.CheckInId == checkinId).FirstOrDefault();
                if (c == null)
                {
                    return null;
                }

                c.EndTime = DateTime.Now;
                ctx.SaveChanges();

                // Notify users
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
         

        /// <summary>
        /// Returns checkin information for the last X amount of recent checkins.
        /// </summary>
        /// <param name="amount"></param>
        /// <returns></returns>
        public static List<Checkin> GetLast(int amount)
        {
            using (EntityContext ctx = new EntityContext())
            {
                return  ctx.Checkins.OrderByDescending(c => c.EndTime.HasValue ? c.EndTime.Value : c.StartTime)
                                   .Take(amount)
                                   .ToList();
            }
        }

        /// <summary>
        /// Returns the current checkins
        /// </summary>
        /// <param name="amount"></param>
        /// <returns></returns>
        public static List<Checkin> GetCurrent()
        {
            using (EntityContext ctx = new EntityContext())
            {
                return ctx.Checkins
                          .Where(c => !c.EndTime.HasValue)
                          .OrderByDescending(c => c.StartTime)
                          .ToList();
            }
        }
    }
}
