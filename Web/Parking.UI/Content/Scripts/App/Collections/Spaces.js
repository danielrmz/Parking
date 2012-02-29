/**
 * Spaces in the system
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
     * Represents the collection of spaces in the system.
     *
     * @extends appbase.Collection
     */
    appcollections.Spaces = appbase.Collection.extend({

        /**
         * Collection base model.
         *
         * @type {appmodels.Space}
         */
        "model": appmodels.Space,

        /**
         * Endpoint URL
         *
         * @type {string}
         */
        "url": config.APIEndpointUrl + "spaces/GetAllByPlaceId/1"
         
        
    });

})(jQuery, Parking);