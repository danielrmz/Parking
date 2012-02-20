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
        /// Saves the user extra information
        /// </summary>
        /// <param name="u"></param>
        /// <returns></returns>
        public static UserInfo Save(UserInfo u)
        {
            using (EntityContext ctx = new EntityContext())
            {
                u.ValidateAndRaise();
                ctx.UserInfos.AddObject(u);
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
