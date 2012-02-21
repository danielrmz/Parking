/**
* Base namespace for the application.
*
* @package     Parking.UI.Scripts
* @author      The JSONs
* @copyright   2012
* @license     Propietary
*/

namespace("Parking.App.Models");

(function ($, models, undefined) {

    models.CheckinNotification = Backbone.Model.extend({
        idAttribute: "NotificationId",

        defaults: {
            NotificationId: "",
            CheckInId: 0,
            UserId: 0,
            SpaceId: 0,
            StartTime: new Date(),
            EndTime: null, 
            RegisteredFrom: 0,
            RegisteredBy: 0,
            LastModified: new Date(),
            NotificationType: 0,
            NotificationDesc: ""
        },

        initialize: function() {
        }

    });

})(jQuery, Parking.App.Models);