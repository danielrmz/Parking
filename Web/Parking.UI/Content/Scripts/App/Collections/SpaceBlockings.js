/**
* Base namespace for the application.
*
* @package     Parking.UI.Scripts
* @author      The JSONs
* @copyright   2012 Propiertary
*/
namespace("Parking.App.Base");
namespace("Parking.App.Collections");

(function ($, collections, undefined) {
    
    collections.SpaceBlockings = Parking.App.Base.Collection.extend({

        model : Parking.App.Models.SpaceBlock,

        url: Parking.Configuration.APIEndpointUrl + "spaces/blockings",
 
        getBlockingIdsBySpaceId: function(spaceId) { 
            return this.filter(function(model){ return model.get("BaseSpaceId") == spaceId}).map(function(model) { return model.get("BlockingSpaceId"); });
        }       
    });

})(jQuery, Parking.App.Collections);