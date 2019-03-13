using System;
using System.Collections.Generic;
using System.Linq;
using Transports.Core.Models;
using Transports.Core.Models.SQL;
using Transports.Core.Repositories;

namespace Transports.Web.WCF
{
    public class TransportService : ITransportService
    {
        private ContextRepository<Driver> _driverRepo = new ContextRepository<Driver>();
        public List<Driver> GetDrivers()
        {
            return _driverRepo.GetAll().ToList();
        }

        public Driver Create(Driver driver)
        {
            return _driverRepo.Create(driver);
        }

        public bool Update(Driver driver)
        {
            var updateDriver = _driverRepo.Get(x => x.DriverId == driver.DriverId);

            updateDriver.Name = driver.Name;
            updateDriver.Age = driver.Age;
            updateDriver.Rang = driver.Rang;

            return _driverRepo.Update(updateDriver);
        }

        public bool Delete(Guid Id)
        {
            return _driverRepo.Remove(_driverRepo.Get(x => x.DriverId == Id));
        }
    }
}
