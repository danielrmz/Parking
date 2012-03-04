using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Sieena.Parking.API.Models.Views
{
    public class CheckinNotification
    {
        public CheckinNotification(CheckinNotification c, Checkin.NotificationType type)
        {
            CheckInId = c.CheckInId;
            SpaceId = c.SpaceId;
            UserId = c.UserId;
            StartTime = c.StartTime;
            EndTime = c.EndTime;
            LastModified = (type == Checkin.NotificationType.Checkout && c.EndTime.HasValue) ? c.EndTime.Value : c.StartTime;
            RegisteredBy = c.RegisteredBy;
            RegisteredFrom = c.RegisteredFrom;
            NotificationType = (int)((type == Checkin.NotificationType.Checkout && c.EndTime.HasValue) ? Checkin.NotificationType.Checkout : Checkin.NotificationType.Checkin);
        }

        public CheckinNotification(Checkin c, Checkin.NotificationType type)
        {
            CheckInId = c.CheckInId;
            SpaceId = c.SpaceId;
            UserId = c.UserId;
            StartTime = c.StartTime;
            EndTime = c.EndTime;
            LastModified = (type == Checkin.NotificationType.Checkout && c.EndTime.HasValue) ? c.EndTime.Value : c.StartTime;
            RegisteredBy = c.RegisteredBy;
            RegisteredFrom = c.RegisteredFrom;
            NotificationType = (int)((type == Checkin.NotificationType.Checkout && c.EndTime.HasValue) ? Checkin.NotificationType.Checkout : Checkin.NotificationType.Checkin);
        
        }

        [Display(Name = "Notification Id")]
        public string NotificationId { 
            get {
                return string.Format("{0}{1}", this.NotificationDesc.ToUpper(), this.CheckInId);
            } 
        }

        [Display(Name = "Checkin Id")]
        public int CheckInId { get; set; }

        [Display(Name = "User Id")]
        public int UserId { get; set; }

        [Display(Name = "Space Id")]
        public int SpaceId { get; set; }

        [Display(Name = "Start Time")]
        public DateTime StartTime { get; set; }

        [Display(Name = "End Time")]
        public DateTime? EndTime { get; set; }

        [Display(Name = "Notification Type")]
        public int NotificationType { get; set; }

        [Display(Name = "Notification Description")]
        public string NotificationDesc { get { return ((NotificationType == (int)Checkin.NotificationType.Checkin) ? "in" : "out"); } }

        [Display(Name = "Last Modified")]
        public DateTime LastModified { get; set; }

        [Display(Name = "Registered From")]
        public int RegisteredFrom { get; set; }

        [Display(Name = "Registered By")]
        public int RegisteredBy { get; set; }
         
    }
}
