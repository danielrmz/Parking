using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ComponentModel.DataAnnotations;
using System.Globalization;
using DataAnnotationsExtensions;

namespace Sieena.Parking.API.Models.Interfaces
{
    public interface ICheckin : IParkingModel
    {
        [Integer]
        [Min(1)]
        [Display(Name = "Check-in Id")]
        int CheckInId { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Start Time")]
        DateTime StartTime { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "End Time")]
        DateTime? EndTime { get; set; }

        [Required]
        [Integer]
        [Min(1)]
        [Display(Name = "Space Id")]
        int SpaceId { get; set; }

        [Required]
        [Integer]
        [Min(1)]
        [Display(Name = "User Id")]
        int UserId { get; set; }

        [Integer]
        [Min(1)]
        [Display(Name = "Reservation Id")]
        int? ReservationId { get; set; }

        [Required]
        [Integer]
        [Min(1)]
        [Display(Name = "Registered From")]
        int RegisteredFrom { get; set; }

        [Required]
        [Integer]
        [Min(1)]
        [Display(Name = "Registered By")]
        int RegisteredBy { get; set; }
    }
}
