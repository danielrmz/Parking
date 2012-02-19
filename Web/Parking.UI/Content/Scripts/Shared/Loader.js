/**
* Common scripts for the application. 
*
* @package     Parking.UI.Scripts
* @author      The JSONs
* @copyright   2012 -
* @license     Propietary
*/
namespace("Parking.Common");

(function($, common, undefined) {
     
    $(function() { 
        common.HeaderDate();
        setInterval(common.HeaderDate, 60 * 1000);
        setInterval(common.HeaderDateTick, 1000);

        common.SetupAjaxErrorHandler();
    });

})(jQuery, Parking.Common);
