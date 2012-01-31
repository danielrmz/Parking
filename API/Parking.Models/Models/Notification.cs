using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sieena.Parking.API.Models
{
    /// <summary>
    /// Notification to a user
    /// </summary>
    public partial class Notification
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

    }

}
