/**
 *
 * @package     Parking.API.Modules
 * @author      The JSONs
 * @copyright   2012 - 20XX
 * @license     Propietary
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nancy;

namespace Sieena.Parking.API.Modules.Classes
{
    /// <summary>
    /// Internal extensions specific to the Endpoint Project
    /// </summary>
    internal static class Extensions
    {
        /// <summary>
        /// Fills the specified instance type with the contents of the dynamic dictionary
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="p"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Fills the specified instance type with the contents of the dynamic dictionary
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="p"></param>
        /// <returns></returns>
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
