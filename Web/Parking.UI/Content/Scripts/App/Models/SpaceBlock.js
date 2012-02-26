/**
* Base namespace for the application.
*
* @package     Parking.UI.Scripts
* @author      The JSONs
* @copyright   2012
* @license     Propietary
*/
namespace("Parking.App.Base");
namespace("Parking.App.Models");

(function ($, models, undefined) {
    
    models.SpaceBlock = Parking.App.Base.Model.extend({

        defaults: {            
            BaseSpaceId: 0,
            BlockingSpaceId: 0
        }
        
    });

})(jQuery, Parking.App.Models);