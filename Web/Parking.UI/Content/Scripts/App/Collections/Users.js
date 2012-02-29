/**
 * Users in the system
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
     * Represents all the users in the system
     * @extends Parking.Base.Collection
     */
    appcollections.Users = appbase.Collection.extend({
        
        /**
         * Endpoint URL
         *
         * @type {string}
         */
        "url": config.APIEndpointUrl + "users/GetAll",

        /**
         * Collection base model.
         *
         * @type {Parking.App.Models.UserInformation}
         */
        "model": appmodels.UserInformation
         
    });


})(jQuery, Parking);