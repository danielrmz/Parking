using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sieena.Parking.API.Models.Exceptions
{
    using i18n = Sieena.Parking.Common.Resources.UI;

    public class InvalidTokenException : APIException
    {
        public InvalidTokenException(string message) : base(message) { }
        public InvalidTokenException() : base(i18n.API_ErrorInvalidToken) { }
        public InvalidTokenException(Exception e) : base(i18n.API_ErrorInvalidToken, e) { }
    }
}
