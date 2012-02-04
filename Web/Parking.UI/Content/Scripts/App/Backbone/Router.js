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
    
    Parking.App.Router = Backbone.Router.extend({
      routes: {
        'login': 'login',
        'about': 'static',
        'help':  'static',
        'privacy': 'static'
      },
      login: function(){ 
        var route = this;
        var loginView = new Parking.App.Views.Login();
        loginView.render(function(el) {
            $("#main").html(el);
        });
      },
      static: function() {
        var route = this;
      }
    });
    
    Parking.App._router = new Parking.App.Router();

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