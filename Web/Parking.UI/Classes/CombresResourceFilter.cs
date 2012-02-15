using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sieena.Parking.UI.Classes
{
    using Sieena.Parking.Common.Resources;
    using ServiceStack.Text;

    using Combres;

    /// <summary>
    /// Outputs to client side the resource i18n strings.
    /// </summary>
    public class CombresResourceFilter : ISingleContentFilter
    { 
        public string TransformContent(ResourceSet resourceSet, Resource resource, string content)
        {
            content += " namespace(\"Parking.Resources.i18n\");";
            Dictionary<string, string> en = Utilities.GetResources("en-US");
            Dictionary<string, string> es = Utilities.GetResources("es-MX");
            //Func<string, Dictionary<string, string>> toJSON = 
            //ServiceStack.Text.JsonExtensions.
            string jsEn = string.Format("resx['i18n']['en-US'] = {0};", en.ToJson());
            string jsEs = string.Format("resx['i18n']['es-MX'] = {0};", es.ToJson());

            content += " (function(resx, undefined) { " + jsEn + jsEs + " })(Parking.Resources);";
            return content;
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

}