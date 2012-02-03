/**
* Configuration for the application. 
*
* @package     Parking.UI.Scripts
* @author      The JSONs
* @copyright   2012 -
* @license     Propietary
*/
namespace("Parking.Configuration");

(function($, undefined) {

    //Default configuration for loading partials views
    jQuery.ajaxSetup({
        type: "GET",
        dataType: "html",
        error: function (req, status, error) {
            if (Parking.Common && Parking.Common.AjaxErrorHandler) {
                Parking.Common.AjaxErrorHandler(req, status, error);
            }
        }
    });

    Parking.Configuration.DateFormat = "MM/dd/yyyy";

    Parking.Configuration.Mask = {
        SSN: "999-99-9999",
        Phone: "999-999-9999",
        PhoneExt: "999-999-9999? x9999",
        CheckNumber: "99999999",
        Date: "99/99/?9999",
        USZip: "99999-?9999"
    };

})($);