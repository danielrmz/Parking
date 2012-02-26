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
    
    models.Space = Parking.App.Base.Model.extend({

        defaults: {            
            SpaceId: 0,
            PlaceId: 0,
            Alias: "",
            AccessTypeId: 0,
            OwnerId: 0,
            CreatedAt: new Date(),
            Deleted: false
        }
        
    });

})(jQuery, Parking.App.Models);