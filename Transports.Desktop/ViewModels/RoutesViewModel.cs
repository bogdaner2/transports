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
        private readonly ContextRepository<InSQL.Route> _routesRepo;
        private readonly ContextRepository<InSQL.Shift> _shiftRepo;

        private ObservableCollection<IRoute> _Routes;
        private ObservableCollection<Guid> _ShiftIds;

        private IRoute _selectedRoute;
        private string _updateBtnVisibility;

        public RoutesViewModel()
        {
            _routesRepo = new ContextRepository<InSQL.Route>();
            _shiftRepo = new ContextRepository<InSQL.Shift>();
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

        public ObservableCollection<Guid> ShiftIds
        {
            get => _ShiftIds;
            set => SetProperty(ref _ShiftIds, value);
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
                ShiftIds = new ObservableCollection<Guid>(InMemoryContext.Instance.Shifts.Select(x => x.ShiftId));
            }
            else
            {
                Routes = new ObservableCollection<IRoute>(_routesRepo.GetAll());
                ShiftIds = new ObservableCollection<Guid>(_shiftRepo.GetAll().Select(x => x.ShiftId));
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
                _routesRepo.Create((InSQL.Route) SelectedRoute);
            }
        }

        public void UpdateRoute()
        {
            if (StateService.StoreType == StoreType.InDatabase)
            {
                _routesRepo.Save();
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
                _routesRepo.Remove((InSQL.Route) SelectedRoute);
                Routes.Remove(SelectedRoute);
                SelectedRoute = new InSQL.Route();
            }
        }
    }
}