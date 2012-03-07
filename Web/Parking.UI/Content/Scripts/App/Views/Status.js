/**
* Base namespace for the application.
*
* @license Copyright 2012. The JSONS
*/

namespace("Parking.App.Views");

(function ($, parking, undefined) {
    var i18n           = parking["Resources"]["i18n"];
    var common         = parking["Common"];
    var config         = parking["Configuration"];
    var appbase        = parking["App"]["Base"];
    var appmodels      = parking["App"]["Models"]; 
    var appdata        = parking["App"]["Data"]; 
    var appviews       = parking["App"]["Views"];
    var appcollections = parking["App"]["Collections"];
    var apphelpers     = parking["App"]["Helpers"];

    /**
     * Status View
     *
     * @extends appbase.View
     */ 
    appviews.Status = appbase.View.extend({
        secure: true,

        "initialize": function() { 
            this.collection = appdata.CheckinsCurrent;
            this.collection.on("add", this.render, this);
            this.collection.on("remove", this.render, this);
        },

        /**
         * View's template
         * @type {string}
         */
        "template": config.ClientTemplatesUrl + "Parking/Status.html",

        /**
         * Model associated to the view
         * @type {appdata.CurrentUser}
         */
        "model": appdata.CurrentUser,

        /**
         * @inheritDoc
         */
        "render":  function() { 
            var spaceUsed = this.collection.length;
            var spaceTotal = appdata.Spaces.length; // - restricted ones...
            var spaceAvailable = spaceTotal - spaceUsed;

            this.model = new Backbone.Model();
            this.model.set({"spaceAvailable": spaceAvailable > 0});
            apphelpers.RenderViewTemplate.apply(this, arguments);
         },

        /**
         * @enum {string}
         */
        "events": {  
        }

    });


})(jQuery, Parking);