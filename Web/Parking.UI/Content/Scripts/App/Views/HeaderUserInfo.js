/**
* Base namespace for the application.
*
* @license Copyright 2012. The JSONS
*/

namespace("Parking.App.Views");

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
     * User information panel
     *
     * @extends appbase.View
     */
    appviews.HeaderUserInfo = appbase.View.extend({
        
        /**
         * @constructor
         */
        "initialize": function() {
            this.model.on("change", this.render, this);
        },

        /**
         * View's template
         * @type {string}
         */
        "template": config.ClientTemplatesUrl + "Shared/HeaderUserInfo.html",
        
        /**
         * @inheritDoc
         */
        "render": apphelpers.RenderViewTemplate,

        
        /**
         * @enum {string}
         */
        "events": {
           "click .js-logout" : "logout",
           "click .js-profile": "showProfile"
        },

        /**
         * Event for the logout link. 
         * Logs out a user.
         *
         * @param {Object} e Event
         * @return {boolean}
         */
        "logout": function(e) {
            appdata.CurrentUser.destroy(function() {  
                appdata.Router.navigate("login", true);
            }); 
            
            return false;
        },

        /**
         * Event for the profile 
         *
         * @param {Object} e Event
         * @return {boolean}
         */
        "showProfile": function(e) { 
           var profile = new appviews.Profile({el: $(".js-placeholder-profile")});
           profile.render();
           $(profile.el).find(".modal").modal("show"); 
           return false;
        }

    });


})(jQuery, Parking);