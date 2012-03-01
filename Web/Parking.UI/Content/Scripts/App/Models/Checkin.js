/**
* Checkin
*
* @license Copyright 2012. The JSONS
*/
namespace("Parking.App.Base");
namespace("Parking.App.Models");

(function ($, parking) {
    var config         = parking["Configuration"];
    var appbase        = parking["App"]["Base"];
    var appmodels      = parking["App"]["Models"]; 

    /**
     *
     * @extends appbase.Model
     */
    appmodels.Checkin = appbase.Model.extend({
         
        /**
         * Primary Key
         * @type {string}
         */
        "idAttribute": "CheckInId",

        /**
         * Model's base endpoint
         * @type {function(): string}
         */
        "urlRoot": function() { 
            var userId = this.get("UserId");
            var checkinId = this.get("CheckInId");
            
            if(checkinId == 0 && !this.isNew()) {
                return config.APIEndpointUrl + "checkins/user/"+userId; 
            } else {
                return config.APIEndpointUrl + "checkins/current"; 
            }
        },

        /**
         * @enum {string|number|boolean|null|Date}
         */
        "defaults": {
            "UserId": 0,
            "CheckInId": "",
            "StartTime": new Date(),
            "EndTime": null,
            "SpaceId": 0,
            "ReservationId": "",
            "RegisteredFrom": 0,
            "RegisteredBy": 0
        },

        /**
         * @constructor
         */
        "initialize": function() {
        },

        /**
         * Determines if the check in is a saved one and active one.
         * @return {boolean}
         */
        isCheckedIn: function() {
            return (this.get("CheckInId") > 0 && !this.isCheckedOut());
        },

        /**
         * Determines if the check in has already left the parking lot.
         * @return {boolean}
         */
        isCheckedOut: function() { 
            return this.get("EndTime") != null;
        },

        /**
         * Checks out the checkin
         */
        Checkout: function() {
            var self = this;
            $.post(config.APIEndpointUrl + "checkins/", function(data) { 
                if(data["Error"] == false) {
                    self.set(data["Response"]);
                }
            });
        }

    });

})(jQuery, Parking);