/**
* Resources for the application. 
*
* @license Copyright 2012. The JSONS
*/
namespace("Parking.Resources");
namespace("Parking.Resources.i18n");
namespace("Parking.Configuration");

(function($, parking) {
    var config    = parking["Configuration"]; 
    var resources = parking["Resources"];

    resources.Months = {
                        "en-US": ['Jan','Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sept', 'Oct', 'Nov', 'Dec'],
                        "es-MX": ['Ene','Feb', 'Mar', 'Abr', 'May', 'Jun', 'Jul', 'Ago', 'Sep', 'Oct', 'Nov', 'Dic']
                        };

    resources.DaysSuperScripts = {
                        "en-US": ['','st','nd','rd','th'],
                        "es-MX": ['','ro','do','ro','to']
                        };
    
    resources.i18n.get = function(id) { 
        var locale          = config["locale"] || "en-US";
        var localeResources = resources["i18n"][locale] || {};
        var resrc = localeResources[id] || id;
        return resrc;   
    };

})(jQuery, Parking);