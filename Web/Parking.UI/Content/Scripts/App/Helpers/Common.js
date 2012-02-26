/**
* Common scripts for the application. 
*
* @package     Parking.UI.Scripts
* @author      The JSONs
* @copyright   2012 -
* @license     Propietary
*/
namespace("Parking.App.Helpers");
namespace("Parking.App.Data");

(function($, helpers, undefined) {
    
    /**
     * Renders a template in a backbone view context using Handlebars/Mustache and by fetching
     * a remote template.
     *
     * @param {Function=} callback - Callback to be done when the template is fetched/compiled.
     */
    helpers.RenderViewTemplate = function(data) {
        var template = this.template;
        var view     = this;

        fetchTemplate(template, function (tmpl) {
            var model = {};
            var collection = [];

            if(view.model != null) {
                model = view.model.toJSON();
            } 

            if(view.collection != null) {
                collection = view.collection.toJSON();
            }
            
            var locale          = Parking.Configuration["locale"] || "en-US";
            var localeResources = Parking.Resources["i18n"][locale] || {};
            var currentUser     = typeof(Parking.App.Data.CurrentUser) == 'undefined'? {} : Parking.App.Data.CurrentUser.toJSON();
            var callback =  typeof data == "function" ? data : function() { };
            var data = typeof data == "function" ? {} : data;

            $(view.el).html(tmpl({ "i18n": localeResources, "model": model, "collection": collection, "currentUser": currentUser, "data": data }));
            
            callback();
        });

    };

     /**
     * Renders a template in a backbone view context using Handlebars/Mustache and by fetching
     * a remote template.
     *
     * @param {string} template - Template file to use
     * @param {Object} data - data to be used in the template. 
     * @param {Function} callback - Callback to be done when the template is fetched/compiled.
     */
    helpers.RenderTemplate = function(template, data, callback) {
        if(callback == null) {
            throw new Error("Callback was expected");
        }

        fetchTemplate(template, function (tmpl) {
            var locale          = Parking.Configuration["locale"] || "en-US";
            var localeResources = Parking.Resources["i18n"][locale] || {};
            var currentUser     = typeof(Parking.App._user) == 'undefined'? {} : Parking.App._user.toJSON();
            data = data || {};
            data["i18n"] = localeResources;
            data["currentUser"] = currentUser; 

            var html = tmpl(data);
            callback(html);


        });

    };
      
    /**
     * Sets the window title
     * @parma {string} title
     */
    helpers.SetWindowTitle = function(title) { 
        $("title").html("My Place | " + title);
    };

    /**
     * Default handler to render the function.
     * To be used the view has to have the same name 
     * as the routing method
     */
    helpers.RenderBackbonePage = function() {
        var frag = Backbone.history.fragment;
        
        frag = Parking.App.router.routes[frag];

        frag = frag.charAt(0).toUpperCase() + frag.substring(1);
        
        if(typeof Parking.App.Views[frag] == "undefined") {
            Parking.Common.DisplayGlobalError("Page does not exist: " + frag);
            return;
        }

        helpers.SetWindowTitle(frag);
        var view = new Parking.App.Views[frag]({el: $("#main")});
        view.render();
    };

    helpers.SetBackboneLinks = function() { 
        $(document).on("click", "a:not([data-bypass])", function(evt) {
            var href = $(this).attr("href");
            var protocol = this.protocol + "//";

            // Ensure the protocol is not part of URL, meaning its relative.
            if (href && href.slice(0, protocol.length) !== protocol) {
                // Stop the default event to ensure the link will not cause a page
                // refresh.
                evt.preventDefault();

                Parking.App.router.navigate(href, true);
            }
        });
    };

    /**
     * Register time ago helper for the templates.
     */
    Handlebars.registerHelper('timeAgo', function(time) {
        var t = Parking.Common.FormatTimeAgo(time); 
        return t;
    });

    /**
     * Handler to obtain the user's name from cache. 
     */
    Handlebars.registerHelper('userFullName', function(userId) {
        if(!Parking.App.Data.Users) {
            return "";
        }
        var user = Parking.App.Data.Users.get(userId);
        if(user) {
            return user.get("FirstName") + " " + user.get("LastName");
        }
        return "";
    });

     /**
     * Handler to obtain the user's name from cache. 
     */
    Handlebars.registerHelper('userProfilePicture', function(userId) {
        if(!Parking.App.Data.Users) {
            return "";
        }
        var user = Parking.App.Data.Users.get(userId);
        if(user) {
            return user.get("ProfilePictureUrl");
        }
        return "";
    });

})(jQuery, Parking.App.Helpers);
