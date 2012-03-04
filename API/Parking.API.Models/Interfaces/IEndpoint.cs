using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ComponentModel.DataAnnotations;
using System.Globalization;
using DataAnnotationsExtensions;

namespace Sieena.Parking.API.Models.Interfaces
{
    public interface IEndpoint : IParkingModel
    {
        [Integer]
        [Min(1)]
        [Display(Name = "Endpoint Id")]
        int EndpointTypeId { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Endpoint Name")]
        string EndpointName { get; set; }
    }
}
