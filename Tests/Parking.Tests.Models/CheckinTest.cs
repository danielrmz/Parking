using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sieena.Parking.Tests.Models
{
    using Parking.API.Models;

    [TestClass]
    public class CheckinTest
    {
        [TestMethod]
        public void CheckInTest()
        {
            Checkin.ClearSpace(1);
            Checkin.ClearUser(39);
            Checkin added = Checkin.CheckIn(new Checkin() { StartTime = DateTime.Now, SpaceId = 1, UserId = 39, RegisteredBy = 39, RegisteredFrom = 1});
            Assert.IsNotNull(added);
            Assert.IsNotNull(added.CheckInId);
        }

        [TestMethod]
        public void CheckOutTest()
        {
        }
    }
}
