/**
* Common helper scripts for the application. 
*
* @license Copyright 2012. The JSONS
*/
namespace("Parking.App.Helpers");
namespace("Parking.App.Data");
namespace("Parking.App.Views");
namespace("Parking.App.Base");

(function($, parking, undefined) {
    var common    = parking["Common"];
    var resources = parking["Resources"];
    var config    = parking["Configuration"];
    var apphelpers= parking["App"]["Helpers"];
    var appdata   = parking["App"]["Data"];
    var appviews  = parking["App"]["Views"];
    var appbase   = parking["App"]["Base"];

    /**
     * Renders a template in a backbone view context using Handlebars/Mustache and by fetching
     * a remote template.
     *
     * @param {Function=} callback - Callback to be done when the template is fetched/compiled.
     */
    apphelpers.RenderViewTemplate = function(data) {
        var template = this.template;
        var view     = this;
        
        if(typeof view.secure != 'undefined' && view.secure) {
            if(appdata.CurrentUser == null || !appdata.CurrentUser.get("IsAuthenticated")) {
                appdata.Router.navigate("login", true);
                return;
            }
        }

        // Verify 

        fetchTemplate(template, function (tmpl) {
            var model = {};
            var collection = [];

            if(view.model != null) {
                model = view.model.toJSON();
            }  

            if(view.collection != null) {
                collection = view.collection.toJSON();
            }
            
            var locale          = config["locale"] || "en-US";
            var localeResources = resources["i18n"][locale] || {};
            var currentUser     = typeof(appdata.CurrentUser) == 'undefined'? {} : appdata.CurrentUser.toJSON();
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
    apphelpers.RenderTemplate = function(template, data, callback) {
        if(callback == null) {
            throw new Error("Callback was expected");
        }

        fetchTemplate(template, function (tmpl) {
            var locale          = config["locale"] || "en-US";
            var localeResources = resources["i18n"][locale] || {};
            var currentUser     = typeof(appdata.CurrentUser) == 'undefined'? {} : appdata.CurrentUser.toJSON();
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
    apphelpers.SetWindowTitle = function(title) { 
        $("title").html("My Place | " + title);
    };

    /**
     * Renders the static page contents.
     */
    apphelpers.RenderStaticPage = function() { 
        var path  = Backbone.history.fragment;
        apphelpers.SetWindowTitle(path);
        
        var view  = new appbase.View({ "el" : $("#main")});
        view.template = config.ClientTemplatesUrl + "Static/" + path + ".html";
        view.render();
    };

    /**
     * Default handler to render the function.
     * To be used the view has to have the same name 
     * as the routing method
     */
    apphelpers.RenderBackbonePage = function() {
        var frag = Backbone.history.fragment;
        
        frag = appdata.Router.routes[frag];

        frag = frag.charAt(0).toUpperCase() + frag.substring(1);
        
        if(typeof appviews[frag] == "undefined") {
            common.DisplayGlobalError("Page does not exist: " + frag);
            return;
        }

        apphelpers.SetWindowTitle(frag);
        var view = new appviews[frag]({ "el": $("#main")});
        view.render();
    };

    /**
     * Converts all the links in a page to possible backbone pages.
     */
    apphelpers.SetBackboneLinks = function() { 
        $(document).on("click", "a:not([data-bypass])", function(evt) {
            var href = $(this).attr("href");
            var protocol = this.protocol + "//";

            // Ensure the protocol is not part of URL, meaning its relative.
            if (href && href.slice(0, protocol.length) !== protocol) {
                // Stop the default event to ensure the link will not cause a page
                // refresh.
                evt.preventDefault();

                appdata.Router.navigate(href, true);
            }
        });
    };

    /**
     * Register time ago helper for the templates.
     */
    Handlebars.registerHelper('timeAgo', function(time) {
        var t = common.FormatTimeAgo(time); 
        return t;
    });

    /**
     * Handler to obtain the user's name from cache. 
     */
    Handlebars.registerHelper('userFullName', function(userId) {
        if(!appdata.Users) {
            return "";
        }
        var user = appdata.Users.get(userId);
        if(user) {
            return user.get("FirstName") + " " + user.get("LastName");
        }
        return "";
    });

     /**
     * Handler to obtain the user's name from cache. 
     */
    Handlebars.registerHelper('userProfilePicture', function(userId) {
        if(!appdata.Users) {
            return "";
        }
        var user = appdata.Users.get(userId);
        if(user) {
            return user.get("ProfilePictureUrl");
        }
        return "";
    });

})(jQuery, Parking);
