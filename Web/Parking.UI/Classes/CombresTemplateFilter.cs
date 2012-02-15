using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sieena.Parking.UI.Classes
{
    using Combres;

    /// <summary>
    /// This Filter transforms the html template into an object literal pair so it can
    /// later be stack up in a templates js object for quick access, instead of fetching it. 
    /// 
    /// Must be used with the combined filter
    /// </summary>
    public class CombresTemplateFilter : ISingleContentFilter
    {
        /// <summary>
        /// Generates a path:template pair
        /// </summary>
        /// <param name="resourceSet"></param>
        /// <param name="resource"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public string TransformContent(ResourceSet resourceSet, Resource resource, string content)
        {
            string path = resource.Path.Replace("~", "");
            return string.Format("\"{0}\":'{1}',", path, HttpUtility.JavaScriptStringEncode(content));
        }

        /// <summary>
        /// Applies only to js
        /// </summary>
        /// <param name="resourceType"></param>
        /// <returns></returns>
        public bool CanApplyTo(ResourceType resourceType)
        {
            return ResourceType.JS == resourceType;
        }
    }

    /// <summary>
    /// Filter that combines the before mentioned templates into the Parking.Templates object literal.
    /// Requires the client side namespace plugin.
    /// </summary>
    public class CombresCombinedTemplateFilter : ICombinedContentFilter
    {
        public string TransformContent(ResourceSet resourceSet, IEnumerable<Resource> resources, string content)
        {
            string rsrcs = "namespace(\"Parking.Templates\"); ";
            rsrcs += "(function(undefined) { ";
            rsrcs += "Parking.Templates = { " + content.Replace("',;","',") + " \"ping\":\"pong\" };";
            rsrcs += "})();";
            return rsrcs;
        }

        public bool CanApplyTo(ResourceType resourceType)
        {
            return ResourceType.JS == resourceType;
        }
    }
}