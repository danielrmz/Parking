/**
 * Users in the system
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
     * Represents all the users in the system
     *
     * @extends appbase.Collection
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
        "model": appmodels.UserInformation,

        searchByName: function(query) {
            if(query == "") { return this; }

            var pattern = new RegExp(query, "gi");
            return _(this.filter(function(data) { 
                return pattern.test(data.FullName());
            }));
        }
         
    });


})(jQuery, Parking);