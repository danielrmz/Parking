using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ComponentModel.DataAnnotations;
using System.Globalization;
using DataAnnotationsExtensions;

namespace Sieena.Parking.API.Models.Interfaces
{
    public interface IUser : IParkingModel
    {
        [Integer]
        [Min(1)]
        [Display(Name = "User Id")]
        int UserId { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        string Password { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        string Email { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Last Access")]
        DateTime? LastAccess { get; set; }

        [Required]
        [Integer]
        [Range(0,1)]
        [Display(Name = "Is Active")]
        bool IsActive { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Created At")]
        DateTime CreatedAt { get; set; }
    }
}
