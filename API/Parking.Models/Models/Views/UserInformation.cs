using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Sieena.Parking.API.Models.Views
{
    public class UserInformation
    {
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Display(Name = "Session Id")]
        public Guid SessionId { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Profile Picture Url")]
        public string ProfilePictureUrl { get; set; }

        [Display(Name = "Is Authenticated")]
        public bool IsAuthenticated { get; set; }
        
    }
}
