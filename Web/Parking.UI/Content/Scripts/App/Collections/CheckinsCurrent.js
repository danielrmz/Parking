/**
* Base namespace for the application.
*
* @package     Parking.UI.Scripts
* @author      The JSONs
* @copyright   2012
* @license     Propietary
*/

namespace("Parking.App.Collections");

(function ($, collections, undefined) {

    collections.CheckinsCurrent = Backbone.Collection.extend({
        
        url: Parking.Configuration.APIEndpointUrl + "checkins/current",

        model: Parking.App.Models.Checkin,
        
        initialize: function() {
            console.log("CheckinCurrent collection has been initialized");
            this.bind("add", this.save);
            // Initialize PUBNUB Listener
            this.listen();
        },

        save: function(checkinsCurrent) {
                console.log("A checkin was added to the collection with the following data:" 
                    + " StartTime: "        + checkinsCurrent.get("StartTime") 
                    + " EndTime: "          + checkinsCurrent.get("EndTime")
                    + " SpaceId: "          + checkinsCurrent.get("SpaceId")
                    + " ReservationId: "    + checkinsCurrent.get("ReservationId")
                    + " RegistredFrom: "    + checkinsCurrent.get("RegistredFrom")
                    + " RegistredBy: "      + checkinsCurrent.get("RegistredBy")
                );
         },
         
        parse: function(response) { 
            if(response["Error"] == false) { 
                return response["Response"];
            }
        },

        listen: function() {
            var self = this;
             
            PUBNUB.subscribe({
                channel : "parking:checkins:current",
                restore : false, 
                callback : function(message) { 
                    console.log(message);
                    // Detect the message, parse the type
                    // and add it to the collection.

                    // Trigger added event.
                },
                disconnect : function() { },
                reconnect : function() { }, 
                connect : function() { }
            });
             
        }

    });


})(jQuery, Parking.App.Collections);