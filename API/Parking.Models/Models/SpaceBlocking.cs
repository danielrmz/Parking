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
    public partial class SpaceBlocking : ISpaceBlocking
    {
        
        /// <summary>
        /// Gets all the blockings.
        /// </summary>
        /// <returns></returns>
        public static List<SpaceBlocking> GetAll()
        {
            using (EntityContext ctx = new EntityContext())
            {
                return ctx.SpaceBlockings.ToList();
            }
        }

        /// <summary>
        /// Gets all the blockings for a space id
        /// </summary>
        /// <returns></returns>
        public static List<SpaceBlocking> GetBlockingsForSpaceId(int id)
        {
            using (EntityContext ctx = new EntityContext())
            {
                return ctx.SpaceBlockings.Where(s => s.BaseSpaceId == id).ToList();
            }
        }
    }
}
