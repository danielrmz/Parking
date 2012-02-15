/**
* User Information
*
* @package     Parking.UI.Scripts
* @author      The JSONs
* @copyright   2012
* @license     Propietary
*/

namespace("Parking.App.Models");

(function ($, models, undefined) {
    
    models.UserInformation = Backbone.Model.extend({
        SessionId: "",
        UserName: "", 
        Email: "",
        FirstName: "",
        LastName: "",
        ProfilePictureUrl: "",
        IsAuthenticated: false,
        Role: "",
        RoleId: 0,

        FullName: function() { 
            return this.FirstName + " " + this.LastName;
        }
    });

})(jQuery, Parking.App.Models);