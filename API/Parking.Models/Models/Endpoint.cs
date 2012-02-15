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
    /// Represents the endpoints, the mediums by which the
    /// persons will be notified.
    /// </summary>
    public partial class Endpoint : ParkingModel, IEndpoint
    {
        /// <summary>
        /// Gets all the endpoints in the system.
        /// </summary>
        /// <returns></returns>
        public static List<Endpoint> GetAll()
        {
            return ctx.Endpoints.ToList();
        }
    }
}
