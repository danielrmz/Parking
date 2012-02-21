/**
* Base namespace for the application.
*
* @package     Parking.UI.Scripts
* @author      The JSONs
* @copyright   2012
* @license     Propietary
*/

namespace("Parking.App.Views");
namespace("Parking.App.Data");

(function ($, views, undefined) {

    views.Dashboard = Backbone.View.extend({

        template: Parking.Configuration.ClientTemplatesUrl + "Shared/Dashboard.html",
        
        render: function() {  
            Parking.App.Helpers.RenderViewTemplate.apply(this, arguments);

            if(Parking.App.Data.Users.length > 0) {
                this.RecentCheckinsView.el = this.$(".panel-notifications");
                this.RecentCheckinsView.render();
            }
        },

        initializeView: function() { 
            var self = this;
            Parking.App.Data.Users.on("reset", function() { self.render(); });

            // Initialize collection
            Parking.App.Data.RecentCheckins = new Parking.App.Collections.CheckinsHistory();
            
            this.RecentCheckinsView = new views.DashboardNotifications({collection: Parking.App.Data.RecentCheckins });
             
        },

        initialize: function() {
            var self = this;
            this.model.on("change", this.render, this);
            this.initializeView();
        },

        events: {
           
        }
         

    });


})(jQuery, Parking.App.Views);