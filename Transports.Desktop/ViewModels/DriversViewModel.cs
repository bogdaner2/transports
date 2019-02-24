using System.Collections.ObjectModel;
using Transports.Core.Contexts;
using Transports.Core.Models;
using Transports.Core.Repositories;
using Transports.Core.Services;
using Transports.Desktop.MVVM;

namespace Transports.Desktop.ViewModels
{
    public class DriversViewModel : BaseViewModel
    {
        private Driver _selectedDriver;
        private ObservableCollection<Driver> _drivers;
        private string _updateBtnVisibility;

        private readonly ContextRepository<Driver> _repo;
        public Driver SelectedDriver
        {
            get => _selectedDriver;
            set => SetProperty(ref _selectedDriver, value);
        }

        public ObservableCollection<Driver> Drivers
        {
            get => _drivers;
            set => SetProperty(ref _drivers, value);
        }

        public string UpdateBtnVisibility
        {
            get => _updateBtnVisibility;
            set => SetProperty(ref _updateBtnVisibility, value);
        }

        public DriversViewModel()
        {
            _repo = new ContextRepository<Driver>();
            _selectedDriver = new Driver();
            UpdateBtnVisibility = StateService.StoreType == StoreType.InMemory ? "Hidden" : "Visible";

            LoadData();
        }

        public void LoadData()
        {
            if (StateService.StoreType == StoreType.InMemory)
            {
                Drivers = new ObservableCollection<Driver>(InMemoryContext.Instance.Drivers);
            }
            else
            {
                Drivers = new ObservableCollection<Driver>(_repo.GetAll());
            }
        }

        public void AddDriver()
        {
            var newDriver = SelectedDriver.Clone() as Driver;

            Drivers.Add(newDriver);

            if (StateService.StoreType == StoreType.InMemory)
            {
                InMemoryContext.Instance.Drivers.Add(newDriver);
            }
            else
            {
                _repo.Create(SelectedDriver);
            }
        }

        public void UpdateDriver()
        {
            if (StateService.StoreType == StoreType.InDatabase)
            {
                _repo.Save();
            }
        }

        public void RemoveDriver()
        {
            Drivers.Remove(SelectedDriver);

            if (StateService.StoreType == StoreType.InMemory)
            {
                InMemoryContext.Instance.Drivers.Remove(SelectedDriver);
            }

            _repo.Remove(SelectedDriver);

            _selectedDriver = new Driver();
        }
    }
}
