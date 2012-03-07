/**
* Configuration for the application. 
* 
* @license Copyright 2012. The JSONS
*/
namespace("Parking.Configuration");

(function ($, config, undefined) {

    /**
     * The client templates remote location. If they are not found,
     * they can be fetched from this url.
     *
     * @const
     * @type {string}
     */
    config.ClientTemplatesUrl = "/Content/Templates/";

    /**
     * The remote location of myplace's API
     * 
     * @const 
     * @type {string}
     */
    config.APIEndpointUrl = "/api/";

    /**
     * Debug mode
     *
     * @const
     * @type {boolean}
     */
    config.Debug = true;

    /**
     * Format in which the date should be formatted
     * 
     * @const
     * @type {string}
     */
    config.DateFormat = "MM/dd/yyyy";

    /**
     * Set of masks
     * @enum {string}
     */
    config.Mask = { 
        Phone: "999-999-9999",
        PhoneExt: "999-999-9999? x9999",
        Date: "99/99/?9999"
    };

    config.locale = $.cookie('ParkingLocale');   

})(jQuery, Parking["Configuration"]);