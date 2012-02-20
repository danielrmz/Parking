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
    
    /**
     * Renders a template in a backbone view context using Handlebars/Mustache and by fetching
     * a remote template.
     *
     * @param {Function=} callback - Callback to be done when the template is fetched/compiled.
     */
    common.RenderViewTemplate = function(callback) {
        var template = this.template;
        var view     = this;

        fetchTemplate(template, function (tmpl) {
            var model = {};

            if(view.model != null) {
                model = view.model.toJSON();
            } 

            var locale          = Parking.Configuration["locale"] || "en-US";
            var localeResources = Parking.Resources["i18n"][locale] || {};
            var currentUser     = typeof(Parking.App._user) == 'undefined'? {} : Parking.App._user.toJSON();
            
            $(view.el).html(tmpl({ "i18n": localeResources, "model": model, "currentUser": currentUser }));
            
            callback = callback || function() { };

            if(typeof(callback) == 'function') {
                callback(tmpl, model);
            }
            
        });

    };

    /**
     * Displays a global error modal box
     * @param {string} message
     */
    common.DisplayGlobalError = function(message) {
        $(".js-globalError .message").html(message)
        $(".js-globalError").modal()
    };

    /**
     * Sets a prefilter to send the session token as a custom header
     * so requests to the api can be made.
     * @param {string} token
     */
    common.SetupAjaxToken = function(token) { 

        $.ajaxPrefilter(function(options, originalOptions, xhr) {
            xhr.setRequestHeader('x-parking-token', token);
        });

    };

    /**
     * Sets jquery ajax base methods
     */
    common.SetupAjaxErrorHandler = function () {
        $.ajaxSetup({
            "complete": function(data) { console.log("1");
                if(data.status == 200) { 
                    try {
                        var message = JSON.parse(data["responseText"]); 
                        
                        if(message["Error"] == true && message["IsGlobalError"] == true) {
                            common.DisplayGlobalError(message["Response"] + "<br /><br /><strong>StackTrace: </strong><br /><span style='font-size:10px;'>" + message["StackTrace"].replace("\n","<br />") + "</span>");
                        }           
                    }catch(error) {
                    }
                } 
            }, 
            "statusCode": {
                403: function() { 
                    common.DisplayGlobalError("You are not authorized to perform this action.");
                },
                404: function() { 
                    common.DisplayGlobalError("Page not found");
                },
                500: function() {
                    common.DisplayGlobalError("Unknown error ocurred, please try again");
                }
            }
        }); 
    };
     
    /**
     * Sets the window title
     * @parma {string} title
     */
    common.SetWindowTitle = function(title) { 
        $("title").html("My Place | " + title);
    };

})(jQuery, Parking.Common);
