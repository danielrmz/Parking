/**
* Base view for the backbone application views.
*
* @license Copyright 2012. The JSONS
*/

namespace("Parking.App.Base");

(function ($, parking) {
    var appbase    = parking["App"]["Base"];
    var apphelpers = parking["App"]["Helpers"];

    /**
     * Base view type.  
     *
     * @extends Backbone.View
     */
    appbase.View = Backbone.View.extend({
        "render": apphelpers.RenderViewTemplate
    });

})(jQuery, Parking);