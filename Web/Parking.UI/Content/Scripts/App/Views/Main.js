/**
* Base namespace for the application.
*
* @package     Parking.UI.Scripts
* @author      The JSONs
* @copyright   2012
* @license     Propietary
*/

namespace("Parking.App.Views");
namespace("Parking.App.Data");

(function ($, undefined) {

    Parking.App.Views.Main = Backbone.View.extend({
        template: Parking.Configuration.ClientTemplatesUrl + "Parking/Home.html",
        
        model: app.Data.CheckinsCurrent,

        initialize: function() {
            //Parking.App.Data.CurrentCheckins = new Parking.App.Collections.CheckinsCurrent();
            //Parking.App.Data.Checkin = new Parking.App.Models.Checkin();
        },

        render: function() {  
            if(Parking.App.Data.CurrentUser == null || !Parking.App.Data.CurrentUser.get("IsAuthenticated")) {
                // Redirect to main page. 
                Parking.App.router.navigate("login", true);
                return;
            }

            Parking.App.Helpers.RenderViewTemplate.apply(this, arguments);
        },

        events: { 
            "click .car": "checkin"
        },
         
        "checkin": function(e) {

            var myCheckin = new Parking.App.Models.Checkin({ 
                SpaceId: 2,
                ReservationId: null,
                RegistredFrom: 1,
                RegistredBy: 39,
                StartTime: new Date()
            });

            //myCheckin.save();

            Parking.App.Data.CurrentCheckins.create( myCheckin );

            var myAttributes = myCheckin.toJSON();
            console.log(myAttributes);

            //Backbone.emulateHTTP = true;
            //Backbone.emulateJSON = true;
//            Parking.App.Data.CurrentCheckins.create( JSON.stringify( myCheckin ) );
            
            //checkinCollection.add([myCheckin]);

//            var form = $(this.el).find("form");
//            var params = form.serialize();
//            var self = this;
//            var submit = form.find("input[type=submit]");

//            submit.val(submit.data("afterclick")).attr("disabled", true);
//            
//            $.post(form.attr("action"), params, function(data){ 
//                
//                if(data.Error == false) {
//                    form.find(".alert-error").hide();
//                    Parking.App._user.set(data["Response"]);
//                    Parking.App._views.HeaderUserInfo.render();

//                    // Redirect to the core app view
//                     Parking.App._router.navigate("home", true);

//                } else {
//                    submit.val(submit.data("click")).attr("disabled", false);
//                    // Display error.
//                    form.find(".alert-error .message").html(data["Response"]);
//                    form.find(".alert-error").show();
//                }
//            });

//            return false;
        }

    });


})(jQuery);