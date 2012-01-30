using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.DirectoryServices.AccountManagement;
using System.DirectoryServices.Protocols;
using System.Net;
using System.Security.Cryptography;

namespace Sieena.Parking.API.Models
{
    public partial class Place
    {
        public static List<Place> GetAll()
        {
            using (var context = new DataStoreDataContext())
            {
                return context.Places.ToList();
            }
        }

        public static Place Get(int id)
        {
            using (var context = new DataStoreDataContext())
            {
                return context.Places.Where(p => p.PlaceId.Equals(id)).FirstOrDefault();
            }
        }

        public static Place Save(Place p) {
            using (var context = new DataStoreDataContext())
            {
                if (p.PlaceId != 0)
                {
                }
                else
                {
                    context.Places.InsertOnSubmit(p);
                }

                context.SubmitChanges();

                return p;
            }
        }

        public static bool Delete(int id)
        {
            using (var context = new DataStoreDataContext())
            {
                context.Places.DeleteOnSubmit(context.Places.Where(p => p.PlaceId.Equals(id)).First());
                context.SubmitChanges();
            }
            return true;
        }
    }

    public partial class User
    {
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

        public static User GetByEmail(string email)
        {
            email = email.ToLower().Trim();
            using (DataStoreDataContext ctx = new DataStoreDataContext())
            {
                return ctx.Users.Where(u => u.Email.Equals(email)).FirstOrDefault();
            }
        }

        public static bool Validate(string user, string password)
        {
            user = user.Trim();

            if(string.IsNullOrEmpty(user) || string.IsNullOrEmpty(password)) {
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
                    if(context.ValidateCredentials(username, password)) {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                // Could be several reasons, but go to next auth type.
            }

            return u.Password.Equals(GetSHA1(password));
        }

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
