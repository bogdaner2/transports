using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Internal;
using Transports.Core.DependencyInjection;
using Transports.Core.Interfaces;
using Transports.Core.Models;
using Transports.Core.Models.SQL;
using Transports.Tests.DriversMockRepository;
using Transports.Web.RESTfullWCF;

namespace Transports.Tests
{
    [TestFixture]
    public class ServiceTests
    {
        private ITransportService _service;

        public ServiceTests()
        {
            _service = new TransportService(new DriverMockRepository());
        }

        [Test]
        public void GET_ALL_DRIVERS_RETURN_COLLECTION()
        {
            var list = _service.GetDrivers();
            Assert.IsTrue(typeof(List<Driver>) == list.GetType());
        }

        [Test]
        public void GET_ALL_DRIVERS_EMPTY_COLLECTION_IF_NO_ITEMS_ADDED()
        {
            var list = _service.GetDrivers();
            Assert.IsTrue(list.Count == 0);
        }
    }
}
