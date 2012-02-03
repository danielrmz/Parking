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
    /// Roles in the system. 
    /// </summary>
    public partial class Role
    {
        /// <summary>
        /// Gets all the roles in the app
        /// </summary>
        /// <returns></returns>
        public static List<Role> GetAll()
        {
            using (DataStoreDataContext ctx = new DataStoreDataContext())
            {
                return ctx.Roles.ToList();
            }
        }
    }
}
