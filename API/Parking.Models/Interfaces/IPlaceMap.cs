using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ComponentModel.DataAnnotations;
using System.Globalization;
using DataAnnotationsExtensions;

namespace Sieena.Parking.API.Models.Interfaces
{
    public interface IPlaceMap : IParkingModel
    {
        [Integer]
        [Min(1)]
        [Display(Name = "Place Map Id")]
        int PlaceMapId { get; set; }

        [Required]
        [Integer]
        [Min(1)]
        [Display(Name = "Place Id")]
        int PlaceId { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Map Url")]
        string PlaceMapUrl { get; set; }

        [Required]
        [Integer]
        [Min(1)]
        [Display(Name = "Map Version")]
        int PlaceMapVersion { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Created At")]
        DateTime CreatedAt { get; set; }
    }
}
