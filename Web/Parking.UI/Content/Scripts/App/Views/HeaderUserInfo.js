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

    appviews.HeaderUserInfo = appbase.View.extend({

        template: config.ClientTemplatesUrl + "Shared/HeaderUserInfo.html",
        
        render: apphelpers.RenderViewTemplate,

        initialize: function() {
            this.model.on("change", this.render, this);
        },

        events: {
           "click .js-logout": "logout"
        },

        "logout": function(e) {
            appdata.CurrentUser.destroy(function() {  
                appdata.Router.navigate("login", true);
            }); 
            
            return false;
        }

    });


})(jQuery, Parking);