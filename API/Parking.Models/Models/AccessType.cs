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
    /// AccessTypes data methods.
    /// </summary>
    public partial class AccessType : IAccessType
    {
        private static DataStoreDataContext ctx = new DataStoreDataContext();

        /// <summary>
        /// Gets all the access types 
        /// </summary>
        /// <returns></returns>
        public static List<AccessType> GetAll()
        {
            return ctx.AccessTypes.ToList();
        }
    }
}
