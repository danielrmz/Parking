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
    /// Notification to a user
    /// </summary>
    public partial class Notification :  INotification
    {
        /// <summary>
        /// Gets all the notifications. 
        /// It is marked as internal a person should not have
        /// access to all of these. It should be done through the appropiate method
        /// </summary>
        /// <returns></returns>
        internal static List<Notification> GetAll()
        {
            using (EntityContext ctx = new EntityContext())
            {
                return ctx.Notifications.ToList();
            }
        }

        /// <summary>
        /// Gets the notifications for the user id.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="lastAmount"></param>
        /// <returns></returns>
        public static List<Notification> GetLastByUserId(int userId, int lastAmount)
        {
            using (EntityContext ctx = new EntityContext())
            {
                IEnumerable<Notification> nots = ctx.Notifications
                                  .Where(n => n.UserId.Equals(userId))
                                  .OrderByDescending(n => n.CreatedAt);

                if (lastAmount > 0)
                {
                    nots = nots.Take(lastAmount);
                }

                return nots.ToList();
            }
        }


        /// <summary>
        /// Gets a specific instance 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Notification Get(int id)
        {
            using (EntityContext ctx = new EntityContext())
            {
                return ctx.Notifications.Where(n => n.NotificationId.Equals(id)).FirstOrDefault();
            }
        }

        /// <summary>
        /// Saves or updates a notification
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static Notification Save(Notification n)
        {
            using (EntityContext ctx = new EntityContext())
            {
                n.ValidateAndRaise();

                if (n.NotificationId == 0)
                {
                    ctx.Notifications.AddObject(n);
                }

                ctx.SaveChanges();

                return n;
            }
        }

        /// <summary>
        /// Deletes a Notification
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Notification Delete(int id)
        {
            using (EntityContext ctx = new EntityContext())
            {
                Notification n = ctx.Notifications.Where(nx => nx.NotificationId == id).FirstOrDefault();
                ctx.Notifications.DeleteObject(n);
                ctx.SaveChanges();
                return n;
            }
        }

    }

}
