using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using DataAnnotationsExtensions;

namespace Sieena.Parking.API.Models.Interfaces
{
    interface ISession : IParkingModel
    {
        [Display(Name = "Session Id")]
        Guid SessionId { get; set; }

        [Integer]
        [Min(1)]
        [Display(Name = "User Id")]
        int? UserId { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Created At")]
        DateTime? CreatedAt { get; set; }
         
        [DataType(DataType.DateTime)]
        [Display(Name = "Expires At")]
        DateTime? ExpiresAt { get; set; }
         
        [DataType(DataType.Text)]
        [Display(Name = "Data")]
        string Data { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Last Access")]
        DateTime? LastAccess { get; set; }
    }
}
