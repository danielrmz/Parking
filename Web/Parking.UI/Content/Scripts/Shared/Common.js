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

    common.DisplayGlobalError = function(message) {
        $(".js-globalError .message").html(message)
        $(".js-globalError").modal()
    };

    common.SetupAjaxToken = function(token) { 

        $.ajaxPrefilter(function(options, originalOptions, xhr) {
            xhr.setRequestHeader('x-parking-token', token);
        });

    };

    common.SetupAjaxErrorHandler = function () {
        $.ajaxSetup({
            "complete": function(data) {  
                if(data.status == 200) { 
                    try {
                        var message = JSON.parse(data["responseText"]); 
                        if(message["Error"] == true && message["Type"] == "APIException") {
                            common.DisplayGlobalError(message["Response"]);
                        }           
                    }catch(error) {
                    }
                } 
            }, 
            "statusCode": {
                404: function() { 
                    common.DisplayGlobalError("Page not found");
                },
                500: function() {
                    common.DisplayGlobalError("Unknown error ocurred, please try again");
                }
            }
        }); 
    };

    /*
    Parking.Common.AjaxErrorHandler = function (req, status, error) {
        if (req.status == 403) {
            alert('You are not authorized to perform this action.');
        }
        Parking.Common.DisplayNotification('error', 'Error in request.', "There was a problem with the system.");
        log('Error in request.', req, status, error);
    };

    Parking.Common.HandleJsonResult = function (form, result) {
        var valid = false;
        switch (result.StatusCode) {
            case 0: //Success
                Parking.Common.DisplaySavedMessage(form);
                valid = true;
                break;
            case 1: //SystemError
                Parking.Common.DisplayNotification('error', 'Error on Save', "An error occurred while saving the information.");
                break;
            case 2: //ValidationError
                Parking.Common.ValidationWarning(form, result.Validation);
                break;
        }
        return valid;
    };
    */
    /*
    Parking.Common.HandleSuccessfulForm = function (responseText, statusText, xhr, form) {
        var result = JSON.parse(responseText);
        Parking.Common.HandleJsonResult(form, result);
    };

    Parking.Common.DisplayNotification = function (type, title, msg, time) {
        var icon, isSticky = typeof time == 'undefined' ? false : !time,
            time = time || 5000;

        switch (type) {
            case 'success':
                icon = Parking.Resources.Icons.Ok;
                break;
            case 'info':
                icon = Parking.Resources.Icons.Info;
                break;
            case 'alert':
                icon = Parking.Resources.Icons.Alert;
                break;
            case 'error':
                icon = Parking.Resources.Icons.Error;
                isSticky = true;
                break;
        }

        return $.gritter.add({
            title: title,
            text: msg,
            sticky: isSticky,
            time: time,
            image: icon
        });
    };

    Parking.Common.RemoveNotification = function (notificationId) {
        if (notificationId) {
            $.gritter.remove(notificationId, {
                fade: true,
                speed: 'fast'
            });
        }
    };

    Parking.Common.DisplaySavedMessage = function (form, message) {
        message = message || "Your information was successfully saved!";
        Parking.Common.ClearValidationMessages(form);
        Parking.Common.DisplayNotification('success', 'Form Saved', message);
    };

    Parking.Common.ValidationWarning = function (form, validationResult) {
        var message = "Please review validation messages.";
        Parking.Common.DisplayNotification('alert', 'Some information is not valid', message);
        var ul = form.find(".validation-summary-errors, .validation-summary-valid").addClass("validation-summary-errors").removeClass("validation-summary-valid").find("ul");
        $.each(validationResult.Errors || [], function () {
            $("<li>").text(this.ErrorMessage).appendTo(ul);
        });
    };

    Parking.Common.ClearValidationMessages = function (form) {
        form.find(".validation-summary-errors, .validation-summary-valid").addClass("validation-summary-valid").removeClass("validation-summary-errors").find("ul").empty();
    };

    */
})(jQuery, Parking.Common);
