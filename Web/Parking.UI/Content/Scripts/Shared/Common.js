/**
* Common scripts for the application. 
* 
* =reference spin.js
*
* @license Copyright 2012. The JSONS
*/
namespace("Parking.Common");
namespace("Parking.Resources.i18n");

(function ($, parking, undefined) {
    var common  = parking["Common"]; 
    var i18n    = parking["Resources"]["i18n"];

    /** 
     * Serializes form fields into key value pairs.
     * @return {Object}
     */
    $.fn.serializeObject = function()
    {
        var o = {};
        var a = this.serializeArray(); 

        $.each(a, function() {
            if (o[this.name] !== undefined) {
                if (!o[this.name].push) {
                    o[this.name] = [o[this.name]];
                }
                o[this.name].push(this.value || '');
            } else {
                o[this.name] = this.value || '';
            }
        });
        return o;
    };

    /**
     * Wrapper for the browser's geolocation API
     * @param {Function} nearSuccessfull Callback to be done when the user is detected to be near the base point
     */
    common.DetectLocation = function(nearSuccessfull) { 
        if (navigator.geolocation) 
        {
	        navigator.geolocation.getCurrentPosition( 
                function (position) {
                    function toRad(num) {
                        return num * Math.PI / 180;
                    }  

                    var lat1 = position.coords.latitude;
                    var lon1 = position.coords.longitude;
                    
                    var baseLat = 25.653876;
                    var baseLong = -100.38137;

                    var R = 6371; // km
                    var dLat = toRad(baseLat-lat1);
                    var dLon = toRad(baseLong-lon1);
                    lat1 = toRad(lat1);
                    var lat2 = toRad(baseLat);
                     
                    var a = Math.sin(dLat/2) * Math.sin(dLat/2) +
                            Math.sin(dLon/2) * Math.sin(dLon/2) * Math.cos(lat1) * Math.cos(lat2); 
                    var c = 2 * Math.atan2(Math.sqrt(a), Math.sqrt(1-a)); 
                    var d = R * c;

                    
                    //console.log(d);
                    if((d*1000) > 750) {
                    } else {
                        nearSuccessfull = nearSuccessfull || function() {};
                        nearSuccessfull();
                    }
                },
                function(error) { 
                    switch(error.code) 
			        {
				        case error.TIMEOUT:
					        alert ('Timeout');
					        break;
				        case error.POSITION_UNAVAILABLE:
					        alert ('Position unavailable');
					        break;
				        case error.PERMISSION_DENIED:
					        alert ('Permission denied');
					        break;
				        case error.UNKNOWN_ERROR:
					        alert ('Unknown error');
					        break;
			        }

                }
            );
        }
    },

    /**
     * Displays a global error modal box
     *
     * @param {string} message
     */
    common.DisplayGlobalError = function(message, onClose) {
        $(".js-globalError .message").html(message);
        $(".js-globalError").modal();

        if(onClose) {
            $(".js-globalError").off("hide").on("hide", onClose);
        }
    };

    /**
     * Sets a prefilter to send the session token as a custom header
     * so requests to the api can be made.
     *
     * @param {string} token
     */
    common.SetupAjaxToken = function(token) { 
        common._ajaxTokenPrefilterToken = function(options, originalOptions, xhr) {
            xhr.setRequestHeader('x-parking-token', token);
        };

        common._ajaxTokenPrefilterBlank = function(options, originalOptions, xhr) {  };
        
        common._ajaxTokenPrefilter = function() { 
            common._ajaxTokenPrefilterToken.apply(this, arguments);
        };

        $.ajaxPrefilter(function() { common._ajaxTokenPrefilter.apply(this, arguments); });

    };

    /**
     * Clears out the session token from the prefilter. 
     */
    common.ClearAjaxToken = function() {
         common._ajaxTokenPrefilter = function() { 
            common._ajaxTokenPrefilterBlank.apply(this, arguments); 
         }; 
    };

    /**
     * Sets jquery ajax base methods
     */
    common.SetupAjaxErrorHandler = function () {
        $.ajaxSetup({
            "complete": function(data) { 
                if(data.status == 200) { 
                    try {
                        var message = JSON.parse(data["responseText"]); 
                        
                        if(message["Error"] == true ) {
                            if(message["Type"] == "InvalidTokenException" || message["Type"] == "AccessException") {
                                common.DisplayGlobalError(message["Response"], function() { 
                                    $.cookie('ParkingUserId', null);
                                    $.cookie('ParkingSessionId', null);
                                    $.cookie('ParkingLocale', null);
                                    common.ClearAjaxToken();
                                    location.reload(); 
                                }); 
                                return;
                            }

                            if(message["IsGlobalError"] == true) {
                                common.DisplayGlobalError(message["Response"] + "<br /><br /><strong>StackTrace: </strong><br /><span style='font-size:10px;'>" + message["StackTrace"].replace("\n","<br />") + "</span>");
                                // Log
                            }

                            
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
     * @license Pretty date by Copyright (c) 2011 John Resig (ejohn.org) MIT / GPL
     */
    /**
     * Takes an ISO time and returns a string representing how
     * long ago the date represents.
     *
     * @param {string|Date} time
     */
    common.FormatTimeAgo = function (time) {
        var date;
        dnFormat = time.match("([0-9]{13})");
        
        if(dnFormat[1] != "") {
            date = new Date();
            date.setTime(parseFloat(dnFormat[1]));
        } else {
            date = (typeof(time.getDate) == "undefined") ? new Date((time || "").replace(/-/g,"/").replace(/[TZ]/g," ")) : time;
        }
        var diff = (((new Date()).getTime() - date.getTime()) / 1000),
		    day_diff = Math.floor(diff / 86400);
		
	    if ( isNaN(day_diff) || day_diff < 0 || day_diff >= 31 )
		    return;
		
	    return day_diff == 0 && (
			    diff < 60 && i18n.get("TimeAgo_JustNow") ||
			    diff < 120 && i18n.get("TimeAgo_Minute")   ||
			    diff < 3600 && i18n.get("TimeAgo_Minutes").replace("{0}",Math.floor( diff / 60 )) ||
			    diff < 7200 && i18n.get("TimeAgo_Hour") ||
			    diff < 86400 && i18n.get("TimeAgo_Hours").replace("{0}",Math.floor( diff / 3600 )) ||
		    day_diff == 1 && i18n.get("TimeAgo_Yesterday") ||
		    day_diff < 7 && i18n.get("TimeAgo_Days").replace("{0}",day_diff) ||
		    day_diff < 31 && i18n.get("TimeAgo_Weeks").replace("{0}",Math.ceil( day_diff / 7 )));
    };
    
    /**
     * Initializes the spinner/loader 

     * @return {Object}
     */
    common.InitializeLoader = function() { 
        
        var loading = $(".js-loading");
        loading.setLoaderText = common.SetLoaderText;

        return loading;
         
    };

    /**
     * Sets the loader text
     *
     * @param {string} text
     */
    common.SetLoaderText = function(text) {
        $(".js-loading-legend").html(text);
    };

    /**
     * Sets fixed language contents.
     */
    common.setLanguageVerbiage = function() { 
        $("[data-langkey]").each(function() { 
            var rsrc = $(this).data("langkey");
            $(this).html(i18n.get(rsrc));
        });
    };

    common.setLanguageLinks = function() {
        $("[data-locale]").each(function() { 
            $(this).click(function() { 
                $.cookie("ParkingLocaleOverride", $(this).data("locale"));
                location.reload();
            });
        });
    };

})(jQuery, Parking);
