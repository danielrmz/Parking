/**
* Access Type
*
* @license Copyright 2012. The JSONS
*/
namespace("Parking.App.Base");
namespace("Parking.App.Models");

(function ($, parking, undefined) {
    var config         = parking["Configuration"];
    var appbase        = parking["App"]["Base"];
    var appmodels      = parking["App"]["Models"]; 
    
    /**
     * AccessType model. 
     * Determines who can access the application
     *
     * @extends appbase.Model
     */
    appmodels.AccessType = appbase.Model.extend({
        
        /**
         * @enum {string|number|boolean|null|Date}
         */
        "defaults": {            
            "AccessTypeName": ""       
        },
        
        /**
         * @constructor
         */
        "initialize": function() {
        }

    });

})(jQuery, Parking);