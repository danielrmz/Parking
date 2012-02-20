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
     
    common.SetupAjaxErrorHandler();

    $(function() { 
        common.HeaderDate();
        setInterval(common.HeaderDate, 60 * 1000);
        setInterval(common.HeaderDateTick, 1000);


        /* (function(){
 

    // LISTEN FOR MESSAGES
    PUBNUB.subscribe({
        channel    : "hello_world",      // CONNECT TO THIS CHANNEL.
 
        restore    : false,              // STAY CONNECTED, EVEN WHEN BROWSER IS CLOSED
                                         // OR WHEN PAGE CHANGES.
 
        callback   : function(message) { // RECEIVED A MESSAGE.
            alert(message)
        },
 
        disconnect : function() {        // LOST CONNECTION.
            alert(
                "Connection Lost." +
                "Will auto-reconnect when Online."
            )
        },
 
        reconnect  : function() {        // CONNECTION RESTORED.
            alert("And we're Back!")
        },
 
        connect    : function() {        // CONNECTION ESTABLISHED.
 
            PUBNUB.publish({             // SEND A MESSAGE.
                channel : "hello_world",
                message : "Hi from PubNub."
            })
 
        }
    })
 
})();*/
    });

})(jQuery, Parking.Common);
