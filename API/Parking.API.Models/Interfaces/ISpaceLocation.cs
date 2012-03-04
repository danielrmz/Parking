using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ComponentModel.DataAnnotations;
using System.Globalization;
using DataAnnotationsExtensions;

namespace Sieena.Parking.API.Models.Interfaces
{
    public interface ISpaceLocation : IParkingModel
    {
        [Required]
        [Integer]
        [Min(1)]
        [Display(Name = "Space Id")]
        int SpaceId { get; set; }

        [Required]
        [Numeric]
        [Display(Name = "Marker Top")]
        int MarkerTop { get; set; }

        [Required]
        [Numeric]
        [Display(Name = "Marker Left")]
        int MarkerLeft { get; set; }

        [Required]
        [Numeric]
        [Display(Name = "Marker Width")]
        int MarkerWidth { get; set; }

        [Required]
        [Numeric]
        [Display(Name = "Marker Height")]
        int MarkerHeight { get; set; }

        [Required]
        [Numeric]
        [Display(Name = "Canvas Width")]
        int CanvasWidth { get; set; }

        [Required]
        [Numeric]
        [Display(Name = "Canvas Height")]
        int CanvasHeight { get; set; }
    }
}
