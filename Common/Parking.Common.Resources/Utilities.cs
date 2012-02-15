using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace Sieena.Parking.Common.Resources
{
    public static class Utilities
    {
        public static Dictionary<string, string> GetResources(string culture)
        {
            Dictionary<string, string> cultureStrings = new Dictionary<string, string>();

            UI ui = new UI();
            CultureInfo bkup = UI.Culture;

            UI.Culture = new CultureInfo(culture);
            
            ui.GetType().GetProperties().ToList().ForEach(pi => {
                if (pi.PropertyType == typeof(String))
                {
                    cultureStrings.Add(pi.Name, pi.GetValue(null, new object[] { }).ToString());
                }
            });

            UI.Culture = bkup;

            return cultureStrings;
        }
    }
}
