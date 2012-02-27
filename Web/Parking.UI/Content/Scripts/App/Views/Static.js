/**
* Base namespace for the application.
*
* @package     Parking.UI.Scripts
* @author      The JSONs
* @copyright   2012 Propiertary 
*/

namespace("Parking.App.Views");

(function ($, undefined) {

    Parking.App.Views.Static = Backbone.View.extend({
        render: Parking.App.Helpers.RenderViewTemplate
    });

})(jQuery);