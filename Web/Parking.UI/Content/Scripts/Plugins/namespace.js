/**
 * @param {string} name Namespace to create
 * @param {separator=} Defaults to a dot
 * @param {container=} Container in which the namespace will be created. Defaults to Window
 */
function namespace(name, separator, container) {
    var ns = name.split(separator || '.'),
    o = container || window,
    i,
    len;
    for (i = 0, len = ns.length; i < len; i++) {
        o = o[ns[i]] = o[ns[i]] || {};
    }
    return o;
};

/**
 * Fetches a template from the server.
 * @param {string} path
 * @param {Function=} done - Callback
 */
function fetchTemplate(path, done) {
    window.JST = window.JST || {};

    // Should be an instant synchronous way of getting the template, if it
    // exists in the JST object.
    if (JST[path]) {
      return done(JST[path]);
    }

    // Fetch it asynchronously if not available from JST
    return $.get(path, function (contents) {
        var tmpl = Handlebars.compile(contents);  //_.template(contents);
        JST[path] = tmpl;

        done(tmpl);
    });
}