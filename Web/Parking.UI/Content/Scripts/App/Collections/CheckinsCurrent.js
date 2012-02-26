﻿/**
* Base namespace for the application.
*
* @package     Parking.UI.Scripts
* @author      The JSONs
* @copyright   2012
* @license     Propietary
*/
namespace("Parking.App.Base");
namespace("Parking.App.Collections");

(function ($, collections, undefined) {

    collections.CheckinsCurrent = Parking.App.Base.ListenerCollection.extend({
        channel: "parking:checkins:current",

        url: Parking.Configuration.APIEndpointUrl + "checkins/current",

        model: Parking.App.Models.Checkin,
        
        onMessageReceived: function(msg) { 
            console.log("CheckinCurrent collection has been initialized");
            this.bind("add", this.save);
            // Check type in order to check adding or removal.
            this.add(msg); 
         
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
         
        }
    });


})(jQuery, Parking.App.Collections);