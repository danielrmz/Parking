/**
* Base namespace for the application.
*
* @package     Parking.UI.Scripts
* @author      The JSONs
* @copyright   2012
* @license     Propietary
*/
namespace("Parking.App.Base");
namespace("Parking.App.Models");

(function ($, models, undefined) {

    models.Checkin = Parking.App.Base.Model.extend({

        urlRoot: Parking.Configuration.APIEndpointUrl + 'checkins/current',

        idAttribute: "CheckInId",
        urlRoot: function() { 
            var userId = this.get("UserId");
            var checkinId = this.get("CheckInId");
            if(checkinId == 0) {
                return Parking.Configuration.APIEndpointUrl + "checkins/user/"+userId; 
            } else {
                return Parking.Configuration.APIEndpointUrl + "checkins"; 
            }
        },

        defaults: {
            UserId: 0,
            CheckInId: "",
            StartTime: new Date(),
            EndTime: null,
            SpaceId: 0,
            ReservationId: "",
            RegisteredFrom: 0,
            RegisteredBy: 0
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
        },


        Checkout: function() {
            var self = this;
            $.post(Parking.Configuration.APIEndpointUrl + "checkins/", function(data) { 
                if(data["Error"] == false) {
                    self.set(data["Response"]);
                }
            });
        }

    });

})(jQuery, Parking.App.Models);