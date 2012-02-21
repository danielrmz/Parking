using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sieena.Parking.API.Models.Exceptions
{
    public class APIException : Exception
    {
        public APIException(string message) : base(message) { }
        public APIException(string message, Exception e) : base(message, e) { }
    }
}
