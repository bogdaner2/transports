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
    public class RoutesViewModel : BaseViewModel
    {
        private readonly ContextRepository<InSQL.Route> _repo;
        private ObservableCollection<IRoute> _Routes;
        private IRoute _selectedRoute;
        private string _updateBtnVisibility;

        public RoutesViewModel()
        {
            _repo = new ContextRepository<InSQL.Route>();
            _selectedRoute = new Route();
            UpdateBtnVisibility = StateService.StoreType == StoreType.InMemory ? "Hidden" : "Visible";

            LoadData();
        }

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
            var newRoute = SelectedRoute.Clone() as Route;

            newRoute.RouteId = Guid.NewGuid();

            Routes.Add(newRoute);

            if (StateService.StoreType == StoreType.InMemory)
            {
                InMemoryContext.Instance.Routes.Add(newRoute);
            }
            else
            {
                _repo.Create((InSQL.Route) SelectedRoute);
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
            if (StateService.StoreType == StoreType.InMemory)
            {
                InMemoryContext.Instance.Routes = InMemoryContext.Instance.Routes.Where(x => x.RouteId != SelectedRoute.RouteId).ToList();
                Routes.Remove(SelectedRoute);
                SelectedRoute = new Route();
            }
            else
            {
                _repo.Remove((InSQL.Route) SelectedRoute);
                Routes.Remove(SelectedRoute);
                SelectedRoute = new InSQL.Route();
            }
        }
    }
}