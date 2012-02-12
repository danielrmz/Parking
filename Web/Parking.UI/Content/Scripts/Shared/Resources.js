/**
* Resources for the application. 
*
* @package     Parking.UI.Scripts
* @author      The JSONs
* @copyright   2012 -
* @license     Propietary
*/
namespace("Parking.Resources");

(function($, resources, undefined) {

    resources.LoadingImage = '<img alt="Loading..." class="loading" src="/Content/Images/ajax-loader.gif" />';
    resources.SmallLoadingImage = '<img alt="Loading..." class="loading" src="/Content/Images/ajax-loader-small.gif" />';

    resources.Icons = {
        Ok: '/Content/Images/Icons/notification-ok.png',
        Info: '/Content/Images/Icons/notification-info.png',
        Alert: '/Content/Images/Icons/notification-alert.png',
        Error: '/Content/Images/Icons/notification-error.png'
    };

    resources.Months = {
                        "en-US": ['Jan','Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sept', 'Oct', 'Nov', 'Dec'],
                        "es-MX": ['Ene','Feb', 'Mar', 'Abr', 'May', 'Jun', 'Jul', 'Ago', 'Sep', 'Oct', 'Nov', 'Dic']
                        };

    resources.DaysSuperScripts = {
                        "en-US": ['','st','nd','rd','th'],
                        "es-MX": ['','ro','do','ro','to']
                        };
    
})(jQuery, Parking.Resources);