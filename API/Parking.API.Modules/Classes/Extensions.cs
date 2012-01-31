using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nancy;

namespace Sieena.Parking.API.Modules
{
    internal static class Extensions
    {
        public static T Fill<T>(this DynamicDictionary p) where T : class, new()
        {
            Type t = typeof(T);
            T instance = new T();
            t.GetProperties().ToList().ForEach(pi => {
                if (p.Contains(pi.Name))
                {
                    pi.SetValue(instance, p[pi.Name], new object[]{});
                }
            });

            return instance;
        }

        public static T Fill<T>(this DynamicDictionary p, T instance) where T : class
        {
            Type t = typeof(T);
            t.GetProperties().ToList().ForEach(pi =>
            {
                if (p.Contains(pi.Name))
                {
                    pi.SetValue(instance, p[pi.Name], new object[] { });
                }
            });

            return instance;
        }
    }
}
