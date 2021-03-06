﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Transports.Core.DependencyInjection;
using Transports.Core.Interfaces;
using Transports.Core.Models.SQL;
using Transports.Core.Repositories;

namespace Transports.WCF.Service
{
    public class TransportsService : ITransportsService
    {
        private readonly IRepository<Driver> _driversRepo = new ContextRepository<Driver>();
        private readonly IRepository<Shift> _shiftsRepo = new ContextRepository<Shift>();
        private readonly IRepository<Route> _routesRepo = new ContextRepository<Route>(); 

        #region drivers

        [OperationBehavior(TransactionScopeRequired = true, TransactionAutoComplete = true)]
        public void CreateDriver(string driver)
        {
            try
            {
                Console.WriteLine("Recieved driver: " + driver);

                _driversRepo.Create(JsonConvert.DeserializeObject<Driver>(driver));
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);

            }
        }

        [OperationBehavior(TransactionScopeRequired = true, TransactionAutoComplete = true)]
        public void GetDrivers()
        {
            try
            {
                _driversRepo.GetAll();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);

            }
        }


        [OperationBehavior(TransactionScopeRequired = true, TransactionAutoComplete = true)]
        public void UpdateDriver(string driver)
        {
            try
            {
                Console.WriteLine("Recieved updated driver: " + driver);
                var driverObj = JsonConvert.DeserializeObject<Driver>(driver);

                var guid = driverObj.DriverId;
                var item = _driversRepo.Get(x => x.DriverId == guid);

                item.Name = driverObj.Name;
                item.Age = driverObj.Age;
                item.Rang = driverObj.Rang;

                _driversRepo.Update(item);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);

            }
        }

        [OperationBehavior(TransactionScopeRequired = true, TransactionAutoComplete = true)]
        public void RemoveDriver(string driverId)
        {
            try
            {
                Console.WriteLine("Remove driver with id: " + driverId);
                var guid = Guid.Parse(driverId);
                _driversRepo.Remove(_driversRepo.Get(x => x.DriverId == guid));
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
        }

        #endregion

        #region shifts

        [OperationBehavior(TransactionScopeRequired = true, TransactionAutoComplete = true)]
        public void CreateShift(string shift)
        {
            try
            {
                Console.WriteLine("Recieved shift: " + shift);

                _shiftsRepo.Create(JsonConvert.DeserializeObject<Shift>(shift));
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);

            }
        }

        [OperationBehavior(TransactionScopeRequired = true, TransactionAutoComplete = true)]
        public void UpdateShift(string shift)
        {
            try
            {
                Console.WriteLine("Recieved updated shift: " + shift);
                var shiftObj = JsonConvert.DeserializeObject<Shift>(shift);

                var guid = shiftObj.ShiftId;
                var item = _shiftsRepo.Get(x => x.ShiftId == guid);

                item.Start = shiftObj.Start;
                item.End = shiftObj.End;

                _shiftsRepo.Update(item);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);

            }
        }

        [OperationBehavior(TransactionScopeRequired = true, TransactionAutoComplete = true)]
        public void RemoveShift(string shiftId)
        {
            try
            {
                Console.WriteLine("Remove shift with id: " + shiftId);
                var guid = Guid.Parse(shiftId);
                _shiftsRepo.Remove(_shiftsRepo.Get(x => x.ShiftId == guid));
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
        }

        #endregion

        #region routes

        [OperationBehavior(TransactionScopeRequired = true, TransactionAutoComplete = true)]
        public void CreateRoute(string route)
        {
            try
            {
                Console.WriteLine("Recieved route: " + route);

                var routeObj = JsonConvert.DeserializeObject<Route>(route);

                var shift = _shiftsRepo.Get(x => x.ShiftId == routeObj.RouteId);

                routeObj.Shift = shift;

                _routesRepo.Create(routeObj);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);

            }
        }

        [OperationBehavior(TransactionScopeRequired = true, TransactionAutoComplete = true)]
        public void UpdateRoute(string route)
        {
            try
            {
                Console.WriteLine("Recieved updated route: " + route);
                var routeObj = JsonConvert.DeserializeObject<Route>(route);

                var guid = routeObj.RouteId;

                var item = _routesRepo.Get(x => x.RouteId == guid);

                item.EstimatedTime = routeObj.EstimatedTime;
                item.IsTrafficJam = routeObj.IsTrafficJam;
                item.Length = routeObj.Length;

                item.Shift = _shiftsRepo.Get(x => x.ShiftId == routeObj.RouteId);

                _routesRepo.Update(item);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);

            }
        }

        [OperationBehavior(TransactionScopeRequired = true, TransactionAutoComplete = true)]
        public void RemoveRoute(string routeId)
        {
            try
            {
                Console.WriteLine("Remove route with id: " + routeId);
                var guid = Guid.Parse(routeId);
                _routesRepo.Remove(_routesRepo.Get(x => x.RouteId == guid));
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
        }

        #endregion

    }
}
