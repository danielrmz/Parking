using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Sieena.Parking.API.Models.Interfaces
{
    public interface IPlaceMap
    {
        int PlaceMapId { get; set; }
        int PlaceId { get; set; }
        string PlaceMapUrl { get; set; }
        int PlaceMapVersion { get; set; }
        DateTime CreatedAt { get; set; }
    }
}
