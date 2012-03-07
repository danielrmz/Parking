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
             "click #general .js-button-success": "doSave",
             "click #notifications .js-button-success": "doNotificationsSave"
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
            var form = $(this.el).find("#general form");
            this.saveUser(form);
            return false;
        },
        
        /**
         * Saves the notifications settings
         */
        "doNotificationsSave": function() { 
            var form = $(this.el).find("#notifications form");
            var notificationAvailability = $(this.el).find("[name=NotificationsAvailability]").attr("checked") == "checked";
            this.saveUser(form, {"NotificationsAvailability": notificationAvailability});
            return false;
        },

        /**
         * Saves the user
         */
        saveUser: function(form, override) {
            var btn  = form.find(".js-button-success");
            var obj  = form.serializeObject();
            var msg  = form.find(".js-success");

            if(btn.hasClass("disabled")) { 
                return;
            }
            btn.addClass("disabled"); 

            var localeChanged = false;
            if(obj["Locale"] && obj["Locale"] != this.model.get("Locale")) {
                localeChanged = true;
            }
             
            this.model.set(obj);
            this.model.set(override);

            this.model.save({}, {"success": function() { 
                msg.fadeIn();
                btn.removeClass("disabled");
                if(localeChanged) {
                    $.cookie("ParkingLocale", obj["Locale"]);
                    location.reload();
                }
            }});
        }
         
    });

     

})(jQuery, Parking);