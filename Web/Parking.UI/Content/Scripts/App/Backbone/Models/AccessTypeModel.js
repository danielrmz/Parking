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
    
    models.AccessType = Backbone.Model.extend({

        defaults: {            
            AccessTypeName: ""       
        }
        
    });

})(jQuery, Parking.App.Models);