/**
* Routing definition for the Backbonejs App
*
* @license Copyright 2012. The JSONS
*/

namespace("Parking.App");
namespace("Parking.App.Data");

(function ($, parking, undefined) {
    var common  = parking["Common"];
    var app     = parking["App"];
    var appdata = parking["App"]["Data"];
    var apphelpers     = parking["App"]["Helpers"];
    var appmodels      = parking["App"]["Models"];
    var appviews       = parking["App"]["Views"];
    var appcollections = parking["App"]["Collections"];

    app.Router = Backbone.Router.extend({
      routes: {
        ''       : 'main',
        'home'   : 'main',
        'login'  : 'login',

        'about'  : 'static',
        'help'   : 'static',
        'terms'  : 'static',
        'privacy': 'static'
      },

      "main":  apphelpers.RenderBackbonePage,
      "login": apphelpers.RenderBackbonePage,
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
        loader.setLoaderText("Loading Spaces...");
        appdata.Spaces.fetch({"async": false});
        appdata.SpaceBlockings.fetch({"async": false});
        
        loader.setLoaderText("Loading current checkins...");
        appdata.CheckinsCurrent.fetch({"async": false});

        loader.setLoaderText("Loading Users...");
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

        // Initialize notification listener
        PUBNUB.subscribe({
                channel : "parking:notification:block",
                restore : false, 
                callback : function(msg) { 
                    console.log(msg);
                    if(msg["UserId"] == appdata.CurrentUser.get("UserId")) {
                        var modl = new Backbone.Model();
                        var baseUser = appdata.Users.get(msg["UserId"]);
                        var reqUser  = appdata.Users.get(msg["RequestingUser"]);

                        modl.set("UserName", baseUser.FullName());                        
                        modl.set("LeavingUserName", reqUser.FullName());

                        var notify = new appviews.BlockNotification({ "el": $(".js-placeholder-generic"), "model": modl});
                        notify.render();
                        $(notify.el).find(".modal").modal("show");
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