using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ComponentModel.DataAnnotations;
using System.Globalization;
using DataAnnotationsExtensions;

namespace Sieena.Parking.API.Models.Interfaces
{
    public interface ISpace : IParkingModel
    {
        [Integer]
        [Min(1)]
        [Display(Name = "Space Id")]
        int SpaceId { get; set; }

        [Required]
        [Integer]
        [Min(1)]
        [Display(Name = "Place Id")]
        int PlaceId { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Alias")]
        string Alias { get; set; }

        [Required]
        [Integer]
        [Min(1)]
        [Display(Name = "Access Type Id")]
        int AccessTypeId { get; set; }

        [Required]
        [Integer]
        [Min(1)]
        [Display(Name = "Owner Id")]
        int OwnerId { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Created At")]
        DateTime CreatedAt { get; set; }

        [Range(0,1)]
        [Integer]
        [Display(Name = "Deleted")]
        bool Deleted { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "CssClass")]
        string CssClass { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Space Direction")]
        string SpaceDirection { get; set; }
    }
}
