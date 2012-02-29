/**
* Base model type for the application
*
* @license Copyright 2012. The JSONS
*/

namespace("Parking.App.Base");


(function ($, parking) {
    var appbase = parking["App"]["Base"];
    
    /**
     * Base model type.  
     *
     * @extends Backbone.Model
     */
    appbase.Model = Backbone.Model.extend({

        /**
         * Parses the API's returned object.
         * @param {Object} response API Response
         * @return {Object}
         */
        "parse": function(response) {  
            if(typeof response["Error"] == "undefined") {
                return response;
            }
            if(response["Error"] == false) { 
                return response["Response"];
            }
            return null;
        }
    });

})(jQuery, Parking);