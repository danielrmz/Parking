/**
* Space Blocking
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
     *
     * @extends Parking.App.Base.Model
     */
    appmodels.SpaceBlock = appbase.Model.extend({

        /**
         * @enum {Object}
         */
        "defaults": {            
            "BaseSpaceId": 0,
            "BlockingSpaceId": 0
        },
        
        /**
         * @constructor
         */
        "initialize": function() {
        }

    });

})(jQuery, Parking);