/**
* Base namespace for the application.
*
* @license Copyright 2012. The JSONS
*/

namespace("Parking.App.Collections");
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

    appviews.Dashboard = appbase.View.extend({

        template: config.ClientTemplatesUrl + "Shared/Dashboard.html",
        
        render: function() {  
            apphelpers.RenderViewTemplate.apply(this, arguments); 
            
            this.renderActionButton();
            this.renderRecentCheckins(); 
        },

        initializeRecentCheckins: function() { 
            var self = this; 
            if(!this.IsInitialized) {
                appdata.Users.on("reset", function() { self.renderRecentCheckins(); });
            
                // Initialize collection
                appdata.RecentCheckins = new appcollections.CheckinsHistory();
            
                this.RecentCheckinsView = new appviews.DashboardNotifications({collection: appdata.RecentCheckins });  
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
            if(appdata.CurrentUserCheckIn == null 
                || appdata.CurrentUserCheckIn.get("EndTime") != null 
                || appdata.CurrentUserCheckIn.get("CheckInId") == "") {
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
            appdata.CurrentUserCheckIn.on("change:CheckInId", this.renderActionButton, this);
            appdata.CurrentUserCheckIn.on("change:EndTime", this.renderActionButton, this);

            this.initializeRecentCheckins();
        },

        events: {
           "click .js-button-label": "doCheckout"
        },

        doCheckout: function() { 
            appdata.CurrentUserCheckIn.Checkout();
        }
         

    });


})(jQuery, Parking);