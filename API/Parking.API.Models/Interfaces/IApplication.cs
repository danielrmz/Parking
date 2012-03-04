using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ComponentModel.DataAnnotations;
using System.Globalization;
using DataAnnotationsExtensions;

namespace Sieena.Parking.API.Models.Interfaces
{
    public interface IApplication : IParkingModel
    {
        [Integer]
        [Min(1)]
        [Display(Name = "Application Id")]
        int ApplicationId { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Name")]
        string ApplicationName { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Created At")]
        DateTime? CreatedAt { get; set; }
    }
}
