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
using System.ComponentModel.DataAnnotations;
using Sieena.Parking.API.Models.Interfaces;
using Sieena.Parking.API.Models.Exceptions;

namespace Sieena.Parking.API.Models
{
    internal static class Extensions
    {
        /// <summary>
        /// Validates a model against its data annotation properties.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="o"></param>
        /// <returns></returns>
        public static List<ValidationResult> Validate<T>(this T o) where T : IParkingModel, new()
        {
            var context = new ValidationContext(o, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(o, context, results);
            
            return results;
        }

        /// <summary>
        /// Validates and raises a validation exception if an error is found.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="o"></param>
        /// <returns></returns>
        public static bool ValidateAndRaise<T>(this T o) where T : IParkingModel, new()
        {
            var context = new ValidationContext(o, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(o, context, results);

            List<ModelValidationError> mvr = results.Select(vr => new ModelValidationError(vr)).ToList();

            if (!isValid)
            {
                throw new ModelValidationException(mvr);
            }

            return isValid;
        }
    }
}
