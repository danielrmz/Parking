*
* @package     Parking.UI.Scripts
* @author      The JSONs
* @copyright   2012
* @license     Propietary
*/

namespace("Parking.App.Collections");

(function ($, collections, undefined) {
    
    collections.CheckinCollection = Backbone.Collection.extend({

        model : Parking.App.Models.Checkin
//        ,

//        initialize: function(){
//            this.bind("change:src", function(){
//                var src = this.get("src"); 
//                console.log('Image source updated to ' + src);
//            });
//        },
        
    });

})(jQuery, Parking.App.Collections);

