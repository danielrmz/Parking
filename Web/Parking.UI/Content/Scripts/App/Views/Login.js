/**
* Base namespace for the application.
*
* @license Copyright 2012. The JSONS
*/

namespace("Parking.App.Views");

(function ($, parking, undefined) {
    var i18n           = parking["Resources"]["i18n"];
    var common         = parking["Common"];
    var config         = parking["Configuration"];
    var appbase        = parking["App"]["Base"];
    var appmodels      = parking["App"]["Models"]; 
    var appdata        = parking["App"]["Data"]; 
    var appviews       = parking["App"]["Views"];
    var appcollections = parking["App"]["Collections"];
    var apphelpers     = parking["App"]["Helpers"];

    appviews.Login = appbase.View.extend({
        
        template: config.ClientTemplatesUrl + "Account/Login.html",

        model: appdata.CurrentUser,

        render: function (done) {
            var view = this; 

            if(appdata.CurrentUser != null && appdata.CurrentUser.get("IsAuthenticated")) {
                // Redirect to main page. 
                appdata.Router.navigate("home", true);
                return;
            }
             
            apphelpers.RenderViewTemplate.apply(this, arguments);
        },

        events: { 
           "click .js-submit": "submit"
        },
        
        "submit": function(e) {
            var form   = $(this.el).find("form");
            var params = form.serialize();
            var self   = this;
            var submit = form.find("input[type=submit]");

            submit.val(submit.data("afterclick")).attr("disabled", true);
            
            $.ajax({type:'POST', 
                    url: form.attr("action"), 
                    data: params, 
                    success: function(data){ 
                
                        if(data.Error == false) {
                            form.find(".alert-error").hide(); 
                            appdata.CurrentUser.save(data["Response"]);

                            // Redirect to the core app view
                            appdata.Router.navigate("home", true);

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


})(jQuery, Parking);