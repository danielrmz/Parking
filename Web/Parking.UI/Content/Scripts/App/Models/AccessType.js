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
    
    models.AccessType = Parking.App.Base.Model.extend({

        defaults: {            
            AccessTypeName: ""       
        }
        
    });

})(jQuery, Parking.App.Models);