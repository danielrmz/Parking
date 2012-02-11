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
    public class UserTest
    {
        [TestMethod]
        public void CreateUser()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                string email = "daniel.ramirez@sieena.com";
                User u = new User()
                {
                    Password = "test123",
                    Email = email,
                    IsActive = true,
                    CreatedAt = DateTime.Now
                };

                User.SaveUser(u);

                Assert.IsNotNull(User.GetByEmail(email));
            }
        }

        [TestMethod]
        public void UpdateUser()
        {
            using (TransactionScope scope = new TransactionScope())
            {
            }
        }

        [TestMethod]
        public void DeleteUser()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                string email = "daniel.ramirez@sieena.com";
                User u = new User()
                {
                    Password = "test123",
                    Email = email,
                    IsActive = true,
                    CreatedAt = DateTime.Now
                };

                User.SaveUser(u);
                User.DeleteUser(email);

                User unew = User.GetByEmail(email);

                Assert.IsNotNull(unew);
                Assert.IsFalse(unew.IsActive);
            }
        }
    }
}
