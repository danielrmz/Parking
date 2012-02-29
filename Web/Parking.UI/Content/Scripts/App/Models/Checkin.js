/**
* Checkin
*
* @license Copyright 2012. The JSONS
*/
namespace("Parking.App.Base");
namespace("Parking.App.Models");

(function ($, parking, undefined) {
    var config         = parking["Configuration"];
    var appbase        = parking["App"]["Base"];
    var appmodels      = parking["App"]["Models"]; 

    /**
     *
     * @extends Parking.App.Base.Model
     */
    appmodels.Checkin = appbase.Model.extend({
         
        /**
         * Primary Key
         * @type {string}
         */
        "idAttribute": "CheckInId",

        /**
         * Model's base endpoint
         * @type {string}
         */
        "urlRoot": function() { 
            var userId = this.get("UserId");
            var checkinId = this.get("CheckInId");
            
            if(checkinId == 0 && !this.isNew()) {
                return Parking.Configuration.APIEndpointUrl + "checkins/user/"+userId; 
            } else {
                return Parking.Configuration.APIEndpointUrl + "checkins/current"; 
            }
        },

        /**
         * @enum {Object}
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

        isCheckedIn: function() {
            return (this.get("CheckInId") > 0 && !this.isCheckedOut());
        },

        isCheckedOut: function() { 
            return this.get("EndTime") != null;
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

})(jQuery, Parking);