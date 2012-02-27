/**
* Base namespace for the application.
*
* @package     Parking.UI.Scripts
* @author      The JSONs
* @copyright   2012 Propiertary
*/

namespace("Parking.App.Views");
namespace("Parking.App.Data");

(function ($, views, undefined) {
    var i18n = Parking.Resources.i18n;

    views.Dashboard = Backbone.View.extend({

        template: Parking.Configuration.ClientTemplatesUrl + "Shared/Dashboard.html",
        
        render: function() {  
            Parking.App.Helpers.RenderViewTemplate.apply(this, arguments); 
            
            this.renderActionButton();
            this.renderRecentCheckins(); 
        },

        initializeRecentCheckins: function() { 
            var self = this; 
            if(!this.IsInitialized) {
                Parking.App.Data.Users.on("reset", function() { self.renderRecentCheckins(); });
            
                // Initialize collection
                Parking.App.Data.RecentCheckins = new Parking.App.Collections.CheckinsHistory();
            
                this.RecentCheckinsView = new views.DashboardNotifications({collection: Parking.App.Data.RecentCheckins });  
                this.IsInitialized = true;
                this.render();
            }
        },

        renderRecentCheckins: function() {
            if(this.RecentCheckinsView) {  
                this.RecentCheckinsView.el = $(this.el).find(".panel-notifications");
                this.RecentCheckinsView.render();  
            }
        },

        renderActionButton: function() { 
            var dashboard = $(this.el);
            var btnGroup  = dashboard.find(".js-button .js-button-group");
            var btnAction = btnGroup.find(".js-button-label");
            var isBlocked = this.model.get("IsBlocked");  
            
            // If the user does not have a check in or the last one is already closed, do not render the button.
            if(Parking.App.Data.CurrentUserCheckIn == null 
                || Parking.App.Data.CurrentUserCheckIn.get("EndTime") != null 
                || Parking.App.Data.CurrentUserCheckIn.get("CheckInId") == "") {
                btnGroup.hide();
                return;
            }

            btnAction.text(isBlocked ? i18n.get("Dashboard_ActionBtn_Blocked") : i18n.get("Dashboard_ActionBtn_Normal"));
            btnGroup.removeClass("green").removeClass("red");
            btnGroup.addClass(isBlocked ? "red" : "green");
            btnGroup.show();
        },

        initialize: function() { 
            this.model.on("change:IsBlocked", this.renderActionButton, this);
            this.model.on("change:IsAuthenticated", this.render, this);
            Parking.App.Data.CurrentUserCheckIn.on("change:CheckInId", this.renderActionButton, this);
            Parking.App.Data.CurrentUserCheckIn.on("change:EndTime", this.renderActionButton, this);

            this.initializeRecentCheckins();
        },

        events: {
           "click .js-button-label": "doCheckout"
        },

        doCheckout: function() { 
            Parking.App.Data.CurrentUserCheckIn.Checkout();
        }
         

    });


})(jQuery, Parking.App.Views);