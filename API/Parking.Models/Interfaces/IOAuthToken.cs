using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Sieena.Parking.API.Models.Interfaces
{
    public interface IOAuthToken
    {
        int UserId { get; set; }
        int ProviderId { get; set; }
        string ConsumerKey { get; set; }
        string ConsumerSecret { get; set; }
        DateTime CreatedAt { get; set; }
    }
}
