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
                    ctx.UserInfos.Attach(u); 
                }
                ctx.SaveChanges();
                return u;
            }
        }

        public static UserInfo GetByUserId(int userId)
        {
            using (EntityContext ctx = new EntityContext())
            {
                return ctx.UserInfos.Where(u => u.UserId.Equals(userId)).FirstOrDefault();
            }
        }
    }
}
