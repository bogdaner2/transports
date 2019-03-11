using System;
using System.Collections.ObjectModel;
using System.Linq;
using Transports.Core.Contexts;
using Transports.Core.Interfaces.Models;
using Transports.Core.Models.InMemory;
using Transports.Core.Repositories;
using Transports.Core.Services;
using Transports.Desktop.MVVM;
using InSQL = Transports.Core.Models.SQL;

namespace Transports.Desktop.ViewModels
{
    public class DriversViewModel : BaseViewModel
    {
        private readonly ContextRepository<InSQL.Driver> _repo;
        private ObservableCollection<IDriver> _drivers;
        private IDriver _selectedDriver;
        private string _updateBtnVisibility;

        public DriversViewModel()
        {
            _repo = new ContextRepository<InSQL.Driver>();
            _selectedDriver = new Driver();
            UpdateBtnVisibility = StateService.StoreType == StoreType.InMemory ? "Hidden" : "Visible";

            LoadData();
        }

        public IDriver SelectedDriver
        {
            get => _selectedDriver;
            set => SetProperty(ref _selectedDriver, value);
        }

        public ObservableCollection<IDriver> Drivers
        {
            get => _drivers;
            set => SetProperty(ref _drivers, value);
        }

        public string UpdateBtnVisibility
        {
            get => _updateBtnVisibility;
            set => SetProperty(ref _updateBtnVisibility, value);
        }

        public void LoadData()
        {
            if (StateService.StoreType == StoreType.InMemory)
                Drivers = new ObservableCollection<IDriver>(InMemoryContext.Instance.Drivers);
            else
                Drivers = new ObservableCollection<IDriver>(_repo.GetAll());
        }

        public void AddDriver()
        {
            var newDriver = SelectedDriver.Clone() as Driver;

            var guid = Guid.NewGuid();

            newDriver.DriverId = guid;

            Drivers.Add(newDriver);

            if (StateService.StoreType == StoreType.InMemory)
                InMemoryContext.Instance.Drivers.Add(newDriver);
            else
                _repo.Create(new InSQL.Driver
                {
                    DriverId = guid, Name = SelectedDriver.Name, Age = SelectedDriver.Age, Rang = SelectedDriver.Rang
                });
        }

        public void UpdateDriver()
        {
            if (StateService.StoreType == StoreType.InDatabase)
            {
                var item = _repo.Get(x => x.DriverId == SelectedDriver.DriverId);

                item.Name = SelectedDriver.Name;
                item.Age = SelectedDriver.Age;
                item.Rang = SelectedDriver.Rang;

                _repo.Save();
            }
        }

        public void RemoveDriver()
        {
            if (StateService.StoreType == StoreType.InMemory)
            {
                InMemoryContext.Instance.Drivers = InMemoryContext.Instance.Drivers
                    .Where(x => x.DriverId != SelectedDriver.DriverId).ToList();
                Drivers.Remove(SelectedDriver);
                SelectedDriver = new Driver();
            }
            else
            {
                _repo.Remove((InSQL.Driver) SelectedDriver);
                Drivers.Remove(SelectedDriver);
                SelectedDriver = new InSQL.Driver();
            }
        }
    }
}