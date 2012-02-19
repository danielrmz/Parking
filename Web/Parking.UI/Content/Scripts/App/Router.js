/**
* Base namespace for the application.
*
* @package     Parking.UI.Scripts
* @author      The JSONs
* @copyright   2012
* @license     Propietary
*/

namespace("Parking.App");

(function ($, undefined) {
    var replaceMain = function(el) { console.log(el); $("#main").html(el); };

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

      "main": function() {  
        var loginView = new Parking.App.Views.Main({el: $("#main")});
        loginView.render();
      },

      "login": function() { 
        var loginView = new Parking.App.Views.Login({el: $("#main")});
        loginView.render();
      },

      "static": function() {
        var path  = window.location.pathname.substring(1);
        var view  = new Parking.App.Views.Static({el: $("#main")}); 
        view.template = Parking.Configuration.ClientTemplatesUrl + "Static/" + path + ".html";
        view.render();
      }
    });
    
    // Required 
    Parking.App._user   = new Parking.App.Models.UserInformation(); 
    Parking.App._router = new Parking.App.Router();

   

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

            Parking.App._router.navigate(href, true);
        }
    });

})(jQuery);