/**
* Base namespace for the application.
*
* @package     Parking.UI.Scripts
* @author      The JSONs
* @copyright   2012 Propiertary 
*/

namespace("Parking.App");
namespace("Parking.App.Data");

(function ($, app, undefined) {
    
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

      "main":  Parking.App.Helpers.RenderBackbonePage,
      "login": Parking.App.Helpers.RenderBackbonePage,

      "static": function() {
        var path  = Backbone.history.fragment;
        app.Helpers.SetWindowTitle(path);
        
        var view  = new app.Views.Static({el: $("#main")});
        view.template = Parking.Configuration.ClientTemplatesUrl + "Static/" + path + ".html";
        view.render();
      }
    });
    
    // Required 
    app.Data.CurrentUser     = new app.Models.UserSession();
    app.Data.Users           = new app.Collections.Users();
    app.Data.SpaceBlockings  = new app.Collections.SpaceBlockings();
    app.Data.CheckinsCurrent = new app.Collections.CheckinsCurrent();
    app.Data.Spaces          = new app.Collections.Spaces();
        
    app.Data.CurrentUser.on("pre-loggedin", function() {
        
        // Get global data. 
        app.Data.SpaceBlockings.fetch({async: false});
        app.Data.CheckinsCurrent.fetch({async: false});
        app.Data.Users.fetch({async: false}); 
        app.Data.Spaces.fetch({async: false});
    });

    app.Data.CurrentUser.on("post-loggedin", function() { 
        // Get users last checkin
        Parking.App.Data.CurrentUserCheckIn = new Parking.App.Models.Checkin({ UserId: Parking.App.Data.CurrentUser.get("UserId") });
        Parking.App.Data.CurrentUserCheckIn.fetch({async: false});

        // Set global views. 
        (new app.Views.HeaderUserInfo({ model: app.Data.CurrentUser, el: $('.user-info .user') })).render(); 
        (new app.Views.Dashboard({model: app.Data.CurrentUser, el: $('#dashboard') })).render(); 
    });

    app.Data.CurrentUser.on("initialized", function() { 
        app.router = new app.Router();
        
        Backbone.history.start({ pushState: true });
         
        app.Helpers.SetBackboneLinks();
    });
    
    app.Data.CurrentUser.load();

})(jQuery, Parking.App);