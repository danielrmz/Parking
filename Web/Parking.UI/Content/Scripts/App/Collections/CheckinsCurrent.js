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

            // Initialize PUBNUB Listener
            this.listen();
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