/**
* Base namespace for the application.
*
* @package     Parking.UI.Scripts
* @author      The JSONs
* @copyright   2012 Propiertary
*/

namespace("Parking.App.Views");

(function ($, undefined) {

    Parking.App.Views.Login = Backbone.View.extend({
        
        template: Parking.Configuration.ClientTemplatesUrl + "Account/Login.html",

        model: Parking.App.Data.CurrentUser,

        render: function (done) {
            var view = this; 

            if(Parking.App.Data.CurrentUser != null && Parking.App.Data.CurrentUser.get("IsAuthenticated")) {
                // Redirect to main page. 
                Parking.App.router.navigate("home", true);
                return;
            }
             
            Parking.App.Helpers.RenderViewTemplate.apply(this, arguments);
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
            
            $.ajax({type:'POST', 
                    url: form.attr("action"), 
                    data: params, 
                    success: function(data){ 
                
                        if(data.Error == false) {
                            form.find(".alert-error").hide(); 
                            Parking.App.Data.CurrentUser.save(data["Response"]);

                            // Redirect to the core app view
                            Parking.App.router.navigate("home", true);

                        } else {
                            submit.val(submit.data("click")).attr("disabled", false);

                            // Display error.
                            form.find(".alert-error .message").html(data["Response"]);
                            form.find(".alert-error").show();
                        }
                  }, 
                  error: function() { 
                    submit.val(submit.data("click")).attr("disabled", false);
                  }
          });

            return false;
        }

    });


})(jQuery);