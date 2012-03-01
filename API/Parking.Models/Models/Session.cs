using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sieena.Parking.API.Models.Interfaces;
using Sieena.Parking.Common.Utils;

namespace Sieena.Parking.API.Models
{
    public partial class Session : ISession
    {
        /// <summary>
        /// Sets a new session object
        /// </summary>
        /// <param name="s"></param>
        public static Session Set(Session s)
        {
            using (EntityContext ctx = new EntityContext())
            {
                s.ValidateAndRaise();

                s.SessionId = Guid.NewGuid();
                ctx.Sessions.AddObject(s);
                ctx.SaveChanges();

                return s;
            }
        }

        /// <summary>
        /// Sets a new session object
        /// </summary>
        /// <param name="s"></param>
        public static Session Set(int userId, TimeSpan timeOut, bool rememberMe)
        {
            using (EntityContext ctx = new EntityContext())
            {
                Session s = new Session()
                {
                    UserId = userId,
                    CreatedAt = DateTime.Now,
                    ExpiresAt = rememberMe ? DateTime.MaxValue : DateTime.Now.ToCommonTime().Add(timeOut),
                    LastAccess = DateTime.Now.ToUniversalTime(),
                    Data = string.Empty
                };

                s.ValidateAndRaise();

                s.SessionId = Guid.NewGuid();
                ctx.Sessions.AddObject(s);
                ctx.SaveChanges();
                return s;
            }
        }

        /// <summary>
        /// Expires a session
        /// </summary>
        /// <param name="sessionId"></param>
        public static void Expire(Guid sessionId)
        {
            using (EntityContext ctx = new EntityContext())
            {
                Session session = ctx.Sessions.Where(s => s.SessionId.Equals(sessionId)).FirstOrDefault();
                if (session != null)
                {
                    ctx.Sessions.DeleteObject(session);
                    ctx.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Expires the current session id of a user
        /// </summary>
        /// <param name="sessionId"></param>
        public static void Expire(int userId)
        {
            using (EntityContext ctx = new EntityContext())
            {
                Session session = ctx.Sessions.Where(s => s.UserId.HasValue && s.UserId.Value.Equals(userId)).FirstOrDefault();
                if (session != null)
                {
                    ctx.Sessions.DeleteObject(session);
                    ctx.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Cleans all the expired sessions.
        /// </summary>
        public static void CleanAll()
        {
            using (EntityContext ctx = new EntityContext())
            {
                ctx.Sessions.Where(s => s.ExpiresAt < DateTime.Now.ToCommonTime() && s.ExpiresAt != DateTime.MaxValue).ToList().ForEach(s => ctx.Sessions.DeleteObject(s));
                ctx.SaveChanges();
            }
        }

        /// <summary>
        /// Gets the specified session. 
        /// If it has expired it returns none.
        /// </summary>
        /// <param name="sessionId"></param>
        /// <returns></returns>
        public static Session Get(Guid sessionId)
        {
            using (EntityContext ctx = new EntityContext())
            {
                Session sess = ctx.Sessions.Where(s => s.SessionId.Equals(sessionId)).FirstOrDefault();
                if (sess == null)
                {
                    return null;
                }

                if (sess.ExpiresAt < DateTime.Now.ToCommonTime() && sess.ExpiresAt != DateTime.MaxValue)
                {
                    ctx.Sessions.DeleteObject(sess);
                    ctx.SaveChanges();
                    return null;
                }
                sess.LastAccess = DateTime.Now.ToCommonTime();
                ctx.SaveChanges();

                return sess;
            }
        }

        /// <summary>
        /// Gets the specified session. 
        /// If it has expired it returns none.
        /// </summary>
        /// <param name="sessionId"></param>
        /// <returns></returns>
        public static Session Get(int userId)
        {
            using (EntityContext ctx = new EntityContext())
            {
                Session sess = ctx.Sessions.Where(s => s.UserId.HasValue && s.UserId.Value.Equals(userId)).FirstOrDefault();
                if (sess == null)
                {
                    return null;
                }

                if (sess.ExpiresAt < DateTime.Now.ToCommonTime() && sess.ExpiresAt != DateTime.MaxValue)
                {
                    ctx.Sessions.DeleteObject(sess);
                    ctx.SaveChanges();
                    return null;
                }
                sess.LastAccess = DateTime.Now.ToCommonTime();
                ctx.SaveChanges();

                return sess;
            }
        }
    }
}
