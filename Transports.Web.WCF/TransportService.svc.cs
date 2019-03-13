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
        private ContextRepository<Shift> _shiftsRepo = new ContextRepository<Shift>();
        private ContextRepository<Route> _routesRepo = new ContextRepository<Route>();

        // Drivers
        public List<Driver> GetDrivers()
        {
            return _driverRepo.GetAll().ToList();
        }

        public Driver CreateDriver(Driver driver)
        {
            return _driverRepo.Create(driver);
        }

        public bool UpdateDriver(Driver driver)
        {
            var updateDriver = _driverRepo.Get(x => x.DriverId == driver.DriverId);

            updateDriver.Name = driver.Name;
            updateDriver.Age = driver.Age;
            updateDriver.Rang = driver.Rang;

            return _driverRepo.Update(updateDriver);
        }

        public bool DeleteDriver(string Id)
        {
            var guid = Guid.Parse(Id);

            return _driverRepo.Remove(_driverRepo.Get(x => x.DriverId == guid));
        }

        // Shifts
        public List<Shift> GetShifts()
        {
            return _shiftsRepo.GetAll().ToList();
        }

        public Shift CreateShift(Shift shift)
        {
            return _shiftsRepo.Create(shift);
        }

        public bool UpdateShift(Shift shift)
        {
            var updateShift = _shiftsRepo.Get(x => x.ShiftId == shift.ShiftId);

            updateShift.Start = shift.Start;
            updateShift.End = shift.End;

            return _shiftsRepo.Update(updateShift);
        }

        public bool DeleteShift(Guid Id)
        {
            return _shiftsRepo.Remove(_shiftsRepo.Get(x => x.ShiftId == Id));
        }

        // Routes
        public List<Route> GetRoutes()
        {
            return _routesRepo.GetAll().ToList();
        }

        public Route CreateRoute(Route route)
        {
            return _routesRepo.Create(route);
        }

        public bool UpdateRoute(Route route)
        {
            var updateRoute = _routesRepo.Get(x => x.RouteId == route.RouteId);
            var shift = _shiftsRepo.Get(x => x.ShiftId == route.ShiftId);

            updateRoute.Length = route.Length;
            updateRoute.IsTrafficJam = route.IsTrafficJam;
            updateRoute.EstimatedTime = route.EstimatedTime;

            updateRoute.Shift = shift;

            return _routesRepo.Update(updateRoute);
        }

        public bool DeleteRoute(Guid Id)
        {
            return _routesRepo.Remove(_routesRepo.Get(x => x.RouteId == Id));
        }
    }
}
