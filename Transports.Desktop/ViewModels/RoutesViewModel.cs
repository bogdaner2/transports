using System.Collections.ObjectModel;
using Transports.Core.Contexts;
using Transports.Core.Interfaces.Models;
using Transports.Core.Repositories;
using Transports.Core.Services;
using Transports.Desktop.MVVM;

namespace Transports.Desktop.ViewModels
{
    public class RoutesViewModel : BaseViewModel
    {
        private IRoute _selectedRoute;
        private ObservableCollection<IRoute> _Routes;
        private string _updateBtnVisibility;

        private readonly ContextRepository<Core.Models.SQL.Route> _repo;
        public IRoute SelectedRoute
        {
            get => _selectedRoute;
            set => SetProperty(ref _selectedRoute, value);
        }

        public ObservableCollection<IRoute> Routes
        {
            get => _Routes;
            set => SetProperty(ref _Routes, value);
        }

        public string UpdateBtnVisibility
        {
            get => _updateBtnVisibility;
            set => SetProperty(ref _updateBtnVisibility, value);
        }

        public RoutesViewModel()
        {
            _repo = new ContextRepository<Core.Models.SQL.Route>();
            _selectedRoute = new Core.Models.InMemory.Route();
            UpdateBtnVisibility = StateService.StoreType == StoreType.InMemory ? "Hidden" : "Visible";

            LoadData();
        }

        public void LoadData()
        {
            if (StateService.StoreType == StoreType.InMemory)
            {
                Routes = new ObservableCollection<IRoute>(InMemoryContext.Instance.Routes);
            }
            else
            {
                Routes = new ObservableCollection<IRoute>(_repo.GetAll());
            }
        }

        public void AddRoute()
        {
            var newRoute = SelectedRoute.Clone() as Core.Models.InMemory.Route;

            Routes.Add(newRoute);

            if (StateService.StoreType == StoreType.InMemory)
            {
                InMemoryContext.Instance.Routes.Add(newRoute);
            }
            else
            {
                _repo.Create((Core.Models.SQL.Route)SelectedRoute);
            }
        }

        public void UpdateRoute()
        {
            if (StateService.StoreType == StoreType.InDatabase)
            {
                _repo.Save();
            }
        }

        public void RemoveRoute()
        {
            Routes.Remove(SelectedRoute);

            if (StateService.StoreType == StoreType.InMemory)
            {
                InMemoryContext.Instance.Routes.Remove((Core.Models.InMemory.Route)SelectedRoute);
                SelectedRoute = new Core.Models.InMemory.Route();
            }
            else
            {
                _repo.Remove((Core.Models.SQL.Route)SelectedRoute);
                SelectedRoute = new Core.Models.SQL.Route();
            }
        }
    }
}
