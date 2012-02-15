/**
* Base namespace for the application.
*
* @package     Parking.UI.Scripts
* @author      The JSONs
* @copyright   2012
* @license     Propietary
*/

namespace("Parking.App.Views");

(function ($, undefined) {

    Parking.App.Views.Main = Backbone.View.extend({
        template: Parking.Configuration.ClientTemplatesUrl + "Parking/Home.html",
        
        model: Parking.App._user,

        render: Parking.Common.RenderViewTemplate,

        events: { 
            
        }
         

    });


})(jQuery);