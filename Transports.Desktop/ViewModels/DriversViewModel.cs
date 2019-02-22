using System.Collections.ObjectModel;
using Transports.Core;
using Transports.Core.Contexts;
using Transports.Core.Models;
using Transports.Desktop.MVVM;

namespace Transports.Desktop.ViewModels
{
    public class DriversViewModel : BaseViewModel
    {
        private Driver _selectedDriver;
        private ObservableCollection<Driver> _drivers;
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

        public DriversViewModel()
        {
            _repo = new ContextRepository<Driver>();
        }

        public void LoadData()
        {
            Drivers = new ObservableCollection<Driver>();
            
            if(Drivers.Count != 0)
                SelectedDriver = Drivers[0];
        }

        public Driver AddDriver()
        {
            Drivers.Add(SelectedDriver);

            if (StateService.StoreType == StoreType.InMemory)
            {
                InMemoryContext.Instance.Drivers.Add(SelectedDriver);
            }
            else
            {
                _repo.Create(SelectedDriver);
            }

            return SelectedDriver;
        }
    }
}
