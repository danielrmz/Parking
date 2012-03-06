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
     * User profiel dialog 
     *
     * @extends appbase.View
     */
    appviews.Profile = appbase.View.extend({
        
        secure: true,

        /**
         * @constructor
         */
        "initialize": function() {
            var userId = appdata.CurrentUser.get("UserId");
            this.model = appdata.Users.get(userId); 
        },


        /**
         * @enum {string}
         */
        "events": {
             "click .js-button-success": "doSave"
        },

        /**
         * View's template
         * @type {string}
         */
        "template": config.ClientTemplatesUrl + "Account/PopProfile.html",

        /**
         * @inheritDoc
         */
        "render": function() { 
            apphelpers.RenderViewTemplate.apply(this, arguments); 
        },


        /**
         * Saves the specified user.
         */
        "doSave": function() { 
            var btn  = $(this.el).find(".js-button-success");
            var form = $(this.el).find("form");
            var msg  = $(this.el).find(".js-success");
            var obj  = form.serializeObject();
            
            if(btn.hasClass("disabled")) { 
                return;
            }
            
            btn.addClass("disabled");
            
            this.model.set(obj); 
            
            this.model.save({}, {"success": function() { 
                msg.fadeIn();
                btn.removeClass("disabled");
            }});

            return false;
        }
            
         
    });

     

})(jQuery, Parking);