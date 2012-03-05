/**
* Base namespace for the application.
*
* @license Copyright 2012. The JSONS
*/

namespace("Parking.App.Views");
namespace("Parking.App.Data");

(function ($, parking) {
    var i18n           = parking["Resources"]["i18n"];
    var common         = parking["Common"];
    var config         = parking["Configuration"];
    var appbase        = parking["App"]["Base"];
    var appmodels      = parking["App"]["Models"]; 
    var appdata        = parking["App"]["Data"]; 
    var appviews       = parking["App"]["Views"];
    var appcollections = parking["App"]["Collections"];
    var apphelpers     = parking["App"]["Helpers"];

    /**
     * User List PopUp
     * This displays the users
     *
     * @extends appbase.View
     */
    appviews.UserSelector = appbase.View.extend({
        
        /**
         * @constructor
         */
         "initialize": function(filter) {
            this.collection = appdata.Users;
            this.selectedUser = null;
            this.collectionFilter = filter;
            this.on("render", function() {this.renderList(this.collection.searchByName("")); }, this);
        },


        /**
         * @enum {string}
         */
        "events": {
            "click .js-button-search": "search",
            "keyup .search-query": "search", 
            "click li": "selectUser",
            "click li span": "selectUser",
            "click li img": "selectUser",
            "click .js-btn-success": "doAction"
        },

        /**
         * View's template
         * @type {string}
         */
        "template": config.ClientTemplatesUrl + "Shared/UserSelector.html",

        /**
         * @inheritDoc
         */
        "render": function() { 
            apphelpers.RenderViewTemplate.apply(this, arguments);
            this.renderList(this.collection); 
        },
        
        /**
         * Event that goes through the collection and searches for the people that match that name
         */
        "search": function() { 
            var query = $(this.el).find(".search-query").val();
            this.renderList(this.collection.searchByName(query));

            return false;
        },

        /**
         * Marks a user as selected
         * @param {Event} e
         */
        "selectUser": function(e) { 
            $(this.el).find(".selected").removeClass("selected");
            var el = $(e.target);
            var li = el;
            if(el[0].tagName != 'LI') {
               li = el.parentsUntil("li").parent();
            }
            li.addClass("selected");
            this.selectedUser = li.data("userid");
        },

        "doAction": function() { 
            // Validate a user has been selected
            var selectedLi = $(this.el).find(".selected");
            var errors = $(this.el).find(".js-errors");

            if(selectedLi.size() > 0) {
                this.trigger("submit", selectedLi.data("userid"));
                this.selectedUser = null;
                selectedLi.removeClass("selected");
                errors.html("");
            } else {
                errors.html("Please select a user");
            }

            return false;
        },

        /**
         * Renders the list of users.
         * @param {Object} users
         * @this
         */
        renderList: function(users) { 
            var self = this;
            var userList = $(".available-user");
                userList.html("");

            if(this.collectionFilter != null) {
                users = this.collectionFilter(users);
            }    

            users.each(function(u) { 
                var v = new appviews.UserSelectorItem({"model":u, "collection": self.collection});
                var el = $(v.render().el);
                var userId = u.get("UserId");
                el.data("userid", userId);
                if(self.selectedUser != null && self.selectedUser == userId) {
                    el.addClass("selected");
                }
                userList.append(el);
            });

            return this;
        }
         
    });


    /** 
     * View that represents a single item of the list.
     */
    appviews.UserSelectorItem =  appbase.View.extend({
        "tagName": "li",

        /**
         * View's template
         * @type {string}
         */
        "template": config.ClientTemplatesUrl + "Shared/UserSelectorItem.html",

        /**
         * @inheritDoc
         */
        "render": apphelpers.RenderViewTemplate
         
    });

})(jQuery, Parking);