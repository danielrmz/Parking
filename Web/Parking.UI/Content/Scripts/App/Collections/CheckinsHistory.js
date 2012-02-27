/**
* Base namespace for the application.
*
* @package     Parking.UI.Scripts
* @author      The JSONs
* @copyright   2012 Propiertary
*/
namespace("Parking.App.Base");
namespace("Parking.App.Collections");

(function ($, collections, undefined) {

    collections.CheckinsHistory = Parking.App.Base.ListenerCollection.extend({

        channel: "parking:checkins:history",

        url: Parking.Configuration.APIEndpointUrl + "checkins/history/3",

        model: Parking.App.Models.CheckinNotification,
        
        initialize: function() {
            this._super('initialize'); 

            this.on("add", this.verifyLimit); 
            this.on("reset", this.verifyLimit);
        },
        
        onMessageReceived: function(msg) { 
            this.add(msg); 
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

        }
        
    });


})(jQuery, Parking.App.Collections);