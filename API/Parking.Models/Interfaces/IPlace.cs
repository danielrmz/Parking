using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Sieena.Parking.API.Models.Interfaces
{
    public interface IPlace 
    {
        int PlaceId { get; set; }
        string PlaceName { get; set; }
        DateTime? CreatedAt { get; set; }
    }
}
