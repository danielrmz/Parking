using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ComponentModel.DataAnnotations;
using System.Globalization;
using DataAnnotationsExtensions;

namespace Sieena.Parking.API.Models.Interfaces
{
    public interface IOAuthToken : IParkingModel
    {
        [Required]
        [Integer]
        [Min(1)]
        [Display(Name = "User Id")]
        int UserId { get; set; }

        [Required]
        [Integer]
        [Min(1)]
        [Display(Name = "Provider Id")]
        int ProviderId { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Consumer Key")]
        string ConsumerKey { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Consumer Secret")]
        string ConsumerSecret { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Created At")]
        DateTime CreatedAt { get; set; }
    }
}
