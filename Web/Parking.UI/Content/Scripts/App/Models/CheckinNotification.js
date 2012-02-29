/**
* Checkin Notification
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
    appmodels.CheckinNotification = appbase.Model.extend({
        
        /**
         * Primary Key
         * @type {string}
         */
        "idAttribute": "NotificationId",

        /**
         * @enum {Object}
         */
        "defaults": {
            "NotificationId": "",
            "CheckInId": 0,
            "UserId": 0,
            "SpaceId": 0,
            "StartTime": new Date(),
            "EndTime": null, 
            "RegisteredFrom": 0,
            "RegisteredBy": 0,
            "LastModified": new Date(),
            "NotificationType": 0,
            "NotificationDesc": ""
        },

        /**
         * @constructor
         */
        "initialize": function() {
        }

    });

})(jQuery, Parking);