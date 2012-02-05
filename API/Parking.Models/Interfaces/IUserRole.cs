using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ComponentModel.DataAnnotations;
using System.Globalization;
using DataAnnotationsExtensions;

namespace Sieena.Parking.API.Models.Interfaces
{
    public interface IUserRole : IParkingModel
    {
        [Required]
        [Integer]
        [Min(1)]
        [Display(Name = "User Id")]
        int UserId { get; set; }

        [Required]
        [Integer]
        [Min(1)]
        [Display(Name = "Role Id")]
        int RoleId { get; set; }
    }
}
