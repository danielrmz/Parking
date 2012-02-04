using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Sieena.Parking.API.Models.Interfaces
{
    public interface ISpaceLocation 
    {
        int SpaceId { get; set; }
        int MarkerTop { get; set; }
        int MarkerLeft { get; set; }
        int MarkerWidth { get; set; }
        int MarkerHeight { get; set; }
        int CanvasWidth { get; set; }
        int CanvasHeight { get; set; }
    }
}
