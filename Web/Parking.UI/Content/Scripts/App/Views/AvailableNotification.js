/**
* Base namespace for the application.
*
* @license Copyright 2012. The JSONS
*/

namespace("Parking.App.Views");
namespace("Parking.App.Data");

(function ($, parking, undefined) {
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
     * Notification to be displayed to a user if he/she is blocking another 
     *
     * @extends appbase.View
     */
    appviews.AvailableNotification = appbase.View.extend({
        
        secure: true,

        /**
         * @constructor
         */
        "initialize": function() { 
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
        "template": config.ClientTemplatesUrl + "Parking/ModalAvailableNotification.html",

        /**
         * @inheritDoc
         */
        "render": function() { 
            apphelpers.RenderViewTemplate.apply(this, arguments); 
            $(this.el).find(".modal").modal("show");
        } 
         
    });

     

})(jQuery, Parking);