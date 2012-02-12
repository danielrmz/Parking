using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sieena.Parking.API.Models.Interfaces;

namespace Sieena.Parking.API.Models
{
    public partial class UserInfo : ParkingModel, IUserInfo
    {
        
        /// <summary>
        /// Saves the user extra information
        /// </summary>
        /// <param name="u"></param>
        /// <returns></returns>
        public static UserInfo Save(UserInfo u)
        {
            u.ValidateAndRaise();
            ctx.UserInfos.InsertOnSubmit(u);
            ctx.SubmitChanges();
            return u;
        }

        public static UserInfo GetByUserId(int userId)
        {
            return ctx.UserInfos.Where(u => u.UserId.Equals(userId)).FirstOrDefault();
        }
    }
}
