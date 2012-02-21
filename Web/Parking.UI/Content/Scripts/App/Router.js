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

(function ($, undefined) {
    
    Parking.App.Router = Backbone.Router.extend({
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
        Parking.App.Helpers.SetWindowTitle(path);
        
        var view  = new Parking.App.Views.Static({el: $("#main")});
        view.template = Parking.Configuration.ClientTemplatesUrl + "Static/" + path + ".html";
        view.render();
      }
    });
    
    // Required 
    Parking.App._user   = new Parking.App.Models.UserSession();
    
    Parking.App._user.on("loggedin", function() { 
        Parking.App.Data.Users.fetch();
         
        // Set global views. 
        (new Parking.App.Views.HeaderUserInfo({ model: Parking.App._user, el: $('.user-info .user') })).render(); 
        (new Parking.App.Views.Dashboard({model: Parking.App._user, el: $('#dashboard') })).render(); 
    });

    Parking.App._user.on("initialized", function() { 
        Parking.App.Data.Users = new Parking.App.Collections.Users();
        
        Parking.App.router = new Parking.App.Router();
        
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

                Parking.App.router.navigate(href, true);
            }
        });
    });
    
    Parking.App._user.load();

})(jQuery);