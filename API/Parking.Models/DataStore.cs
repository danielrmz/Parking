using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sieena.Parking.API.Models
{
    public partial class Place
    {
        public static List<Place> GetAll()
        {
            using (var context = new DataStoreDataContext())
            {
                return context.Places.ToList();
            }
        }
    }
}
