/**
* Base class for the collection stubs for this app
*
* =reference PUBNUB.js
*
* @license Copyright 2012. The JSONS
*/

namespace("Parking.App.Base");

(function ($, parking, undefined) {
    var common  = parking["Common"];
    var appbase = parking["App"]["Base"];

    /**
     * Calls a parent method.
     * Extends Backbone's base collection prototype chain
     *
     * @param {string} funcName Function's name to be called from the parent. 
     * @return {Object}
     */
    Backbone.Collection.prototype._super = function(funcName){
        return this.constructor.__super__[funcName].apply(this, _.rest(arguments));
    }

    /**
     * Base collection type.  
     *
     * @extends Backbone.Collection
     */
    appbase.Collection = Backbone.Collection.extend({
        
        /**
         * Initializes the collection class
         * @constructor
         */
        "initialize": function() {
        },

        /**
         * Parses the API's returned object.
         * @param {Object} response API Response
         * @return {Object}
         */
        "parse": function(response) { 
            if(typeof response["Error"] == "undefined") {
                return response;
            }
            if(response["Error"] == false) { 
                return response["Response"];
            } else {
                common.DisplayGlobalError(response["Response"] + "<br /><br /><strong>StackTrace: </strong><br /><span style='font-size:10px;'>" + response["StackTrace"].replace("\n","<br />") + "</span>");
            }
        }
    });

    /**
     * Implements the collection base object and adds 
     * real-time listener handlers.
     *
     * @extends Parking.Base.Collection
     */
    appbase.ListenerCollection = Backbone.Collection.extend({
        
        /**
         * Initializes the collection class
         * and the listener channel.
         * @constructor
         */
        "initialize": function() { 
            this.listen();
        },

        /**
         * Parses the API's returned object.
         * @param {Object} response API Response
         * @return {Object}
         */
        "parse": function(response) {  
            if(typeof response["Error"] == "undefined") {
                return response;
            }

            if(response["Error"] == false) { 
                return response["Response"];
            }
        },

        /**
         * Channel the endpoint will listen to.
         *
         * @type {string}
         * @protected
         */
        channel: "",

        /**
         * Method to be triggered when a message is received.
         *
         * @param {Object} message
         */
        onMessageReceived: function(message) {  
        },

        /**
         * Listens to the specified channel
         * @private 
         */
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

})(jQuery, Parking);
 
        
         