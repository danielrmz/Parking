/**
* Resources for the application. 
*
* @license Copyright 2012. The JSONS
*/
namespace("Parking.Resources");
namespace("Parking.Resources.i18n");

(function($, resources, undefined) {

    resources.Months = {
                        "en-US": ['Jan','Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sept', 'Oct', 'Nov', 'Dec'],
                        "es-MX": ['Ene','Feb', 'Mar', 'Abr', 'May', 'Jun', 'Jul', 'Ago', 'Sep', 'Oct', 'Nov', 'Dic']
                        };

    resources.DaysSuperScripts = {
                        "en-US": ['','st','nd','rd','th'],
                        "es-MX": ['','ro','do','ro','to']
                        };
    
    resources.i18n.get = function(id) { 
        var locale          = Parking.Configuration["locale"] || "en-US";
        var localeResources = Parking.Resources["i18n"][locale] || {};
        var resrc = localeResources[id] || id;
        return resrc;   
    };

})(jQuery, Parking.Resources);