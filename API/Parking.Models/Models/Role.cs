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
    /// Roles in the system. 
    /// </summary>
    public partial class Role : IRole
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
