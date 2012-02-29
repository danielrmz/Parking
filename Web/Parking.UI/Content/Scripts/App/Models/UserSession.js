/**
* User Session data
*
* =reference jquery.cookie.js
*
* @license Copyright 2012. The JSONS
*/

namespace("Parking.App.Base");
namespace("Parking.App.Models");

(function ($, parking, undefined) {
    var common         = parking["Common"];
    var config         = parking["Configuration"];
    var appbase        = parking["App"]["Base"];
    var appmodels      = parking["App"]["Models"]; 
    var appdata        = parking["App"]["Data"]; 

    /**
     * Represents the current logged in user in the system.
     *
     * @extends Parking.App.Base.Model
     */
    appmodels.UserSession = appbase.Model.extend({
        
        /**
         * @enum {Object}
         */
        "defaults": { 
            "SessionId": "",
            "UserId": "",
            "UserName": "", 
            "Email": "",
            "FirstName": "",
            "LastName": "",
            "ProfilePictureUrl": "",
            "IsAuthenticated": false,
            "Role": "",
            "RoleId": 0,
            "IsBlocked": false
        },

        /**
         * Initializes the following events:
         *  - change:IsAuthenticated - Updates the status of isblocked
         *  - renew:IsBlocked - Updates the status of isblocked
         *
         * @constructor
         */
        "initialize": function() { 
            this.on("change:IsAuthenticated", this.verifyIsBlocked, this);
            this.on("renew:IsBlocked", this.verifyIsBlocked, this);
        },
        
        /**
         * Saves the session information to a cookie 
         * and sets the ajax token information.
         *
         * @param {Object} data
         */
        "save": function(data) {  
            
            common.SetupAjaxToken(data['SessionId']); 

            this.trigger("pre-loggedin");
            this.set(data);
            this.trigger("post-loggedin");

            $.cookie('ParkingUserId', this.get('UserName'));
            $.cookie('ParkingSessionId', this.get('SessionId')); 
        },

        /**
         * Destroys the current session, cookies
         *
         * @param {Function=} callback
         */
        "destroy": function(callback) {
            var self = this;

            // Can't delegate directly to Backbone.sync
            $.ajax(config.APIEndpointUrl + 'session', { type: 'DELETE', success: function() { 
            
                    $.cookie('ParkingUserId', null);
                    $.cookie('ParkingSessionId', null);

                    self.clear();
                
                    common.ClearAjaxToken();
                 
                    callback = callback || function() { }; 
                    callback();
                } 
            });
        },

        /**
         * Returns the complete full name
         *
         * @return {string} Returns "FirstName LastName"
         */
        FullName: function() { 
            return this.FirstName + " " + this.LastName;
        },
        
        /**
         * Returns if the user is an administrator or not
         * @return {boolean}
         */
        isAdmin: function() {
            return this.get("Role") == "Administrator";
        },

        /**
         * Verifies if the user is blocked or not. 
         * If it is not authenticated it does not check anything.
         */
        verifyIsBlocked: function() {
            if(this.get("IsAuthenticated")) {
                var self = this;
                this.isBlocked(function(b) { self.set("IsBlocked", b); });
            }
        },

        /**
         * Gets the last check in from the checked in user.
         *
         * @param {Function=} cbk Callback to be called when the last checked in is fetched.
         */
        getLastCheckin: function(cbk) {
            cbk = cbk || function (){};
            var self = this;

            // Get current user check in state. 
            if(appdata.CurrentUserCheckIn == null) {
                appdata.CurrentUserCheckIn = new appmodels.Checkin({ "UserId": self.get("UserId") });
                appdata.CurrentUserCheckIn.fetch({success: cbk});
            } else {
                cbk(appdata.CurrentUserCheckIn);
            }
        },

        /**
         * Gets the blocking users for the current logged in user
         * @param {Function=} cbk Callback to be called when the last checked in is fetched.
         */
        getBlockingUsers: function(cbk) {
            var self = this;
            cbk = cbk || function () { };

            this.getLastCheckin(function(model) { 
                var spaceId = model.get("SpaceId");
                var endDate = model.get("EndTime");

                // If the user's last check in is already marked as checkout, there is no one blocking him/her.
                if(endDate != null && endDate != "") {
                    cbk(false);
                    return;
                }
                 
                var blockings = appdata.SpaceBlockings.getBlockingIdsBySpaceId(spaceId);
                var checkins  = appdata.CheckinsCurrent.filter(function(model) { return _.include(blockings, model.get("SpaceId")); }); 
                var users = _.map(checkins, function(m) { return m.get("UserId"); });
                
                cbk(users);
            });

        },

        /**
         * Gets the blocking users for the current logged in user
         * @param {Function=} cbk Callback to be called when the blocking users calculation is complete
         */
        isBlocked: function(cbk) {
            this.getBlockingUsers(function(users) { cbk(users.length > 0); });
        },

        /**
         * Loads the user info from db based on the cookie
         * Triggers the following events when the load is complete
         *  - pre-loggedin
         *  - post-loggedin
         *  - initialized
         */
        load: function() {
            var self = this;
            var sessionId = $.cookie('ParkingSessionId');
            
            if(sessionId != null) { 
                this.set("SessionId", $.cookie('ParkingSessionId'));
                this.set("UserName", $.cookie('ParkingUserId')); 
            
                // Fetch data from server based on these cookies
                Parking.Common.SetupAjaxToken(this.get('SessionId'));
               
                $.get(config.APIEndpointUrl + "session", function(data) { 
                    if(data.Error == false) {
                        self.trigger("pre-loggedin");
                        self.set(data["Response"]);
                        self.trigger("initialized");
                        self.trigger("post-loggedin");
                    } else {
                        // Show token error 
                    }
                });
            } else {  
                self.trigger("initialized"); 
            }
        }
    
    });

})(jQuery, Parking);