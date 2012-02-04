using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Sieena.Parking.API.Models.Interfaces
{
    public interface IUserInfo 
    {
        int UserId { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        bool Gender { get; set; }
        string PhoneHome { get; set; }
        string PhoneOffice { get; set; }
        string PhoneOfficeExtension { get; set; }
        string PhoneCel { get; set; }
        string ProfilePictureUrl { get; set; }
    }
}
