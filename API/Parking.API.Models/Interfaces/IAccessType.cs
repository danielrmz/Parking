using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ComponentModel.DataAnnotations;
using System.Globalization;
using DataAnnotationsExtensions;

namespace Sieena.Parking.API.Models.Interfaces
{
    public interface IAccessType : IParkingModel
    {
        [Integer]
        [Min(1)]
        [Display(Name = "Access Type Id")]
        int AccessTypeId { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Access Type")]
        string AccessTypeName { get; set; }
    }
}
