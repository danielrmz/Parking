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
    using Parking.API.Models.Exceptions;
    using System.DirectoryServices;

    public partial class User : IUser
    {
        private static DataStoreDataContext ctx = new DataStoreDataContext();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="u"></param>
        /// <returns></returns>
        public static User SaveUser(User u)
        {
            u.Email = u.Email.ToLower();
            u.ValidateAndRaise();

            if (u.UserId == 0)
            {
                u.Password = GetSHA1(u.Password);
                ctx.Users.InsertOnSubmit(u);
            }
            else
            {
                ctx.Users.Attach(u, true);
            }

            ctx.SubmitChanges();

            return u;
        }

        public static void AddRoles(User u, List<Role> r)
        {
            foreach (Role ri in r)
            {
                Models.UserRole ur = new Models.UserRole()
                {
                    UserId = u.UserId,
                    RoleId = ri.RoleId
                };
                if (ctx.UserRoles.Where(uro => uro.UserId.Equals(u.UserId) && uro.RoleId.Equals(ri.RoleId)).Any())
                {
                }
                else
                {
                    ctx.UserRoles.InsertOnSubmit(ur);
                }
            }
            ctx.SubmitChanges();
        }

        public static void DeleteRoles(User u, List<Role> r)
        {
            ctx.UserRoles.Where(ur => ur.UserId.Equals(u.UserId) 
                                        && r.Where( ri => ri.RoleId.Equals(ur.RoleId)).Any()
                                    ).ToList().ForEach(ur => ctx.UserRoles.DeleteOnSubmit(ur));
            ctx.SubmitChanges();
        }

        /// <summary>
        /// Marks a user as deleted.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static bool DeleteUser(string email)
        {
            User u = GetByEmail(email);
            if (u != null)
            {
                u.IsActive = false;
            }

            ctx.Users.Attach(u, true);
            ctx.SubmitChanges();
            
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static User GetByEmail(string email)
        {
            email = email.ToLower().Trim();
            return ctx.Users.Where(u => u.Email.Equals(email)).FirstOrDefault();
        }

        /// <summary>
        /// Gets a user by its id
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static User GetById(int id)
        { 
            return ctx.Users.Where(u => u.UserId.Equals(id)).FirstOrDefault();
        }

        /// <summary>
        /// Returns all the users available in the system.
        /// </summary>
        /// <returns></returns>
        public static List<User> GetAll()
        {
            return ctx.Users.ToList();
        }

        /// <summary>
        /// Finds a set of users based on an email 
        /// </summary>
        /// <param name="emailToMatch"></param>
        /// <returns></returns>
        public static List<User> FindByEmail(string emailToMatch)
        {
            emailToMatch = emailToMatch.ToLower().Trim();
            return ctx.Users.Where(u => u.Email.Contains(emailToMatch)).ToList();
        }

        /// <summary>
        /// Verifies the credentials via active directory.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static bool VerifyCredentialsByAD(string user, string password)
        {
            user = user.Trim();

            if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(password))
            {
                // Throw custom buisness exception. 
                throw new APIException("User or password not specified");
            }

            string username = user.Split('@').First();
            try
            {
                using (PrincipalContext context = new PrincipalContext(ContextType.Domain, "SIEENA"))
                {
                    if (context.ValidateCredentials(username, password))
                    {
                        DirectorySearcher dSearch = new DirectorySearcher(new DirectoryEntry("ldap://sieena"));
                        dSearch.Filter = string.Format("(&(objectClass=user)(l={0}))", username);
                        SearchResult sr = dSearch.FindOne();
                        string givenName = GetADProperty(sr, "givenName");

                        return true;
                    }
                }
            }
            catch (Exception e)
            {
                // Could be several reasons, but go to next auth type.
                int i = 0;
            }

            return false;

        }

        private static string GetADProperty(SearchResult result, string propertyName)
        {
            if (result.Path.Contains(propertyName))
            {
                return result.Properties[propertyName][0].ToString();
            }
            else
            {
                return string.Empty;
            }
        }
         

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static bool VerifyCredentials(string user, string password)
        {
            user = user.Trim();

            if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(password))
            {
                // Throw custom buisness exception. 
                throw new APIException("User or password not specified");
            }


            User u = GetByEmail(user);

            if (u == null)
            {
                if (VerifyCredentialsByAD(user, password))
                {
                    u = API.Models.User.SaveUser(new API.Models.User()
                    {
                        Email = string.Format("{0}@sieena.com", user),
                        CreatedAt = DateTime.Now,
                        IsActive = true,
                        Password = password
                    });
                }
            }
            else if (u != null)
            {
                if (!u.Password.Equals(GetSHA1(password)))
                {
                    u = null;
                }
            }

            return u != null;
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
