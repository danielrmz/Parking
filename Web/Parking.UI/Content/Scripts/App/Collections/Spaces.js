/**
 * Spaces in the system
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
     * Represents the collection of spaces in the system.
     *
     * @extends Parking.Base.Collection
     */
    appcollections.Spaces = appbase.Collection.extend({

        /**
         * Collection base model.
         *
         * @type {Parking.App.Models.Space}
         */
        "model": appmodels.Space,

        /**
         * Endpoint URL
         *
         * @type {string}
         */
        "url": config.APIEndpointUrl + "spaces/GetAllByPlaceId/1",
         
        
    });

})(jQuery, Parking);