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
         * Validates the model
         * @param {Object} attrs
         * @return {Array.<string>}
         */
        "validate": function(attrs) { 
            
            var errors = [];
            if(attrs.SpaceId == 0) {
                errors.push("A space id must be defined for the checkin model");
            }
            if(attrs.UserId == 0) {
                errors.push("A user id must be specified for the checkin model");
            }
            if(errors.length > 0) {
                return errors;
            }
        },

        /**
         * Returns the start time correctly formatted
         * @return {Date}
         */
        StartTime: function() {
            var st = this.get("StartTime");
            var time = null;

            if(typeof st == "string") {
                var reg = st.match(/Date\(([0-9]+)\)/);
                if(reg) {
                    st = st.replace(/\//g, "");
                    time = new Date(parseFloat(reg[1]));
                } else {
                    time = st;
                } 
            } else {
                time = st;
            }

            return time;
        },

         /**
         * Returns the end time correctly formatted
         * @return {Date}
         */
        EndTime: function() {
            var st = this.get("EndTime");
            var time = null;

            if(typeof st == "string") {
                var reg = st.match(/Date\(([0-9]+)\)/);
                if(reg) {
                    st = st.replace(/\//g, "");
                    time = new Date(parseFloat(reg[1]));
                } else {
                    time = st;
                } 
            } else {
                time = st;
            }


            return time;
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
            $.ajax(config.APIEndpointUrl + "checkins/" + this.get("CheckInId"),{ type: 'DELETE', success: function(data) { 
                if(data["Error"] == false) {
                        self.set(data["Response"]);
                }
            }});
             
        }
         

    });

})(jQuery, Parking);