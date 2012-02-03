/**
 *
 * @package     Parking.API.Models
 * @author      The JSONs
 * @copyright   2012 -
 * @license     Propietary
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sieena.Parking.API.Models
{
    /// <summary>
    /// Represents the endpoints, the mediums by which the
    /// persons will be notified.
    /// </summary>
    public partial class Endpoint
    {
        /// <summary>
        /// Gets all the endpoints in the system.
        /// </summary>
        /// <returns></returns>
        public static List<Endpoint> GetAll()
        {
            using (DataStoreDataContext ctx = new DataStoreDataContext())
            {
                return ctx.Endpoints.ToList();
            }
        }
    }
}
