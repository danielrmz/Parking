using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ComponentModel.DataAnnotations;
using System.Globalization;
using DataAnnotationsExtensions;

namespace Sieena.Parking.API.Models.Interfaces
{
    public interface IUserEndpoint : IParkingModel
    {
        [Required]
        [Integer]
        [Min(1)]
        [Display(Name = "User Id")]
        int UserId { get; set; }

        [Required]
        [Integer]
        [Min(1)]
        [Display(Name = "Endpoint Type Id")]
        int EndpointTypeId { get; set; }

        [Required]
        [Integer]
        [Range(0,1)]
        [Display(Name = "Is Enabled")]
        bool IsEnabled { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Value")]
        string Value { get; set; }

        [Required]
        [Integer]
        [Range(0,10)]
        [Display(Name = "Priority")]
        int Priority { get; set; }
    }
}
