using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ComponentModel.DataAnnotations;
using System.Globalization;
using DataAnnotationsExtensions;

namespace Sieena.Parking.API.Models.Interfaces
{
    public interface INotification : IParkingModel
    {
        [Integer]
        [Min(1)]
        [Display(Name = "Notification Id")]
        int NotificationId { get; set; }

        [Required]
        [Integer]
        [Min(1)]
        [Display(Name = "User Id")]
        int UserId { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Message")]
        string Message { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Created At")]
        DateTime CreatedAt { get; set; }

        [Required]
        [Integer]
        [Min(1)]
        [Display(Name = "Created By")]
        int CreatedBy { get; set; }

        [Integer]
        [Min(1)]
        [Display(Name = "Check-in Id")]
        int? CheckInId { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Read At")]
        DateTime? ReadAt { get; set; }
    }
}
