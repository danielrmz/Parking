/**
* Base namespace for the application.
*
* @package     Parking.UI.Scripts
* @author      The JSONs
* @copyright   2012 Propiertary 
*/

namespace("Parking.App.Data");
namespace("Parking.App.Views");

(function ($, undefined) {
    var i18n = Parking.Resources.i18n;

    Parking.App.Views.Main = Backbone.View.extend({
        
        secure: true,
        
        template: Parking.Configuration.ClientTemplatesUrl + "Parking/Home.html",
        
        initialize: function() {
            this.collection = Parking.App.Data.Spaces;
            
            Parking.App.Data.CheckinsCurrent.on("remove", this.onRemove, this);
            Parking.App.Data.CheckinsCurrent.on("add", this.onAdd, this);
        },

        render: function() {
            Parking.App.Helpers.RenderViewTemplate.apply(this, arguments);

            // Update checked in spaces.
            this.renderCheckedInSpaces();
        },
          
        renderCheckedInSpaces: function() {
            var map = $(this.el);

            $(this.el).find(".js-space").removeClass("used").removeClass("me").addClass("available");
            if(Parking.App.Data.CheckinsCurrent) { 
                Parking.App.Data.CheckinsCurrent.map(function(checkin) { 
                    var spaceId = checkin.get("SpaceId");
                    var spaceUI = map.find("[data-spaceid=" + spaceId + "].js-space");
                    spaceUI.removeClass("available").addClass("used");

                    if(checkin.get("UserId") == Parking.App.Data.CurrentUser.get("UserId")) {
                        spaceUI.addClass("me");
                    }
                    spaceUI.data("checkinid", checkin.get("CheckInId"));
                });
            }
        },
       
        onAdd: function(checkin) { 
            var spaceId = checkin.get("SpaceId");
            var car = $(this.el).find("[data-spaceid=" + spaceId + "]");

            car.data("checkinid", checkin.get("CheckInId")); 
            car.removeClass("available").addClass("used");

            if(Parking.App.Data.CurrentUser.get("UserId") == checkin.get("UserId")) {
                car.addClass("me");
            }

            // Recheck if the user is blocked.
            Parking.App.Data.CurrentUser.trigger("renew:IsBlocked");
                                                        
        },

        onRemove: function(checkin) { 
            var spaceId = checkin.get("SpaceId");
            var car = $(this.el).find("[data-spaceid=" + spaceId + "]");
            car.data("checkinid", null); 
            car.removeClass("used").removeClass("me").addClass("available");
        },

        events: { 
            "click .js-space.used": "showDetailsDialog",
            "click .js-space.available": "showConfirmDialog",
            "click .js-confirmation-dialog .btn-close": "closeConfirmDialog",
            "click .js-confirmation-dialog .btn-success": "doCheckin"
        },

        "showDetailsDialog": function() {},
       
        "closeConfirmDialog": function() { 
            $(this.el).find(".js-confirmation-dialog").modal('hide');
            $(this.el).find(".selected").removeClass("selected");
        },

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

            var checkin = new Parking.App.Models.Checkin(data);
            
            checkin.save({}, { success: function(m) { 
                                                        Parking.App.Data.CurrentUserCheckIn.set(m);
                                                        
                                                        dialog.modal('hide');
                                                        car.removeClass("selected");
                                                        }
                                            });

             
        },

        "showConfirmDialog": function(e) {
            var car = $(e.target);
            var spaceId = car.data("spaceid");
            var space  = null;
            var userId = 0;

            if(!spaceId || spaceId <= 0 || isNaN(spaceId)) {
                Parking.Common.DisplayGlobalError(i18n.get("Main_ErrorSpaceNotAvailable"));
                return;
            }

            space = Parking.App.Data.Spaces.get(spaceId);

            if(Parking.App.Data.CurrentUserCheckIn.isCheckedIn()) {
                // Change to display a warning message
                Parking.Common.DisplayGlobalError(i18n.get("Main_InfoAlreadyCheckedIn"));
                return;
            }

            // Check that space isn't taken
            if(Parking.App.Data.CheckinsCurrent.isSpaceUsed(spaceId)) {
                Parking.Common.DisplayGlobalError(i18n.get("Main_ErrorSpaceNotAvailable"));
                return;
            }

            // Proceed to open confirmation box
            car.addClass("selected");

            if(Parking.App.Data.CurrentUser.isAdmin()) {
                // Display user selection box.
                var dialog = $(this.el).find(".js-confirmation-dialog");
                var msg = dialog.find(".js-message");
                msg.html(i18n.get("Main_ConfirmCheckinMessage").replace("{{Alias}}", space.get("Alias")));

                $(this.el).find(".js-confirmation-dialog").modal(); 
            } else {
                // Display confirm dialog.
                $(this.el).find(".js-confirmation-dialog").modal();
                
            }

        }

    });


})(jQuery);