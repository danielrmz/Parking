/**
* Base namespace for the application.
*
* @package     Parking.UI.Scripts
* @author      The JSONs
* @copyright   2012
* @license     Propietary
*/

namespace("Parking.App.Views");

(function ($, undefined) {

Parking.App.Views.Index = function() {
    var URL = "/Home";

    var _view = $("<div class='home-activities'>"), 
        _grid, _saveCallback;
    
    function load(options) {
        AICE.Common.LoadPanel(_view, URL, options, init);
    }

    function save(callback) {
        _saveCallback = callback;

        _saveCallback();
    }

    function requiresSave() {
        return false;
    }


    function init() {
        //TODO
        AICE.Controls.Activities(_view, "Home");
    }

    return {
        View: _view,
        Load: load,
        Save: save,
        RequiresSave: requiresSave
    };
};


}(jQuery));