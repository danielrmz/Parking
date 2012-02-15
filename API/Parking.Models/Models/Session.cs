using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sieena.Parking.API.Models.Interfaces;

namespace Sieena.Parking.API.Models
{
    public partial class Session : ParkingModel, ISession
    {
        /// <summary>
        /// Sets a new session object
        /// </summary>
        /// <param name="s"></param>
        public static Session Set(Session s)
        {
            s.ValidateAndRaise();

            s.SessionId = Guid.NewGuid();
            ctx.Sessions.InsertOnSubmit(s);
            ctx.SubmitChanges();

            return s;
        }

        /// <summary>
        /// Expires a session
        /// </summary>
        /// <param name="sessionId"></param>
        public static void Expire(Guid sessionId)
        {
            Session session = ctx.Sessions.Where(s => s.SessionId.Equals(sessionId)).FirstOrDefault();
            if (session != null)
            {
                ctx.Sessions.DeleteOnSubmit(session);
                ctx.SubmitChanges();
            }
        }

        /// <summary>
        /// Expires the current session id of a user
        /// </summary>
        /// <param name="sessionId"></param>
        public static void Expire(int userId)
        {
            Session session = ctx.Sessions.Where(s => s.UserId.HasValue && s.UserId.Value.Equals(userId)).FirstOrDefault();
            if (session != null)
            {
                ctx.Sessions.DeleteOnSubmit(session);
                ctx.SubmitChanges();
            }
        }

        /// <summary>
        /// Cleans all the expired sessions.
        /// </summary>
        public static void CleanAll()
        {
            ctx.Sessions.Where(s => s.ExpiresAt < DateTime.Now).ToList().ForEach(s => ctx.Sessions.DeleteOnSubmit(s));
            ctx.SubmitChanges();
        }

        /// <summary>
        /// Gets the specified session. 
        /// If it has expired it returns none.
        /// </summary>
        /// <param name="sessionId"></param>
        /// <returns></returns>
        public static Session Get(Guid sessionId)
        {
            Session sess = ctx.Sessions.Where(s => s.SessionId.Equals(sessionId)).FirstOrDefault();
            if (sess.ExpiresAt < DateTime.Now)
            {
                ctx.Sessions.DeleteOnSubmit(sess);
                ctx.SubmitChanges();
                return null;
            }
            sess.LastAccess = DateTime.Now;
            ctx.SubmitChanges();

            return sess;
        }
    }
}
