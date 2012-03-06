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
    using Sieena.Parking.Common.Utils;
    using Sieena.Parking.API.Models.Views;
    using Sieena.Parking.API.Models.Exceptions;
    using i18n = Sieena.Parking.Common.Resources.UI;

    /// <summary>
    /// Represents the checkin from a person to a specified place.
    /// </summary>
    public partial class Checkin : ICheckin
    {
        #region Getters
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
        /// Gets the last checkin the user has made.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static Checkin GetLastForUser(int userId)
        {
            using (EntityContext ctx = new EntityContext())
            {
                return ctx.Checkins.Where(c => c.UserId == userId).OrderByDescending(c => c.CheckInId).Take(1).FirstOrDefault();
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
                return ctx.Checkins.OrderByDescending(c => c.EndTime.HasValue ? c.EndTime.Value : c.StartTime)
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

        /// <summary>
        /// Returns checkin information for the last X amount of recent checkins.
        /// </summary>
        /// <param name="amount"></param>
        /// <returns></returns>
        public static List<CheckinNotification> GetNotificationStream(int amount)
        {
            using (EntityContext ctx = new EntityContext())
            {
                List<CheckinNotification> final = new List<CheckinNotification>();

                List<CheckinNotification> nots = ctx.Checkins.OrderByDescending(c => c.CheckInId)
                                   .Take(amount)
                                   .ToList()
                                   .Select(c => new CheckinNotification(c, NotificationType.Checkin))
                                   .ToList();

                nots.ForEach(n => {
                    final.Add(n);
                    if (n.EndTime.HasValue)
                    {
                        final.Add(new CheckinNotification(n, NotificationType.Checkout));
                    }
                });

                return final.OrderByDescending(n => n.LastModified).Take(amount).ToList();
            }
        }
    
        #endregion

        #region Checkin Management

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
      
        #endregion

        #region Core

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
                if (ctx.Checkins.Where(ci => !ci.EndTime.HasValue && ci.UserId == c.UserId).Any())
                {
                    throw new CheckinExistsException(i18n.API_ErrorCheckinExists);
                }

                // Validate space is not being used
                if (ctx.Checkins.Where(ci => ci.SpaceId == c.SpaceId && !ci.EndTime.HasValue).Any())
                {
                    throw new CheckinExistsException(i18n.API_ErrorSpaceUsed);
                }

                c.CheckInId = 0;
                c.StartTime = c.StartTime.ToCommonTime();
                c.EndTime = null;

                ctx.Checkins.AddObject(c);

                ctx.SaveChanges();

                // Notify users.
                Notify(NotificationType.Checkin, c);

                return c;
            }
        }

        /// <summary>
        /// Checks out a user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static Checkin CheckOutByUserId(int userId)
        {
            Checkin c = Checkin.GetLastForUser(userId);
            if (c == null)
            {
                return null;
            }

            if (c.EndTime == null)
            {
                return CheckOut(c.CheckInId);
            }
            return c;
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


                // Validate if users are blocking you.

                c.EndTime = DateTime.Now.ToCommonTime();
                ctx.SaveChanges();

                // Notify users
                Notify(NotificationType.Checkout, c);

                return c;
            }
        }

        public List<Checkin> GetCheckinsIBlock()
        {
            List<Checkin> checkins = new List<Checkin>();

            using (EntityContext ctx = new EntityContext())
            {
                List<int> blockins = ctx.SpaceBlockings.Where(sb => sb.BaseSpaceId == this.SpaceId).Select(sb => sb.BlockingSpaceId).ToList();
                checkins = ctx.Checkins.Where(c => !c.EndTime.HasValue && blockins.Contains(c.SpaceId) && c.CheckInId != this.CheckInId).ToList();
            }

            return checkins;
        }

        #endregion

        #region Notifications
        
        /// <summary>
        /// Notification types
        /// </summary>
        public enum NotificationType
        {
            Checkin,
            Checkout
        }

        /// <summary>
        /// Sends messages based on the notification type.
        /// </summary>
        /// <param name="notify"></param>
        /// <param name="checkin"></param>
        public static void Notify(NotificationType notify, Checkin checkin) 
        {
            Pubnub nub = PubnubFactory.GetInstance();
            nub.Publish(PubnubFactory.Channels.CheckinHistory, new CheckinNotification(checkin, notify));
            nub.Publish(PubnubFactory.Channels.CheckinCurrent, checkin);

            switch (notify)
            {
                case NotificationType.Checkin: 
                    // Insert custom actions based on the type here.

                    break;
                case NotificationType.Checkout:
                    // Insert custom actions based on the type here.
                    NotifyBlockingUsers(checkin, checkin.GetCheckinsIBlock());
                    break;
            }
        }

        /// <summary>
        /// Notifies the users that are blocking the specified checkin/space. 
        /// At the moment it sends IM and Email regardless of the users' preferences
        /// </summary>
        /// <param name="baseCheckin"></param>
        /// <param name="checkins"></param>
        private static void NotifyBlockingUsers(Checkin baseCheckin, List<Checkin> checkins) {
            List<UserInfo> userInfos = new List<UserInfo>();
            List<User> users = new List<User>();
            List<int> userIds = checkins.Select(c => c.UserId).ToList();
            UserInfo requestingUser;
            using (EntityContext ctx = new EntityContext())
            {
                requestingUser = ctx.UserInfos.Where(ui => ui.UserId == baseCheckin.UserId).FirstOrDefault();

                userInfos = ctx.UserInfos.Where(ui => userIds.Contains(ui.UserId)).ToList();
                users = ctx.Users.Where(ui => userIds.Contains(ui.UserId)).ToList();
            }

            Pubnub pub = PubnubFactory.GetInstance();

            users.ForEach(user =>
            {
                // Notify via email
                EmailerFactory.SendMail(user.Email, i18n.Notification_EmailTitle, string.Format(i18n.Notification_EmailMessage, requestingUser.FullName));

                // Notify via UI (if the user has it open)
                pub.Publish(PubnubFactory.Channels.NotifyBlock, new { UserId = user.UserId, RequestingUser = requestingUser.UserId });
            
            });

            userInfos.ForEach(ui => {
                if (!string.IsNullOrEmpty(ui.ContactEmail))
                {
                    // Notify via IM
                    MessageQueue.Save(new MessageQueue() { To = ui.ContactEmail, Text = string.Format(i18n.Notification_IMMessage, requestingUser.FullName) });
                }
            });


            TropoFactory.CreateSession();
        }


        #endregion
    }
}
