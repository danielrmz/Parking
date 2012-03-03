/**
* Base namespace for the application.
*
* @license Copyright 2012. The JSONS
*/

namespace("Parking.App.Views");
namespace("Parking.App.Data");

(function ($, parking) {
    var i18n           = parking["Resources"]["i18n"];
    var common         = parking["Common"];
    var config         = parking["Configuration"];
    var appbase        = parking["App"]["Base"];
    var appmodels      = parking["App"]["Models"]; 
    var appdata        = parking["App"]["Data"]; 
    var appviews       = parking["App"]["Views"];
    var appcollections = parking["App"]["Collections"];
    var apphelpers     = parking["App"]["Helpers"];

    /**
     * User List PopUp
     * This displays the users
     *
     * @extends appbase.View
     */
    appviews.UserList = appbase.View.extend({
        
        /**
         * @constructor
         */

         "initialize": function() {
            this.collection = appdata.Users;                        
        },


        /**
         * @enum {string}
         */
        "events": {
        },

        /**
         * View's template
         * @type {string}
         */
        "template": config.ClientTemplatesUrl + "Shared/UserList.htm",

        /**
         * @inheritDoc
         */
        "render": apphelpers.RenderViewTemplate
         
         
    });


})(jQuery, Parking);