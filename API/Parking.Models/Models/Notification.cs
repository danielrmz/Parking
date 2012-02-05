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
    public partial class Notification : INotification
    {
        /// <summary>
        /// Gets all the notifications. 
        /// It is marked as internal a person should not have
        /// access to all of these. It should be done through the appropiate method
        /// </summary>
        /// <returns></returns>
        internal static List<Notification> GetAll()
        {
            using (var context = new DataStoreDataContext())
            {
                return context.Notifications.ToList();
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
            using (var context = new DataStoreDataContext())
            {
                IEnumerable<Notification> nots = context.Notifications
                              .Where(n => n.UserId.Equals(userId))
                              .OrderByDescending(n => n.CreatedAt);

                if(lastAmount > 0) {
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
            using (DataStoreDataContext ctx = new DataStoreDataContext())
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
            using (DataStoreDataContext ctx = new DataStoreDataContext())
            {
                n.ValidateAndRaise();

                if (n.NotificationId == 0)
                {
                    ctx.Notifications.InsertOnSubmit(n);
                }

                ctx.SubmitChanges();
            }

            return n;
        }

        /// <summary>
        /// Deletes a Notification
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Notification Delete(int id)
        {
            using (DataStoreDataContext ctx = new DataStoreDataContext())
            {
                Notification n = Get(id);
                ctx.Notifications.DeleteOnSubmit(n);
                ctx.SubmitChanges();
                return n;
            }
        }

    }

}
