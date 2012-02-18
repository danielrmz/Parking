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

        defaults: {
//            CheckInId: 0,
            StartTime: new Date(),
            EndTime: "",
            SpaceId: 0,
            ReservationId: "",
            RegistredFrom: 0,
            RegistredBy: 0
        },

        initialize: function() {
            this.set({"StartTime": this.defaults.StartTime});
            console.log('Checkin model has been initialized');
        }

    });

})(jQuery, Parking.App.Models);