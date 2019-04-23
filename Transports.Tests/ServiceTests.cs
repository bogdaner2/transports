using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Transports.Core.Models;
using Transports.Core.Models.SQL;
using Transports.Tests.DriversMockRepository;
using Transports.Web.RESTfullWCF;

namespace Transports.Tests
{
    [TestFixture]
    public class ServiceTests
    {
        private readonly ITransportService _service;
        private readonly Driver _mockDriver = new Driver { DriverId = Guid.NewGuid(), Name = "Mock", Rang = RangEnum.A};

        public ServiceTests()
        {
            _service = new TransportService(new DriverMockRepository());
        }

        [Test]
        public void GET_ALL_DRIVERS_RETURN_COLLECTION()
        {
            var list = _service.GetDrivers();
            Assert.IsTrue(typeof(List<Driver>) == list.GetType(), "typeof(List<Driver>) != list.GetType()");
        }

        [Test]
        public void GET_ALL_DRIVERS_EMPTY_COLLECTION_IF_NO_ITEMS_ADDED()
        {
            var list = _service.GetDrivers();
            Assert.IsTrue(list.Count == 0, "list.Count != 0");
        }

        [Test]
        public void GET_ALL_DRIVERS_RETURN_COLLECTION_WITH_ITEMS_IF_ANY()
        {
            _service.CreateDriver(_mockDriver);
            var list = _service.GetDrivers();
            Assert.IsTrue(list.Any(), "list.Count == 0");

            _service.ClearDrivers();
        }

        [Test]
        public void CREATE_DRIVER_POPULATE_COLLECTION_WITH_DRIVER()
        {
            _service.CreateDriver(_mockDriver);
            var item = _service.GetDrivers().FirstOrDefault();
            Assert.IsNotNull(item, "item == null");

            _service.ClearDrivers();
        }

        [Test]
        public void CREATE_DRIVER_POPULATE_COLLECTION_WITH_CERTAIN_DRIVER()
        {
            _service.CreateDriver(_mockDriver);
            var item = _service.GetDrivers().FirstOrDefault();
            Assert.AreEqual(item, _mockDriver, "Items not equal");

            _service.ClearDrivers();
        }

        [Test]
        public void UPDATE_DRIVER_DOES_NOT_CHANGE_COLLECTION_COUNT()
        {
            _service.CreateDriver(_mockDriver);

            var countBeforeCreate = _service.GetDrivers().Count;

            _service.UpdateDriver(_mockDriver);

            var countAfterCreate = _service.GetDrivers().Count;

            Assert.IsTrue(countBeforeCreate == countAfterCreate, "countBeforeCreate != countAfterCreate");

            _service.ClearDrivers();
        }

        [Test]
        public void UPDATE_DRIVER_CHANGE_OBJECT_IN_COLLECTION()
        {
            var newName = "Updated_Name";

            _service.CreateDriver(_mockDriver);

            var updatedItem = _mockDriver;
            updatedItem.Name = newName;

            _service.UpdateDriver(updatedItem);

            var item = _service.GetDrivers().FirstOrDefault();

            Assert.IsTrue(ReferenceEquals(_mockDriver, item));
            Assert.IsTrue(item?.Name == newName);

            _service.ClearDrivers();
        }

        [Test]
        public void REMOVE_DRIVER_DECREASE_ITEMS_COUNT()
        {
            _service.CreateDriver(_mockDriver);
            _service.CreateDriver(_mockDriver);

            var countBeforeRemove = _service.GetDrivers().Count;

            _service.DeleteDriver(_mockDriver.DriverId.ToString());

            var countAfterRemove = _service.GetDrivers().Count;

            Assert.IsTrue(countAfterRemove == countBeforeRemove - 1, "countAfterRemove == countBeforeRemove");

            _service.ClearDrivers();
        }

        [Test]
        public void REMOVE_DRIVER_METHOD_DELETE_ITEM_FROM_COLLECTION()
        {
            _service.CreateDriver(_mockDriver);
            _service.CreateDriver(new Driver { DriverId = Guid.NewGuid(), Name = "New Name"});

            _service.DeleteDriver(_mockDriver.DriverId.ToString());

            var item = _service.GetDrivers().FirstOrDefault(x => x.DriverId == _mockDriver.DriverId);

            Assert.IsNull(item, "item != null");

            _service.ClearDrivers();
        }

        [Test]
        public void CLEAR_METHOD_REMOVE_ALL_DRIVERS()
        {
            _service.CreateDriver(_mockDriver);
            _service.CreateDriver(_mockDriver);
            _service.CreateDriver(_mockDriver);

            _service.ClearDrivers();

            Assert.IsTrue(_service.GetDrivers().Count == 0, "_service.GetDrivers().Count != 0");
        }
    }
}
