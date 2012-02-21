using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Sieena.Parking.API.Models.Views
{
    public class CheckinInformation
    {
        [Display(Name = "User Id")]
        public int UserId { get; set; }

        [Display(Name = "User name")]
        public string UserName { get; set; }
         
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Profile Picture Url")]
        public string ProfilePictureUrl { get; set; }

        [Display(Name = "Start Time")]
        public DateTime StartTime { get; set; }

        [Display(Name = "End Time")]
        public DateTime? EndTime { get; set; }

        [Display(Name = "Space Id")]
        public int SpaceId { get; set; }

        [Display(Name = "Checkin Id")]
        public int CheckInId { get; set; }
    }
}
