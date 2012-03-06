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
     *
     * @extends appbase.Collection
     */
    appcollections.Users = appbase.ListenerCollection.extend({
        
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

        channel: "parking:users",

        /**
         * @inheritDoc
         */
        onMessageReceived: function(msg) { 
            var userId = msg["UserId"];
            var collectionObj = this.get(userId);
            
            // Update or add the user
            if(collectionObj) { 
                collectionObj.set(msg);
            } else {
                this.add(msg);
            }
        },

        /**
         * Searches a user by its full name
         *
         * @param {string} query    The query to be performed.
         * @return {Array.<Object>}
         */
        searchByName: function(query) {
            if(query == "") { return this; }

            var pattern = new RegExp(query, "gi");
            return _(this.filter(function(data) { 
                return pattern.test(data.FullName());
            }));
        }
         
    });


})(jQuery, Parking);