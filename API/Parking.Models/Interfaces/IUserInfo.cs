using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ComponentModel.DataAnnotations;
using System.Globalization;
using DataAnnotationsExtensions;

namespace Sieena.Parking.API.Models.Interfaces
{
    public interface IUserInfo : IParkingModel
    {
        [Required]
        [Integer]
        [Min(1)]
        [Display(Name = "User Id")]
        int UserId { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "First Name")]
        string FirstName { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Last Name")]
        string LastName { get; set; }

        [Required]
        [Display(Name = "Gender")]
        bool Gender { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Home Phone")]
        string PhoneHome { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Office Phone")]
        string PhoneOffice { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Office Extension")]
        string PhoneOfficeExtension { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Cellphone")]
        string PhoneCel { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Profile Pic url")]
        string ProfilePictureUrl { get; set; }
    }
}
