using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Sieena.Parking.API.Models.Interfaces
{
    public interface INotification
    {
        int NotificationId { get; set; }
        int UserId { get; set; }
        string Message { get; set; }
        DateTime CreatedAt { get; set; }
        int CreatedBy { get; set; }
        int? CheckInId { get; set; }
        DateTime? ReadAt { get; set; }
    }
}
