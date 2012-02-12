using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Sieena.Parking.Common.Resources;

namespace Parking.Tests.Common
{
    [TestClass]
    public class ResourcesTest
    {
        [TestMethod]
        public void TestEnUS()
        {
            Dictionary<string, string> resources = Utilities.GetResources("en-US");

            Assert.IsNotNull(resources);
            Assert.IsInstanceOfType(resources, typeof(Dictionary<string, string>));
            Assert.AreEqual(resources["TEST"], "TEST");
        }

        [TestMethod]
        public void TestEsMX()
        {
            Dictionary<string, string> resources = Utilities.GetResources("es-MX");

            Assert.IsNotNull(resources);
            Assert.IsInstanceOfType(resources, typeof(Dictionary<string, string>));
            Assert.AreEqual(resources["TEST"], "PRUEBA");
        }
    }
}
