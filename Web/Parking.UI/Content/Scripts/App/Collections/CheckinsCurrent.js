/**
 * Collection type that represents the current checkins in the application
 *
 * @license Copyright 2012. The JSONS
 */
namespace("Parking.App.Base");
namespace("Parking.App.Collections");

(function ($, parking, undefined) {
    var config         = parking["Configuration"];
    var appbase        = parking["App"]["Base"];
    var appmodels      = parking["App"]["Models"];
    var appcollections = parking["App"]["Collections"];

    /**
     * Collection that represents the current checkins. 
     * Since it inherits from ListenerCollection this will be updated in real time.
     *
     * @extends appbase.ListenerCollection
     */
    appcollections.CheckinsCurrent = appbase.ListenerCollection.extend({
        
        /**
         * Endpoint URL
         *
         * @type {string}
         */
        "url": config.APIEndpointUrl + "checkins/current",

        /**
         * Collection base model.
         *
         * @type {appmodels.Checkin}
         */
        "model": appmodels.Checkin,
        
        /**
         * Constructor
         * @constructor
         */
        "initialize": function() { 
            this._super('initialize');   
        },

        /**
         * @inheritDoc
         */
        channel: "parking:checkins:current",

        /**
         * @inheritDoc
         */
        onMessageReceived: function(msg) { 

            // Check type in order to check adding or removal.
            var checkinId = msg["CheckInId"];
            var collectionObj = this.get(checkinId);
             
            if(collectionObj) {
                // Probably an enddate update.
                collectionObj.set(msg);

                if(collectionObj.isCheckedOut()) {
                    this.remove(collectionObj);
                }

            } else {
                this.add(msg);
            }

        },

        /**
         * Determines if a space in the collection is taken or not.
         *
         * @param {number} spaceId The Space Id
         * @return {boolean} True if the space is taken by someone, False otherwise.
         */
        isSpaceUsed: function(spaceId) { 
            return this.filter(function(c) { return c.get("SpaceId") == spaceId }).length > 0;
        }
         
    });


})(jQuery, Parking);