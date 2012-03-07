/**
* Things to load manually after all the other scripts have been loaded 
*
* @license Copyright 2012. The JSONS
*/
namespace("Parking.Common");

(function ($, parking, undefined) {
    var common   = parking["Common"]; 

    common.SetupAjaxErrorHandler();
     
    $(function() { 
        common.HeaderDate();
        common.setLanguageVerbiage(); 
        setInterval(common.HeaderDate, 60 * 1000);
        setInterval(common.HeaderDateTick, 1000);
        setInterval(function() { $("[data-time]").each(function() { $(this).html(common.FormatTimeAgo($(this).data("time"))); });  }, 60000); 
    });

})(jQuery, Parking);
