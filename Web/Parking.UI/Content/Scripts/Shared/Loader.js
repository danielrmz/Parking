/**
* Things to load manually after all the other scripts have been loaded 
*
* @package     Parking.UI.Scripts
* @author      The JSONs
* @copyright   2012 -
* @license     Propietary
*/
namespace("Parking.Common");

(function($, common, undefined) {
     
    common.SetupAjaxErrorHandler();
     
    $(function() { 
        common.HeaderDate();
        setInterval(common.HeaderDate, 60 * 1000);
        setInterval(common.HeaderDateTick, 1000);
    });

})(jQuery, Parking.Common);
