/**
* Base namespace for the application.
*
* @package     Parking.UI.Scripts
* @author      The JSONs
* @copyright   2012 Propiertary
*/

namespace("Parking.App.Base");
namespace("Parking.App.Models");


(function ($, models, undefined) {
   
    models.UserInformation = Parking.App.Base.Model.extend({
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
        }

    });

})(jQuery, Parking.App.Models);