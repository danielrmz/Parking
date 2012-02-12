using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Transactions;

using Sieena.Parking.API.Models;

namespace Sieena.Parking.Tests.Models
{
    [TestClass]
    public class AuthTest
    {
        [TestMethod]
        public void ValidateUserByActiveDirectory()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                Assert.IsTrue(User.VerifyCredentials("daniel.ramirez@sieena.com", "xrZ40uye"));
                scope.Dispose();
            }
        }

        [TestMethod]
        public void ValidateUserByDefaultPassword()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                User.SaveUser(new User()
                {
                    Password = "test123",
                    Email = "daniel.ramirez@sieena.com",
                    IsActive = true,
                    CreatedAt = DateTime.Now
                });
                Assert.IsTrue(User.VerifyCredentials("daniel.ramirez@sieena.com", "test123"));
                scope.Dispose();
            }
        }
    }
}
