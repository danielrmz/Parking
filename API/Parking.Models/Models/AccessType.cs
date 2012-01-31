using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sieena.Parking.API.Models
{
    /// <summary>
    /// AccessTypes data methods.
    /// </summary>
    public partial class AccessType
    {
        /// <summary>
        /// Gets all the access types 
        /// </summary>
        /// <returns></returns>
        public static List<AccessType> GetAll()
        {
            using (DataStoreDataContext ctx = new DataStoreDataContext())
            {
                return ctx.AccessTypes.ToList();
            }
        }
    }
}
