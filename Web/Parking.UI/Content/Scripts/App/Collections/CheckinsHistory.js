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

    collections.CheckinsHistory = Backbone.Collection.extend({
        
        url: Parking.Configuration.APIEndpointUrl + "checkins/history/3",

        model: Parking.App.Models.Checkin,
        
        initialize: function() {
            this.on("add", this.verifyLimit); 
            this.on("reset", this.verifyLimit);

            // Initialize PUBNUB Listener
            this.listen();
        },
        
        verifyLimit: function(args) {
            // Only show the last X ones. 

            var sorted = this.sortBy(function(checkin){ 
                var start = checkin.get("StartTime");
                var checkinId = checkin.get("CheckInId");

                dnFormat = start.match("([0-9]{13})");

                if(dnFormat[1] != "") { 
                    return parseFloat(dnFormat[1]);
                } 
            });

            if(this.length > 3) {
                this.remove(_.first(sorted));
            }

        },

        parse: function(response) { 
            if(response["Error"] == false) { 
                return response["Response"];
            }
        },

        listen: function() {
            var self = this;
             
            PUBNUB.subscribe({
                channel : "parking:checkins:history",
                restore : false, 
                callback : function(message) { 
                    self.add(message);
                    // Detect the message, parse the type
                    // and add it to the collection.

                    // Trigger added event.
                },
                disconnect : function() { },
                reconnect : function() { }, 
                connect : function() { }
            });

            /*PUBNUB.publish({             // SEND A MESSAGE.
                channel : "hello_world",
                message : "Hi from PubNub."
            })*/
        }

    });


})(jQuery, Parking.App.Collections);