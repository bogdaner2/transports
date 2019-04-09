using System;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Transports.Core.DependencyInjection;
using Transports.Core.Interfaces;
using Transports.Core.Models;
using Transports.Core.Models.SQL;

namespace Transports.Tests
{
    [TestFixture]
    public class ServiceTests
    {
        [SetUp]
        public void Init()
        {
            var drivers = new List<Driver>
            {
                new Driver { DriverId = Guid.NewGuid(), Name = "test", Age = 18, Rang = RangEnum.A},
                new Driver { DriverId = Guid.NewGuid(), Name = "test1", Age = 18, Rang = RangEnum.A},
                new Driver { DriverId = Guid.NewGuid(), Name = "test2", Age = 18, Rang = RangEnum.A}
            };
            var mock = new Mock<IRepository<Driver>>();
            mock.Setup(a => a.GetAll()).Returns(drivers);

            ServiceLocator.SetDriverRepo(mock.Object);
        }

        [Test]
        public void Return_Driver_Collection()
        {
            ServiceLocator.SetDriverRepo(null);
        }
    }
}
