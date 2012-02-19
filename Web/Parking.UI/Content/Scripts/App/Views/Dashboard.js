/**
* Base namespace for the application.
*
* @package     Parking.UI.Scripts
* @author      The JSONs
* @copyright   2012
* @license     Propietary
*/

namespace("Parking.App.Views");

(function ($, views, undefined) {

    views.Dashboard = Backbone.View.extend({

        template: Parking.Configuration.ClientTemplatesUrl + "Shared/Dashboard.html",
        
        render: Parking.Common.RenderViewTemplate,

        initialize: function() {
            this.model.on("change", this.render, this);
        },

        events: {
           
        },
         

    });


})(jQuery, Parking.App.Views);