using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Sieena.Parking.Common.Resources;
using System.Globalization;

namespace Parking.Tests.Common
{
    [TestClass]
    public class ResourcesTest
    {
        /// <summary>
        /// Verifies that the english resource file is correctly read.
        /// </summary>
        [TestMethod]
        public void TestEnUS()
        {
            CultureInfo info = Sieena.Parking.Common.Resources.UI.Culture;
            Dictionary<string, string> resources = Utilities.GetResources("en-US");

            Assert.IsNotNull(resources);
            Assert.IsInstanceOfType(resources, typeof(Dictionary<string, string>));
            Assert.AreEqual(resources["TEST"], "TEST");
            Assert.AreEqual(info, Sieena.Parking.Common.Resources.UI.Culture);
        }

        /// <summary>
        /// Verifies that the spanish resource file is correctly obtained.
        /// </summary>
        [TestMethod]
        public void TestEsMX()
        {
            CultureInfo info = Sieena.Parking.Common.Resources.UI.Culture;
            Dictionary<string, string> resources = Utilities.GetResources("es-MX");

            Assert.IsNotNull(resources);
            Assert.IsInstanceOfType(resources, typeof(Dictionary<string, string>));
            Assert.AreEqual(resources["TEST"], "PRUEBA");
            Assert.AreEqual(info, Sieena.Parking.Common.Resources.UI.Culture);   
        }

        /// <summary>
        /// Verifies both resource files have the same keys.
        /// </summary>
        [TestMethod]
        public void SameKeys()
        {
            Dictionary<string, string> ES = Utilities.GetResources("es-MX");
            Dictionary<string, string> EN = Utilities.GetResources("en-US");

            Assert.AreEqual(ES.Count, EN.Count); 

            ES.Keys.ToList().ForEach(s => {
                Assert.IsTrue(EN.ContainsKey(s));
            });

        }
    }
}
