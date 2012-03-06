using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sieena.Parking.API.Modules.Classes
{
    /// <summary>
    /// Constants 
    /// </summary>
    internal enum AccessLevel
    {
        All = 0,
        Admin, 
        User
    }


    /// <summary>
    /// HTTP Methods available for the api definition
    /// </summary>
    public enum ApiMethod
    {
        GET,
        PUT,
        POST,
        DELETE,
        GETPOST
    }
}
