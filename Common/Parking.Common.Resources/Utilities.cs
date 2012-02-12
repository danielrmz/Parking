using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sieena.Parking.Common.Resources
{
    public static class Utilities
    {
        public static Dictionary<string, string> GetResources(string culture)
        {
            Dictionary<string, string> cultureStrings = new Dictionary<string, string>();

            UI ui = new UI();

            UI.Culture = new System.Globalization.CultureInfo(culture);
            
            ui.GetType().GetProperties().ToList().ForEach(pi => {
                if (pi.PropertyType == typeof(String))
                {
                    cultureStrings.Add(pi.Name, pi.GetValue(null, new object[] { }).ToString());
                }
            });

            return cultureStrings;
        }
    }
}
