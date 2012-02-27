/**
* Base namespace for the application.
*
* @package     Parking.UI.Scripts
* @author      The JSONs
* @copyright   2012 Propiertary 
*/

namespace("Parking.App.Collections");

(function ($, collections, undefined) {

    collections.Users = Backbone.Collection.extend({
        
        url: Parking.Configuration.APIEndpointUrl + "users/GetAll",

        model: Parking.App.Models.UserInformation,
        
        initialize: function() {
        },
         
        parse: function(response) { 
            if(response["Error"] == false) { 
                return response["Response"];
            }
        }
         

    });


})(jQuery, Parking.App.Collections);