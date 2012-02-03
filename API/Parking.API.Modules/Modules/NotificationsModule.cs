/**
 *
 * @package     Parking.API.Modules
 * @author      The JSONs
 * @copyright   2012 -
 * @license     Propietary
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Nancy;
using Nancy.ViewEngines.Razor;
using Nancy.Serializers.Json;
using Sieena.Parking.API.Models;

namespace Sieena.Parking.API.Modules
{
    using Classes;

    public class NotificationsModule : AbstractBaseModule
    {
        public NotificationsModule()
            : base("Notifications")
        {
        }

        [Api("/GetAll", ApiMethod.GET)]
        public List<Notification> GetAll(int userId, int amount)
        {
            return Notification.GetLastByUserId(userId, amount);
        }
    }
}