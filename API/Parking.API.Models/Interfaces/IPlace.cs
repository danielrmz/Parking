using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ComponentModel.DataAnnotations;
using System.Globalization;
using DataAnnotationsExtensions;

namespace Sieena.Parking.API.Models.Interfaces
{
    public interface IPlace : IParkingModel
    {
        [Integer]
        [Min(1)]
        [Display(Name = "Place Id")]
        int PlaceId { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Place Name")]
        string PlaceName { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Created At")]
        DateTime? CreatedAt { get; set; }
    }
}
