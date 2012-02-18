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

    Parking.App.Views.Main = Backbone.View.extend({
        template: Parking.Configuration.ClientTemplatesUrl + "Parking/Home.html",
        
        model: Parking.App._user,

        el: $('#results'),

        render: Parking.Common.RenderViewTemplate,

        events: { 
            "click .car": "checkin",
//            "click .a2": "checkin",
//            "click .a3": "checkin",
//            "click .a4": "checkin",
//            "click .a5": "checkin",
//            "click .a6": "checkin",
//            "click .a7": "checkin",
//            "click .a8": "checkin",
//            "click .a9": "checkin",
//            "click .a10": "checkin",
//            "click .a11": "checkin",
//            "click .a12": "checkin",
//            "click .a13": "checkin",
//            "click .a14": "checkin",

//            "click .b1": "checkin",
//            "click .b2": "checkin",
//            "click .b3": "checkin",
//            "click .b4": "checkin",
//            "click .b5": "checkin",
//            "click .b6": "checkin",
//            "click .b7": "checkin",
//            "click .b8": "checkin",
//            "click .b9": "checkin",
//            "click .b10": "checkin",
//            "click .b11": "checkin",

//            "click .c1": "checkin",
//            "click .c2": "checkin",
//            "click .c3": "checkin",
//            "click .c4": "checkin",
//            "click .c5": "checkin",
//            "click .c6": "checkin",
//            "click .c7": "checkin",
//            "click .c8": "checkin",
//            "click .c9": "checkin"
            //this.css class
        },
         
        "checkin": function(e) {

            var myCheckin = new Checkin();
            
            myCheckin.set({ SpaceId: 0,
                ReservationId: "",
                RegistredFrom: 0,
                RegistredBy: 0
                });

            var myAttributes = myCheckin.toJSON();
            console.log(myAttributes);
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