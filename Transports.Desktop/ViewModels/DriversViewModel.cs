using System.Collections.ObjectModel;
using Transports.Core.Contexts;
using Transports.Core.Interfaces;
using Transports.Core.Interfaces.Models;
using Transports.Core.Repositories;
using Transports.Core.Services;
using Transports.Desktop.MVVM;
using InMemory = Transports.Core.Models.InMemory;
using InSQL = Transports.Core.Models.SQL;

namespace Transports.Desktop.ViewModels
{
    public class DriversViewModel : BaseViewModel
    {
        private IDriver _selectedDriver;
        private ObservableCollection<IDriver> _drivers;
        private string _updateBtnVisibility;

        private readonly ContextRepository<InSQL.Driver> _repo;
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

        public DriversViewModel()
        {
            _repo = new ContextRepository<InSQL.Driver>();
            _selectedDriver = new InMemory.Driver();
            UpdateBtnVisibility = StateService.StoreType == StoreType.InMemory ? "Hidden" : "Visible";

            LoadData();
        }

        public void LoadData()
        {
            if (StateService.StoreType == StoreType.InMemory)
            {
                Drivers = new ObservableCollection<IDriver>(InMemoryContext.Instance.Drivers);
            }
            else
            {
                Drivers = new ObservableCollection<IDriver>(_repo.GetAll());
            }
        }

        public void AddDriver()
        {
            var newDriver = SelectedDriver.Clone() as InMemory.Driver;

            Drivers.Add(newDriver);

            if (StateService.StoreType == StoreType.InMemory)
            {
                InMemoryContext.Instance.Drivers.Add(newDriver);
            }
            else
            {
                _repo.Create((InSQL.Driver)SelectedDriver);
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
                InMemoryContext.Instance.Drivers.Remove((InMemory.Driver)SelectedDriver);
            }

            _repo.Remove((InSQL.Driver)SelectedDriver);

        }
    }
}
