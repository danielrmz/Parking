/**
* Base namespace for the application.
*
* @package     Parking.UI.Scripts
* @author      The JSONs
* @copyright   2012
* @license     Propietary
*/

namespace("Parking.App.Views");

(function ($, undefined) {

    Parking.App.Views.Login = Backbone.View.extend({
        template: "/Content/Templates/Login.html",

        render: function (done) {
            var view = this;

            // Fetch the template, render it to the View element and call done.
            fetchTemplate(this.template, function (tmpl) {
                view.el.innerHTML = tmpl();

                done(view.el);
            });
        }
    });


})(jQuery);