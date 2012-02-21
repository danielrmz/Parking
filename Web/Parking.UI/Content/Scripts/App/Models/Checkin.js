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

    models.Checkin = Backbone.Model.extend({
        idAttribute: "CheckInId",

        defaults: {
            CheckInId: 0,
            StartTime: new Date(),
            EndTime: "",
            SpaceId: 0,
            ReservationId: "",
            RegistredFrom: 0,
            RegistredBy: 0
        },

        initialize: function() {
        }

    });

})(jQuery, Parking.App.Models);