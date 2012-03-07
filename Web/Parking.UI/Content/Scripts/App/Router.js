/**
* Routing definition for the Backbonejs App
*
* @license Copyright 2012. The JSONS
*/

namespace("Parking.App");
namespace("Parking.App.Data");
namespace("Parking.Resources.i18n");

(function ($, parking, undefined) {
    var common  = parking["Common"];
    var app     = parking["App"];
    var appdata = parking["App"]["Data"];
    var apphelpers     = parking["App"]["Helpers"];
    var appmodels      = parking["App"]["Models"];
    var appviews       = parking["App"]["Views"];
    var appcollections = parking["App"]["Collections"];
    var i18n    = parking["Resources"]["i18n"];

    app.Router = Backbone.Router.extend({
      routes: {
        ''       : 'main',
        'home'   : 'main',
        'login'  : 'login',

        'about'  : 'static',
        'help'   : 'static',
        'terms'  : 'static',
        'privacy': 'static',
        'status' : 'status'
      },

      "main":  apphelpers.RenderBackbonePage,
      "login": apphelpers.RenderBackbonePage,
      "status": apphelpers.RenderBackbonePage,
      "static":apphelpers.RenderStaticPage 

    });
    
    // Required, consider moving these to another part
    appdata.CurrentUser     = new appmodels.UserSession();
    appdata.Users           = new appcollections.Users();
    appdata.SpaceBlockings  = new appcollections.SpaceBlockings();
    appdata.CheckinsCurrent = new appcollections.CheckinsCurrent();
    appdata.Spaces          = new appcollections.Spaces();
        
    appdata.CurrentUser.on("pre-loggedin", function() {
        loader.show();

        // Get global data. 
        loader.setLoaderText(i18n.get("Loader_Spaces"));
        appdata.Spaces.fetch({"async": false});
        appdata.SpaceBlockings.fetch({"async": false});
        
        loader.setLoaderText(i18n.get("Loader_Checkins"));
        appdata.CheckinsCurrent.fetch({"async": false});

        loader.setLoaderText(i18n.get("Loader_Users"));
        appdata.Users.fetch({"async": false}); 

    });

    appdata.CurrentUser.on("post-loggedin", function() { 
        // Get users last checkin
        appdata.CurrentUserCheckIn = new appmodels.Checkin({ "UserId": appdata.CurrentUser.get("UserId") });
        appdata.CurrentUserCheckIn.fetch({"async": false});
        
        loader.hide();

        // Set global views. 
        (new appviews.HeaderUserInfo({ "model": appdata.CurrentUser, "el": $('.user-info .user') })).render(); 
        (new appviews.Dashboard({ "model": appdata.CurrentUser, "el": $('#dashboard') })).render(); 

        // Initialize general notification listener
        PUBNUB.subscribe({
                channel : "parking:notifications",
                restore : false, 
                callback : function(msg) { 
                    var typ = msg["Class"];
                   
                    if(msg["UserId"] == appdata.CurrentUser.get("UserId")) {
                            
                        switch(typ) {
                            case "BlockNotification":
                                    var modl = new Backbone.Model();
                                    var baseUser = appdata.Users.get(msg["UserId"]);
                                    var reqUser  = appdata.Users.get(msg["RequestingUser"]);

                                    modl.set("UserName", baseUser.FullName());                        
                                    modl.set("LeavingUserName", reqUser.FullName());

                                    var notify = new appviews.BlockNotification({ "el": $(".js-placeholder-generic"), "model": modl});
                                    notify.render();
                                    $(notify.el).find(".modal").modal("show");
                           
                            break;
                        
                            case "AvailableNotification":
                                var notify = new appviews.AvailableNotification({ "el": $(".js-placeholder-generic"), "model": modl});
                                    notify.render();
                                    $(notify.el).find(".modal").modal("show");
                            break;
                        }
                   }
                },
                disconnect : function() { },
                reconnect : function() { }, 
                connect : function() { }
        });
         
    });

    appdata.CurrentUser.on("initialized", function() { 
        appdata.Router = new app.Router();
        
        Backbone.history.start({ "pushState": true });
         
        apphelpers.SetBackboneLinks();

        loader.hide();
    });
    
    var loader = common.InitializeLoader();
    appdata.CurrentUser.load();

})(jQuery, Parking);