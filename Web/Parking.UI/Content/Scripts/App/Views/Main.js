/**
* Base namespace for the application.
*
* @license Copyright 2012. The JSONS
*/

namespace("Parking.App.Data");
namespace("Parking.App.Views");
namespace("Parking.App.Models");

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
     * Main Car View
     *
     * @extends appbase.View
     */
    appviews.Main = appbase.View.extend({
        
        /**
         * @constructor
         */
        "initialize": function() {
            this.collection = appdata.Spaces;
            
            appdata.CheckinsCurrent.on("remove", this.onRemove, this);
            appdata.CheckinsCurrent.on("add", this.onAdd, this);
        },

        /**
         * Determines if the view is secure or not
         * @type {boolean}
         */
        secure: true,
        
        /**
         * View's template
         * @type {string}
         */
        "template": config.ClientTemplatesUrl + "Parking/Home.html",
        
        /**
         * @inheritDoc
         */
        "render": function() {
            apphelpers.RenderViewTemplate.apply(this, arguments);

            // Update checked in spaces.
            this.renderCheckedInSpaces();
        },
        
        /**
         * @enum {string}
         */
        "events": { 
            "click .js-space.used": "showDetailsDialog",
            "click .js-space.available": "showConfirmDialog",
            "click .js-confirmation-dialog .btn-close": "closeConfirmDialog",
            "click .js-confirmation-dialog .btn-success": "doCheckin"
        },

        /**
         * Does the checkin for a user.
         */
        "doCheckin": function() { 
            var car = $(this.el).find(".selected");
            var spaceId = car.data("spaceid");
            var userId = 0;
            var dialog = $(this.el).find(".js-confirmation-dialog");

            var data = { 
                CheckInId: null,
                SpaceId: spaceId,
                UserId: userId
            };

            var checkin = new appmodels.Checkin(data);
            
            checkin.save({}, { success: function(m) { 
                                                        appdata.CurrentUserCheckIn.set(m);
                                                        
                                                        dialog.modal('hide');
                                                        car.removeClass("selected");
                                                        }
                                            });

             
        },


        /**
         * Shows the details for a checked in user.
         */
        "showDetailsDialog": function() {},
       
        /**
         * Closes the confirmation dialog
         */
        "closeConfirmDialog": function() { 
            $(this.el).find(".js-confirmation-dialog").modal('hide');
            $(this.el).find(".selected").removeClass("selected");
        },

        
        /**
         * Shows the confirm dialog
         * @param {Object} e
         */
        "showConfirmDialog": function(e) {
            var car = $(e.target);
            var spaceId = car.data("spaceid");
            var space  = null;
            var userId = 0;

            if(!spaceId || spaceId <= 0 || isNaN(spaceId)) {
                common.DisplayGlobalError(i18n.get("Main_ErrorSpaceNotAvailable"));
                return;
            }

            space = appdata.Spaces.get(spaceId);

            if(appdata.CurrentUserCheckIn.isCheckedIn()) {
                // Change to display a warning message
                common.DisplayGlobalError(i18n.get("Main_InfoAlreadyCheckedIn"));
                return;
            }

            // Check that space isn't taken
            if(appdata.CheckinsCurrent.isSpaceUsed(spaceId)) {
                common.DisplayGlobalError(i18n.get("Main_ErrorSpaceNotAvailable"));
                return;
            }

            // Proceed to open confirmation box
            car.addClass("selected");

            if(appdata.CurrentUser.isAdmin()) {
                // Display user selection box.
                var dialog = $(this.el).find(".js-confirmation-dialog");
                var msg = dialog.find(".js-message");
                msg.html(i18n.get("Main_ConfirmCheckinMessage").replace("{{Alias}}", space.get("Alias")));
                
                $(this.el).find(".js-confirmation-dialog").modal(); 
            } else {
                // Display confirm dialog.
                $(this.el).find(".js-confirmation-dialog").modal();
                
            }

        },

        /**
         * Renders the check in spaces
         */
        renderCheckedInSpaces: function() {
            var map = $(this.el);

            $(this.el).find(".js-space").removeClass("used").removeClass("me").addClass("available");
            if(appdata.CheckinsCurrent) { 
                appdata.CheckinsCurrent.map(function(checkin) { 
                    var spaceId = checkin.get("SpaceId");
                    var spaceUI = map.find("[data-spaceid=" + spaceId + "].js-space");
                    spaceUI.removeClass("available").addClass("used");

                    if(checkin.get("UserId") == appdata.CurrentUser.get("UserId")) {
                        spaceUI.addClass("me");
                    }
                    spaceUI.data("checkinid", checkin.get("CheckInId"));
                });
            }
        },
       
        /**
         * Event-handler to add a check in to the map.
         * @param {Object} checkin Checkin to add
         * @private
         */
        onAdd: function(checkin) { 
            var spaceId = checkin.get("SpaceId");
            var car = $(this.el).find("[data-spaceid=" + spaceId + "]");

            car.data("checkinid", checkin.get("CheckInId")); 
            car.removeClass("available").addClass("used");

            if(appdata.CurrentUser.get("UserId") == checkin.get("UserId")) {
                car.addClass("me");
            }

            // Recheck if the user is blocked.
            appdata.CurrentUser.trigger("renew:IsBlocked");
                                                        
        },

        /**
         * Event-handler to remove the checkin from the map
         * @param {Object} checkin Checkin to remove
         * @private
         */
        onRemove: function(checkin) { 
            var spaceId = checkin.get("SpaceId");
            var car = $(this.el).find("[data-spaceid=" + spaceId + "]");
            car.data("checkinid", null); 
            car.removeClass("used").removeClass("me").addClass("available");
        }

        

    });


})(jQuery, Parking);