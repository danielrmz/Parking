/**
* User Information
*
* @package     Parking.UI.Scripts
* @author      The JSONs
* @copyright   2012
* @license     Propietary
*/

namespace("Parking.App.Models");

(function ($, models, undefined) {
    
    models.UserInformation = Backbone.Model.extend({
        SessionId: "",
        UserName: "", 
        Email: "",
        FirstName: "",
        LastName: "",
        ProfilePictureUrl: "",
        IsAuthenticated: false,
        Role: "",
        RoleId: 0,

        FullName: function() { 
            return this.FirstName + " " + this.LastName;
        },

        initialize: function() {
            this.load();

            
            // Load required associated views. 
            (new Parking.App.Views.HeaderUserInfo({ model: this, el: $('.user-info .user') })).render(); 
            (new Parking.App.Views.Dashboard({model: this, el: $('#dashboard') })).render(); 
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
                        self.set(data["Response"]);
                    }
                });
            }
        },

        save: function(data) {  
            this.set(data);

            $.cookie('ParkingUserId', this.get('UserName'));
            $.cookie('ParkingSessionId', this.get('SessionId'));

            Parking.Common.SetupAjaxToken(this.get('SessionId'));
        },

        destroy: function(callback) {
            var self = this;

            $.ajax('/api/session', { type: 'DELETE', success: function() { 
            
                $.cookie('ParkingUserId', null);
                $.cookie('ParkingSessionId', null);

                self.clear();

                callback = callback || function() { };
                callback();
            } });


        }
    
    });

})(jQuery, Parking.App.Models);