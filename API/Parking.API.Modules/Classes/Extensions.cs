/**
 *
 * @package     Parking.API.Modules
 * @author      The JSONs
 * @copyright   2012 -
 * @license     Propietary
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nancy;

namespace Sieena.Parking.API.Modules.Classes
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
