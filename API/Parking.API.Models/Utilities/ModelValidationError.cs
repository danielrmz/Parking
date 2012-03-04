using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Sieena.Parking.API.Models
{
    public class ModelValidationError
    {
        public ModelValidationError(ValidationResult result)
        {
            this.ErrorMessage = result.ErrorMessage;
            this.MemberNames = result.MemberNames.ToList();
        }

        public ModelValidationError(string errorMessage, List<string> memberNames)
        {
            this.ErrorMessage = errorMessage;
            this.MemberNames = memberNames;
        }

        public string ErrorMessage { get; private set; }
        public List<string> MemberNames { get; private set; }
    }
}
