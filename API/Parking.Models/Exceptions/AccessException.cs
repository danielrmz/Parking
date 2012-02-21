using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sieena.Parking.API.Models.Exceptions
{
    public class AccessException : Exception
    {
        public AccessException(string message) : base(message) { }
    }
}
