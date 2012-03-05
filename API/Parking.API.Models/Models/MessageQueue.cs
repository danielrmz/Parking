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
    /// Represents a parking space.
    /// </summary>
    public partial class MessageQueue 
    {
         
        /// <summary>
        /// Saves or updates a space
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static MessageQueue Save(MessageQueue s)
        {
            using (EntityContext ctx = new EntityContext())
            {
                ctx.MessageQueues.AddObject(s);
                
                ctx.SaveChanges();

                return s;
            }
        }

        public static List<MessageQueue> GetPending()
        {
            using (EntityContext ctx = new EntityContext())
            {
                return ctx.MessageQueues.Where( mq => mq.Delivered == false).ToList();
            }
        }

        public static void ClearIds(List<int> ids)
        {
            using (EntityContext ctx = new EntityContext())
            {
                ctx.MessageQueues.Where(mq => ids.Contains(mq.MessageId)).ToList().ForEach(mq => { mq.Delivered = true; });
                ctx.SaveChanges();
            }
        }

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
