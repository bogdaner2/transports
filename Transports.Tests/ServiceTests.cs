using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Moq;
using NUnit.Framework;
using Transports.Core.DependencyInjection;
using Transports.Core.Interfaces;
using Transports.Core.Models;
using Transports.Core.Models.SQL;
using Transports.Tests.Proxy;

namespace Transports.Tests
{
    [TestFixture]
    public class ServiceTests
    {
        private int addedItems = 0;
        private int removedItems = 0;
        private TransportsServiceClient _wcfClient;


        List<Driver> drivers = new List<Driver>
        {
            new Driver { DriverId = Guid.NewGuid(), Name = "test", Age = 18, Rang = RangEnum.A},
            new Driver { DriverId = Guid.NewGuid(), Name = "test1", Age = 18, Rang = RangEnum.A},
            new Driver { DriverId = Guid.NewGuid(), Name = "test2", Age = 18, Rang = RangEnum.A}
        };

        [SetUp]
        public void Init()
        {
            _wcfClient = new TransportsServiceClient();
            var mock = new Mock<IRepository<Driver>>();
            mock.Setup(r => r.GetAll()).Returns(drivers);
            mock.Setup(r => r.Get(It.IsAny<Expression<Func<Driver, bool>>>())).Returns(drivers[new Random().Next(0, 2)]);
            mock.Setup(r => r.Create(It.IsAny<Driver>())).Callback((Driver item) =>
            {
                addedItems++;
                drivers.Add(item);
            });
            mock.Setup(r => r.Remove(It.IsAny<Driver>())).Callback((Driver result) => removedItems++);

            ServiceLocator.SetDriverRepo(mock.Object);
        }

        [Test]
        public void Return_Driver_Collection()
        {
        }
    }
}
