/**
* Base namespace for the application.
*
* @license Copyright 2012. The JSONS
*/

namespace("Parking.App.Collections");
namespace("Parking.App.Views");
namespace("Parking.App.Data");
namespace("Parking.Resources");
namespace("Parking.Resources.i18n");

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
     * Dashboard View. Represents the whole strip under the header
     *
     * @extends appbase.View
     */
    appviews.Dashboard = appbase.View.extend({
        
        /**
         * @constructor
         */
        "initialize": function() { 
            this.model.on("change:IsBlocked", this.renderActionButton, this);
            this.model.on("change:IsAuthenticated", this.render, this);
            appdata.CurrentUserCheckIn.on("change:CheckInId", this.renderActionButton, this);
            appdata.CurrentUserCheckIn.on("change:EndTime", this.renderActionButton, this);

            this.initializeRecentCheckins();
        },

        /**
         * View's template
         * @type {string}
         */
        "template": config.ClientTemplatesUrl + "Shared/Dashboard.html",
        
        /**
         * Renders the template and the nested views. 
         */
        "render": function() {  
            apphelpers.RenderViewTemplate.apply(this, arguments); 
            
            this.renderActionButton();
            this.renderRecentCheckins(); 
        },

        /**
         * @enum {string}
         */
        "events": {
           "click .js-button-label": "doCheckout"
        },

        /**
         * Initializes the recent check in dashboard widget
         * @private
         */
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

        /**
         * Renders the recent check ins widget
         */
        renderRecentCheckins: function() {
            if(this.RecentCheckinsView) {  
                this.RecentCheckinsView.el = $(this.el).find(".panel-notifications");
                this.RecentCheckinsView.render();  
            }
        },

        /**
         * Renders the action button widget
         */
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

        /**
         * Does the checkout for the actual signed-in user
         */
        doCheckout: function() { 
            appdata.CurrentUserCheckIn.Checkout();
        }
         

    });


})(jQuery, Parking);