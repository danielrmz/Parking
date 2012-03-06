/**
* Base namespace for the application.
*
* @license Copyright 2012. The JSONS
*/

namespace("Parking.App.Views");

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
           "click .js-logout": "logout",
           "click .js-profile": "showEditProfileDialog"
        },

        /**
         * Event for the logout link. 
         * Logs out a user.
         *
         * @return {boolean}
         */
        "logout": function(e) {
            appdata.CurrentUser.destroy(function() {  
                appdata.Router.navigate("login", true);
            }); 
            
            return false;
        },

        "showEditProfileDialog": function(e) {
//            var car = $(e.target);
            //var spaceId = car.data("spaceid");
//            var space  = null;
//            var userId = 0;

//            if(!spaceId || spaceId <= 0 || isNaN(spaceId)) {
//                common.DisplayGlobalError(i18n.get("Main_ErrorSpaceNotAvailable"));
//                return;
//            }

//            space = appdata.Spaces.get(spaceId);

//            if(appdata.CurrentUserCheckIn.isCheckedIn() && !appdata.CurrentUser.isAdmin()) {
//                // Change to display a warning message
//                common.DisplayGlobalError(i18n.get("Main_InfoAlreadyCheckedIn"));
//                return;
//            }

            // Check that space isn't taken
//            if(appdata.CheckinsCurrent.isSpaceUsed(spaceId)) {
//                common.DisplayGlobalError(i18n.get("Main_ErrorSpaceNotAvailable"));
//                return;
//            }

            // Proceed to open confirmation box
//            car.addClass("selected");
//            
//            if(appdata.CurrentUser.isAdmin()) {
//                var dialog = $(this.el).find(".js-confirmation-dialog.js-dialog-select-user");
//                dialog.modal('show');
//                dialog.off("hide").on("hide", function(){car.removeClass("selected");});
//                this.UserSelector.trigger("render");
//            } else {
                // Display user selection box.
                //modal dialog-message dialog-warning js-warning-dialog fade
                //modal dialog-confirmation js-confirmation-dialog js-dialog-selfuser fade
                //var dialog = $(this.el).find(".js-confirmation-dialog.js-dialog-selfuser");
                var dialog = $(this.el).find(".dialog-warning.js-warning-dialog");
                
                //var msg = dialog.find(".js-message");
                //msg.html(i18n.get("Main_ConfirmCheckinMessage").replace("{{Alias}}", space.get("Alias")));
                //car.data("tmpUserId", appdata.CurrentUser.get("UserId"));

                // Display confirm dialog.
                
                dialog.modal('show');
                //dialog.off("hide").on("hide", function(){car.removeClass("selected");} );
            //}

        }

    });


})(jQuery, Parking);