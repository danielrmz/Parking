using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sieena.Parking.API.Models.Exceptions
{
    public class CheckinExistsException : APIException
    {
        public CheckinExistsException(string message) : base(message) { }
    }
}
