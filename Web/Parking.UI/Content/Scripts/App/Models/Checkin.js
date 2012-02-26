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

        urlRoot: Parking.Configuration.APIEndpointUrl + 'checkins/current',

        idAttribute: "CheckInId",

        defaults: {
            CheckInId: "",
            StartTime: new Date(),
            EndTime: null,
            SpaceId: 0,
            ReservationId: "",
            RegistredFrom: 0,
            RegistredBy: 0
        },

        initialize: function() {
            console.log("Checkin model has been initialized");
            this.bind("add", this.insert);
        },

        insert: function(checkinsCurrent) {
            console.log("A checkin was added to the collection with the following data:" 
                + " StartTime: "        + checkinsCurrent.get("StartTime") 
                + " EndTime: "          + checkinsCurrent.get("EndTime")
                + " SpaceId: "          + checkinsCurrent.get("SpaceId")
                + " ReservationId: "    + checkinsCurrent.get("ReservationId")
                + " RegistredFrom: "    + checkinsCurrent.get("RegistredFrom")
                + " RegistredBy: "      + checkinsCurrent.get("RegistredBy")
            );
        }

    });

})(jQuery, Parking.App.Models);