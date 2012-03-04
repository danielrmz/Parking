using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sieena.Parking.API.Models
{
    public class ParkingModel
    {
        private static DataStoreDataContext _context;

        internal static DataStoreDataContext ctx
        {
            get
            {
                if (_context == null)
                {
                    _context = new DataStoreDataContext();
                }
                else
                {
                    if (_context.Connection.State == System.Data.ConnectionState.Closed)
                    {
                        _context.Dispose();
                        _context = new DataStoreDataContext();
                    }
                }

                return _context;
            }
        }
    }
}
