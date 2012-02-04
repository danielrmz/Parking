using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Sieena.Parking.API.Models.Interfaces
{
    public interface ICheckin
    {
        int CheckInId { get; set; }
        DateTime StartTime { get; set; }
        DateTime? EndTime { get; set; }
        int SpaceId { get; set; }
        int UserId { get; set; }
        int? ReservationId { get; set; }
        int RegisteredFrom { get; set; }
        int RegisteredBy { get; set; }
    }
}
