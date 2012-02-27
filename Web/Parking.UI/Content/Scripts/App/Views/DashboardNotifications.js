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

    views.DashboardNotifications = Backbone.View.extend({

        template: Parking.Configuration.ClientTemplatesUrl + "Shared/DashboardNotifications.html",
        singleTemplate: Parking.Configuration.ClientTemplatesUrl + "Shared/DashboardNotification.html",

        render: Parking.App.Helpers.RenderViewTemplate,

        onAdd: function(checkin) { 
            var ul = $(this.el).find("ul");  

            Parking.App.Helpers.RenderTemplate(this.singleTemplate, checkin.toJSON(), function(html) { 
                var el = $(html);
                el.hide();
                ul.prepend(el);
                el.fadeIn(500);
            });
        },

        onRemove: function(checkin) {
            var element = $(this.el).find("[data-notificationid="+checkin.get("NotificationId")+"].js-checkin-notification"); 
            element.addClass("transitioning").animate({ left: "-=50", opacity: 0, position: "absolute" }, 500, function(){element.remove();});

        },

        initialize: function() { 
            var self = this;
            this.collection.on("reset", function() { self.render(); });
            this.collection.on("add", function() { self.onAdd.apply(self, arguments); });
            this.collection.on("remove", function() {  self.onRemove.apply(self, arguments);   });
            this.collection.fetch();
        },

        events: {
           
        }
         

    });


})(jQuery, Parking.App.Views);