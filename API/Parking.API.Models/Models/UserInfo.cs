using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sieena.Parking.API.Models.Interfaces;

namespace Sieena.Parking.API.Models
{
    public partial class UserInfo : IUserInfo
    {
        /// <summary>
        /// Users full name
        /// </summary>
        public string FullName
        {
            get
            {
                return string.Format("{0} {1}", this.FirstName, this.LastName);
            }
        }

        /// <summary>
        /// Saves the user extra information
        /// </summary>
        /// <param name="u"></param>
        /// <returns></returns>
        public static UserInfo Save(UserInfo u)
        {
            using (EntityContext ctx = new EntityContext())
            {
                u.ValidateAndRaise();

                UserInfo existing = ctx.UserInfos.Where(ui => ui.UserId == u.UserId).FirstOrDefault();
                if (existing == null)
                {
                    ctx.UserInfos.AddObject(u);
                }
                else
                {
                    existing.ContactEmail = u.ContactEmail;
                    existing.PhoneCel = u.PhoneCel;
                    existing.PhoneHome = u.PhoneHome;
                    existing.PhoneOffice = u.PhoneOffice;
                    existing.PhoneOfficeExtension = u.PhoneOfficeExtension;
                    existing.FirstName = u.FirstName;
                    existing.LastName = u.LastName;
                    existing.Gender = u.Gender;
                    existing.NotificationsAvailability = u.NotificationsAvailability;
                    existing.Locale = u.Locale;
                }
                ctx.SaveChanges();

                // Notify connected endpoints.
                PubnubFactory.GetInstance().Publish(PubnubFactory.Channels.Users, u);
            

                return u;
            }
        }

        /// <summary>
        /// Returns the user information for the specified user id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static UserInfo GetByUserId(int userId)
        {
            using (EntityContext ctx = new EntityContext())
            {
                return ctx.UserInfos.Where(u => u.UserId.Equals(userId)).FirstOrDefault();
            }
        }

        /// <summary>
        /// Returns all the user infos available
        /// </summary>
        /// <returns></returns>
        public static List<UserInfo> GetAll()
        {
            List<User> users = User.GetAll();
            List<UserInfo> uis = new List<UserInfo>();

            users.ForEach(ux =>
            {
                uis.Add(User.GetBasicUserInformation(ux));
            });

            return uis;
        }
    }
}
