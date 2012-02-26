using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ComponentModel.DataAnnotations;
using System.Globalization;
using DataAnnotationsExtensions;

namespace Sieena.Parking.API.Models.Interfaces
{
    public interface ISpaceBlocking : IParkingModel
    {
        [Required]
        [Integer]
        [Min(1)]
        [Display(Name = "Space Id")]
        int BaseSpaceId { get; set; }

        [Required]
        [Integer]
        [Min(1)]
        [Display(Name = "Blocking Space Id")]
        int BlockingSpaceId { get; set; }
         
    }
}
