using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ComponentModel.DataAnnotations;
using System.Globalization;
using DataAnnotationsExtensions;

namespace Sieena.Parking.API.Models.Interfaces
{
    public interface IRole : IParkingModel
    {
        [Integer]
        [Min(1)]
        [Display(Name = "Role Id")]
        int RoleId { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Role Name")]
        string RoleName { get; set; }

        [Required]
        [Integer]
        [Range(0,10)]
        [Display(Name = "Role Level")]
        int RoleLevel { get; set; }
    }
}
