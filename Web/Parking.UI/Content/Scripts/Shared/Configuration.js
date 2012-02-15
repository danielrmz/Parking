﻿/**
* Configuration for the application. 
*
* @package     Parking.UI.Scripts
* @author      The JSONs
* @copyright   2012 -
* @license     Propietary
*/
namespace("Parking.Configuration");

(function($, config, undefined) {

    config.ClientTemplatesUrl = "/Content/Scripts/App/Backbone/Templates/";
    config.APIEndpointUrl = "/api/";
    config.i18nResourceEndpoint = "/Static/i18n";
    
    config.DateFormat = "MM/dd/yyyy";

    config.Mask = {
        SSN: "999-99-9999",
        Phone: "999-999-9999",
        PhoneExt: "999-999-9999? x9999",
        CheckNumber: "99999999",
        Date: "99/99/?9999",
        USZip: "99999-?9999"
    };


})(jQuery, Parking.Configuration);