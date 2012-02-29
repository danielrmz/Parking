/**
 * Base namespace for the application.
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
     * @extends Parking.Base.Collection
     */
    appcollections.SpaceBlockings = appbase.Collection.extend({

        /**
         * Collection base model.
         *
         * @type {Parking.App.Models.SpaceBlock}
         */
        "model": appmodels.SpaceBlock,

        /**
         * Endpoint URL
         *
         * @type {string}
         */
        "url": config.APIEndpointUrl + "spaces/blockings",
 
        /**
         * Gets the Space Ids of the spaces blocking a particular one
         * @param {number} spaceId Space we are gonna retrieve the ones that are blocking it
         * @param Array.<number> Array of space ids that are blocking the specified one.
         */
        getBlockingIdsBySpaceId: function(spaceId) { 
            return this.filter(function(model){ return model.get("BaseSpaceId") == spaceId}).map(function(model) { return model.get("BlockingSpaceId"); });
        }       
    });

})(jQuery, Parking);