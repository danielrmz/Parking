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
    
    collections.Spaces = Parking.App.Base.Collection.extend({

        model : Parking.App.Models.Space,

        url: Parking.Configuration.APIEndpointUrl + "spaces/GetAllByPlaceId/1",

        getDeleted: function() {
            return this.filter(function(space){ return photo.get('Deleted'); });
        },

        getAll: function() {
            return this.without.apply(this, this.getDeleted());
        }
        
    });

})(jQuery, Parking.App.Collections);