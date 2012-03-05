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

    /// <summary>
    /// Message Queue for the Tropo Service
    /// </summary>
    public partial class MessageQueue 
    {
         
        /// <summary>
        /// Saves a new Message
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static MessageQueue Save(MessageQueue s)
        {
            using (EntityContext ctx = new EntityContext())
            {
                s.CreatedAt = DateTime.Now;
                ctx.MessageQueues.AddObject(s);
                
                ctx.SaveChanges();

                return s;
            }
        }

        /// <summary>
        /// Gets the messages pending to be processed
        /// </summary>
        /// <returns></returns>
        public static List<MessageQueue> GetPending()
        {
            using (EntityContext ctx = new EntityContext())
            {
                return ctx.MessageQueues.Where( mq => mq.Delivered == false || !mq.Delivered.HasValue).ToList();
            }
        }

        /// <summary>
        /// Marks the messages as delivered.
        /// </summary>
        /// <param name="ids"></param>
        public static void ClearIds(List<int> ids)
        {
            using (EntityContext ctx = new EntityContext())
            {
                ctx.MessageQueues.Where(mq => ids.Contains(mq.MessageId)).ToList().ForEach(mq => { mq.Delivered = true; });
                ctx.SaveChanges();
            }
        }

        /// <summary>
        /// Returns the specified messages as a dictionary
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public static Dictionary<string, List<string>> GetPendingAsDictionary(List<int> ids)
        {
            Dictionary<string, List<string>> dic = new Dictionary<string, List<string>>();

            using (EntityContext ctx = new EntityContext())
            {
                ctx.MessageQueues.Where(mq => ids.Contains(mq.MessageId)).ToList()
                    .ForEach(mq => {
                        if (dic.ContainsKey(mq.To))
                        {
                            dic[mq.To].Add(mq.Text);
                        }
                        else
                        {
                            dic.Add(mq.To, new List<string>() { mq.Text });
                        }
                    });
                return dic;
            }
        }

    }
}
