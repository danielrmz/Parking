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
                User u = new User()
                {
                    Password = "test123",
                    Email = "daniel.ramirez@sieena.com",
                    IsActive = true,
                    CreatedAt = DateTime.Now
                };

                User.SaveUser(u);
                //Assert.IsTrue(User.Validate("daniel.ramirez@sieena.com", "pass123"));
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
