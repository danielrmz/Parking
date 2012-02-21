/**
* Base namespace for the application.
*
* @package     Parking.UI.Scripts
* @author      The JSONs
* @copyright   2012
* @license     Propietary
*/

namespace("Parking.App.Models");


(function ($, models, undefined) {
   
    models.UserInformation = Backbone.Model.extend({
        urlRoot: Parking.Configuration.APIEndpointUrl + "users/",
        idAttribute: "UserId",

        defaults: { 
            UserId: 0,
            FirstName: "",
            LastName: "",
            Gender: "",
            PhoneHome: "",
            PhoneOffice: "",
            PhoneOfficeExtension: "",
            PhoneCel: "",
            ProfilePictureUrl: ""
        },

        parse: function(response) {  
            if(typeof response["Error"] == "undefined") {
                return response;
            }
            if(response["Error"] == false) { 
                return response["Response"];
            }
        }

    });

})(jQuery, Parking.App.Models);