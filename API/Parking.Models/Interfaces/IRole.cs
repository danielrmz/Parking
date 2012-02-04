using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Sieena.Parking.API.Models.Interfaces
{
    public interface IRole
    {
        int RoleId { get; set; }
        string RoleName { get; set; }
        int RoleLevel { get; set; }
    }
}
