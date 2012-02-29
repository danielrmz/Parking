/**
 * Collection that represents the last check ins made in the application
 *
 * @license Copyright 2012. The JSONS
 */
namespace("Parking.App.Base");
namespace("Parking.App.Collections");

(function ($, parking) {
    var config         = parking["Configuration"];
    var appbase        = parking["App"]["Base"];
    var appmodels      = parking["App"]["Models"];
    var appcollections = parking["App"]["Collections"];

    /**
     * Collection that represents the history of checkins. 
     * Since it inherits from ListenerCollection this will be updated in real time.
     *
     * @extends appbase.ListenerCollection
     */
    appcollections.CheckinsHistory = appbase.ListenerCollection.extend({
        
        /**
         * Endpoint URL
         *
         * @type {string}
         */
        "url": config.APIEndpointUrl + "checkins/history/3",

        /**
         * Collection base model.
         *
         * @type {appmodels.CheckinNotification}
         */
        "model": appmodels.CheckinNotification,
        
        /**
         * Constructor
         * @constructor
         */
        "initialize": function() {
            this._super('initialize'); 

            this.on("add", this.verifyLimit); 
            this.on("reset", this.verifyLimit);
        },

        /**
         * Collection's max size
         * @const
         * @type {number}
         */
        limit: 3,

        /**
         * @inheritDoc
         */
        channel: "parking:checkins:history",

        /**
         * @inheritDoc
         */
        onMessageReceived: function(msg) { 
            this.add(msg); 
        },

        /**
         * Verifes that the collection size doesn't go pass the limit. 
         * @private
         */
        verifyLimit: function() {
            // Only show the last X ones. 

            var sorted = this.sortBy(function(checkin){ 
                var start     = checkin.get("StartTime");
                var checkinId = checkin.get("CheckInId");

                dnFormat = start.match("([0-9]{13})");

                if(dnFormat[1] != "") { 
                    return parseFloat(dnFormat[1]);
                } 
            });

            if(this.length > this.limit) {
                this.remove(_.first(sorted));
            }

        }
        
    });


})(jQuery, Parking);