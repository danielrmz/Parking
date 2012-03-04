using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ComponentModel.DataAnnotations;
using System.Globalization;
using DataAnnotationsExtensions;

namespace Sieena.Parking.API.Models.Interfaces
{
    public interface IOAuthProvider : IParkingModel
    {
        [Min(1)]
        [Display(Name = "Provider Id")]
        int ProviderId { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Provider Name")]
        string ProviderName { get; set; }
    }
}
