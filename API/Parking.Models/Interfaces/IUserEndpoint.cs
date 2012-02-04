using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Sieena.Parking.API.Models.Interfaces
{
    public interface IUserEndpoint
    {
        int UserId { get; set; }
        int EndpointTypeId { get; set; }
        bool IsEnabled { get; set; }
        string Value { get; set; }
        int Priority { get; set; }
    }
}
