/**
* User Information
*
* @package     Parking.UI.Scripts
* @author      The JSONs
* @copyright   2012
* @license     Propietary
*/

namespace("Parking.App.Base");
namespace("Parking.App.Models");

(function ($, models, undefined) {
    
    models.UserSession = Parking.App.Base.Model.extend({
        SessionId: "",
        UserId: "",
        UserName: "", 
        Email: "",
        FirstName: "",
        LastName: "",
        ProfilePictureUrl: "",
        IsAuthenticated: false,
        Role: "",
        RoleId: 0,
        IsBlocked: false,
        FullName: function() { 
            return this.FirstName + " " + this.LastName;
        },

        initialize: function() {
            var self = this;
            this.on("change:IsAuthenticated", function() { 
                if(self.get("IsAuthenticated")) {
                    self.isBlocked(function(b) { self.set("IsBlocked", b); });
                }
            });
        },

        getLastCheckin: function(cbk) {
            cbk = cbk || function (){};
            var self = this;

            // Get current user check in state. 
            if(Parking.App.Data.CurrentUserCheckIn == null) {
                Parking.App.Data.CurrentUserCheckIn = new Parking.App.Models.Checkin({ UserId: self.get("UserId") });
                Parking.App.Data.CurrentUserCheckIn.fetch({success: cbk});
            } else {
                cbk(Parking.App.Data.CurrentUserCheckIn);
            }
        },

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
                 
                var blockings = Parking.App.Data.SpaceBlockings.getBlockingIdsBySpaceId(spaceId);
                var checkins  = Parking.App.Data.CheckinsCurrent.filter(function(model) { return _.include(blockings, model.get("SpaceId")); }); 
                var users = _.map(checkins, function(m) { return m.get("UserId"); });
                
                cbk(users);
            });

        },

        isBlocked: function(cbk) {
            this.getBlockingUsers(function(users) { cbk(users.length > 0); });
        },

        load: function() {
            var self = this;
            var sessionId = $.cookie('ParkingSessionId');
            
            if(sessionId != null) { 
                this.set("SessionId", $.cookie('ParkingSessionId'));
                this.set("UserName", $.cookie('ParkingUserId')); 
            
                // Fetch data from server based on these cookies
                Parking.Common.SetupAjaxToken(this.get('SessionId'));
               
                $.get("/api/session", function(data) { 
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
        },

        save: function(data) {  
            
            Parking.Common.SetupAjaxToken(data['SessionId']); 

            this.trigger("pre-loggedin");
            this.set(data);
            this.trigger("post-loggedin");

            $.cookie('ParkingUserId', this.get('UserName'));
            $.cookie('ParkingSessionId', this.get('SessionId'));

             
        },

        destroy: function(callback) {
            var self = this;

            $.ajax('/api/session', { type: 'DELETE', success: function() { 
            
                $.cookie('ParkingUserId', null);
                $.cookie('ParkingSessionId', null);

                self.clear();
                
                Parking.Common.ClearAjaxToken();
                 
                callback = callback || function() { }; 
                callback();
            } });


        }
    
    });

})(jQuery, Parking.App.Models);