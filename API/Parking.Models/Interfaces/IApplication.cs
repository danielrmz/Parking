using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Sieena.Parking.API.Models.Interfaces
{
    public interface IApplication
    {
        int ApplicationId { get; set; }
        string ApplicationName { get; set; }
        string ApplicationConsumerKey { get; set; }
        string ApplicationConsumerToken { get; set; }
        DateTime? CreatedAt { get; set; }
    }
}
