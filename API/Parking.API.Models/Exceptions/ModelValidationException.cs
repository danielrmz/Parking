using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Sieena.Parking.API.Models.Exceptions
{
    /// <summary>
    /// Exception that represents that a validation error of a model's properties
    /// has been raised.
    /// </summary>
    public class ModelValidationException : Exception
    {
        public ModelValidationException(List<ModelValidationError> results) {
            this.Errors = results;    
        }

        public ModelValidationException(List<ModelValidationError> results, Exception innerException) : base(string.Empty, innerException)
        {
            this.Errors = results;
        }

        public List<ModelValidationError> Errors { get; private set; }
    }
}
