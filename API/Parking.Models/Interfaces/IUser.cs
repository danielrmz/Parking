using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Sieena.Parking.API.Models.Interfaces
{
    public interface IUser
    {
        int UserId { get; set; }
        string Password { get; set; }
        string Email { get; set; }
        DateTime? LastAccess { get; set; }
        bool IsActive { get; set; }
        DateTime CreatedAt { get; set; }
    }
}
