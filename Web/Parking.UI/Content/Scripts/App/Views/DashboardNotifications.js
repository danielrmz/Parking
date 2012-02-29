/**
* Base namespace for the application.
*
* @license Copyright 2012. The JSONS
*/

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

    appviews.DashboardNotifications = appbase.View.extend({

        template: config.ClientTemplatesUrl + "Shared/DashboardNotifications.html",

        singleTemplate: config.ClientTemplatesUrl + "Shared/DashboardNotification.html",

        render: apphelpers.RenderViewTemplate,

        onAdd: function(checkin) { 
            var ul = $(this.el).find("ul");  

            apphelpers.RenderTemplate(this.singleTemplate, checkin.toJSON(), function(html) { 
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


})(jQuery, Parking);