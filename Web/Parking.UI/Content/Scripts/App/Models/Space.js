/**
* Space
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
    appmodels.Space = appbase.Model.extend({
        
        /**
         * Primary Key
         * @type {string}
         */
        "idAttribute": "SpaceId",
        
        /**
         * @enum {Object}
         */
        "defaults": {            
            "SpaceId": 0,
            "PlaceId": 0,
            "Alias": "",
            "AccessTypeId": 0,
            "OwnerId": 0,
            "CreatedAt": new Date(),
            "Deleted": false,
            "CssClass": "",
            "SpaceDirection": ""
        },
        
        /**
         * @constructor
         */
        "initialize": function() {
        }

    });

})(jQuery, Parking);