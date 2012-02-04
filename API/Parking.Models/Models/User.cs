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
using System.DirectoryServices.AccountManagement;
using System.Security.Cryptography;

namespace Sieena.Parking.API.Models
{
    using Interfaces;

    public partial class User : IUser
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="u"></param>
        /// <returns></returns>
        public static User SaveUser(User u)
        {
            using (DataStoreDataContext ctx = new DataStoreDataContext())
            {
                if (u.UserId == 0)
                {
                    u.Password = GetSHA1(u.Password);
                    ctx.Users.InsertOnSubmit(u);
                }
                ctx.SubmitChanges();

                return u;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static User GetByEmail(string email)
        {
            email = email.ToLower().Trim();
            using (DataStoreDataContext ctx = new DataStoreDataContext())
            {
                return ctx.Users.Where(u => u.Email.Equals(email)).FirstOrDefault();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static bool Validate(string user, string password)
        {
            user = user.Trim();

            if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(password))
            {
                // Throw custom buisness exception. 
                throw new Exception("User or password not specified");
            }

            User u = GetByEmail(user);
            if (u == null)
            {
                throw new Exception("User does not exists");
            }

            string username = user.Split('@').First();
            try
            {
                using (PrincipalContext context = new PrincipalContext(ContextType.Domain, "SIEENA"))
                {
                    if (context.ValidateCredentials(username, password))
                    {
                        return true;
                    }
                }
            }
            catch (Exception)
            {
                // Could be several reasons, but go to next auth type.
            }

            return u.Password.Equals(GetSHA1(password));
        }

        /// <summary>
        /// Obtains the SHA1 sum of a string.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private static string GetSHA1(string str)
        {
            SHA1 sha1 = SHA1Managed.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = sha1.ComputeHash(encoding.GetBytes(str));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString();
        }
    }

}
