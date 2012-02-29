/**
* User information
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
    * User information for a particular user
    * @extends Parking.App.Base.Model
    */
    appmodels.UserInformation = appbase.Model.extend({
        
        /**
         * Model's base endpoint
         * @type {string}
         */
        "urlRoot": config.APIEndpointUrl + "users/",
        
        /**
         * Primary Key
         * @type {string}
         */
        "idAttribute": "UserId",

        /**
         * @enum {Object}
         */
        "defaults": { 
            "UserId": 0,
            "FirstName": "",
            "LastName": "",
            "Gender": "",
            "PhoneHome": "",
            "PhoneOffice": "",
            "PhoneOfficeExtension": "",
            "PhoneCel": "",
            "ProfilePictureUrl": ""
        },

        /**
         * @constructor
         */
        "initialize": function() {
        }

    });

})(jQuery, Parking);