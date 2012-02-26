/**
* Base namespace for the application.
*
* @package     Parking.UI.Scripts
* @author      The JSONs
* @copyright   2012
* @license     Propietary
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
    app.Data.CurrentUser   = new app.Models.UserSession();
    
    app.Data.CurrentUser.on("pre-loggedin", function() {
        app.Data.Users = new app.Collections.Users();
        app.Data.SpaceBlockings = new app.Collections.SpaceBlockings();
        app.Data.CheckinsCurrent = new app.Collections.CheckinsCurrent();
        app.Data.Spaces = new app.Collections.Spaces();
        
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
        
        // Start pushState
        Backbone.history.start({ pushState: true });
         
        $(document).on("click", "a:not([data-bypass])", function(evt) {
            var href = $(this).attr("href");
            var protocol = this.protocol + "//";

            // Ensure the protocol is not part of URL, meaning its relative.
            if (href && href.slice(0, protocol.length) !== protocol) {
                // Stop the default event to ensure the link will not cause a page
                // refresh.
                evt.preventDefault();

                app.router.navigate(href, true);
            }
        });
    });
    
    app.Data.CurrentUser.load();

})(jQuery, Parking.App);