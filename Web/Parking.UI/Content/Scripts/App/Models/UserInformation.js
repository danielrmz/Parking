/**
* User information
*
* @license Copyright 2012. The JSONS
*/
namespace("Parking.App.Base");
namespace("Parking.App.Models");

(function ($, parking) {
    var config         = parking["Configuration"];
    var appbase        = parking["App"]["Base"];
    var appmodels      = parking["App"]["Models"];

   /**
    * User information for a particular user
    * @extends appbase.Model
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
         * @enum {string|number|boolean|null|Date}
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
            "ProfilePictureUrl": "",
            "ContactEmail":""
        },

        /**
         * @constructor
         */
        "initialize": function() {
        },

        FullName: function() {  
            return this.get("FirstName") + " " + this.get("LastName");
        }

    });

})(jQuery, Parking);