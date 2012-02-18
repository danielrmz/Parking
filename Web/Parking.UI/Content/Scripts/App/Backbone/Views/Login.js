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
        template: Parking.Configuration.ClientTemplatesUrl + "Account/Login.html",
        model: Parking.App._user,

        render: function (done) {
            var view = this;

            if(Parking.App._user != null && Parking.App._user.get("IsAuthenticated")) {
                // Redirect to main page.
                Parking.App._router.navigate("home");
                return;
            }
            
            Parking.Common.RenderViewTemplate.apply(this, arguments);
        },

        events: { 
           "click .js-submit": "submit"
        },
        
        "submit": function(e) {
            var form = $(this.el).find("form");
            var params = form.serialize();
            var self = this;
            var submit = form.find("input[type=submit]");

            submit.val(submit.data("afterclick")).attr("disabled", true);
            
            $.post(form.attr("action"), params, function(data){ 
                
                if(data.Error == false) {
                    form.find(".alert-error").hide();
                    Parking.App._user.save(data["Response"]);
                    
                    // Redirect to the core app view
                     Parking.App._router.navigate("home", true);

                } else {
                    submit.val(submit.data("click")).attr("disabled", false);
                    // Display error.
                    form.find(".alert-error .message").html(data["Response"]);
                    form.find(".alert-error").show();
                }
            });

            return false;
        }

    });


})(jQuery);