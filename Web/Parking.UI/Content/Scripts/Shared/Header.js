/**
* Common scripts for the application. 
*
* @license Copyright 2012. The JSONS
*/
namespace("Parking.Common");

(function($, common, undefined) {

    /**
     * Provides the logic for the header clock
     */
    common.HeaderDate = function() {
        var d = new Date();

        var locale = Parking.Configuration['locale'] || 'en-US';
        var months = Parking.Resources.Months[locale];
        var supers = Parking.Resources.DaysSuperScripts[locale];
        var currentDay = d.getDate();
        var hrs = d.getHours();
        var ampm = hrs >= 12 ? "PM" : "AM";
        var mins = d.getMinutes();
        
        var hrs = hrs > 12 ? hrs - 12 : hrs;
        $(".js-day ").html(currentDay);
        $(".js-dayup").html(currentDay >= 4 ? supers[4] : supers[currentDay]);
        $(".js-monthyear").html(months[d.getMonth()] + " " + d.getFullYear());
        
        $(".js-time").html(hrs + "<span class='js-hrtick tick'>:</span>" + (mins < 10 ? "0" + mins : mins) + " " + ampm); 
    };

    /**
     * Provides the logic for the header clock
     */
    common.HeaderDateTick = function() {
        var el = $(".js-hrtick");
        if(el.html() == ":") {
            el.html("&nbsp;");
        } else {
            el.html(":");
        }
    };

})(jQuery, Parking["Common"]);
