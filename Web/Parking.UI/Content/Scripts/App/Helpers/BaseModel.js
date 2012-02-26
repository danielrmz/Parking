/**
* Base namespace for the application.
*
* @package     Parking.UI.Scripts
* @author      The JSONs
* @copyright   2012
* @license     Propietary
*/

namespace("Parking.App.Base");


(function ($, base, undefined) {
   
    base.Model = Backbone.Model.extend({
        parse: function(response) {  
            if(typeof response["Error"] == "undefined") {
                return response;
            }
            if(response["Error"] == false) { 
                return response["Response"];
            }
        }
    });

})(jQuery, Parking.App.Base);