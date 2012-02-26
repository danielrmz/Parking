/**
* Base namespace for the application.
*
* @package     Parking.UI.Scripts
* @author      The JSONs
* @copyright   2012
* @license     Propietary
*/

namespace("Parking.App.Base");


(function ($, base, undefined) {

    /**
     * Calls a parent method.
     */
    Backbone.Collection.prototype._super = function(funcName){
        return this.constructor.__super__[funcName].apply(this, _.rest(arguments));
    }

    /**
     * Base collection type.  
     */
    base.Collection = Backbone.Collection.extend({
        parse: function(response) {  
            if(typeof response["Error"] == "undefined") {
                return response;
            }
            if(response["Error"] == false) { 
                return response["Response"];
            } else {
                Parking.Common.DisplayGlobalError(response["Response"] + "<br /><br /><strong>StackTrace: </strong><br /><span style='font-size:10px;'>" + response["StackTrace"].replace("\n","<br />") + "</span>");
            }
        }
    });

    /**
     * Implements the collection base object and adds 
     * real-time listener handlers.
     */
    base.ListenerCollection = Backbone.Collection.extend({
        
        channel: function() { 
            return ""; 
        },

        onMessageReceived: function(message) {  },

        super: function() { 
            return base.ListenerCollection.prototype;
        },

        initialize: function() {
            this.listen();
        },
        
        parse: function(response) {  
            if(typeof response["Error"] == "undefined") {
                return response;
            }

            if(response["Error"] == false) { 
                return response["Response"];
            }
        },

        listen: function() {
            var self = this; 
            
            PUBNUB.subscribe({
                channel : self.channel,
                restore : false, 
                callback : function(msg) { self.onMessageReceived(msg); },
                disconnect : function() { },
                reconnect : function() { }, 
                connect : function() { }
            });
             
        }
    });

})(jQuery, Parking.App.Base);
 
        
         