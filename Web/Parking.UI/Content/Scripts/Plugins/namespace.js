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