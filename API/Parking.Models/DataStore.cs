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

        public static Place Get(int id)
        {
            using (var context = new DataStoreDataContext())
            {
                return context.Places.Where(p => p.PlaceId.Equals(id)).FirstOrDefault();
            }
        }

        public static Place Save(Place p) {
            using (var context = new DataStoreDataContext())
            {
                if (p.PlaceId != 0)
                {
                }
                else
                {
                    context.Places.InsertOnSubmit(p);
                }

                context.SubmitChanges();

                return p;
            }
        }

        public static bool Delete(int id)
        {
            using (var context = new DataStoreDataContext())
            {
                context.Places.DeleteOnSubmit(context.Places.Where(p => p.PlaceId.Equals(id)).First());
                context.SubmitChanges();
            }
            return true;
        }
    }
}
