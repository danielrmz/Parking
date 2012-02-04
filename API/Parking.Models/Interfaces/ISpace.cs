using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Sieena.Parking.API.Models.Interfaces
{
    public interface ISpace
    {
        int SpaceId { get; set; }
        int PlaceId { get; set; }
        string Alias { get; set; }
        int AccessTypeId { get; set; }
        int OwnerId { get; set; }
        DateTime CreatedAt { get; set; }
        bool Deleted { get; set; }
    }
}
