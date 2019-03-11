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
    public class DriverShiftsViewModel : BaseViewModel
    {
        private readonly ContextRepository<InSQL.DriverShift> _repoDriverShift;
        private readonly ContextRepository<InSQL.Driver> _repoDrivers;
        private readonly ContextRepository<InSQL.Shift> _repoShifts;

        private ObservableCollection<IDriverShift> _DriverShifts;
        private IDriverShift _selectedDriverShift;
        private ObservableCollection<Guid> _driversIds;
        private ObservableCollection<Guid> _shiftsIds;


        private string _updateBtnVisibility;

        public DriverShiftsViewModel()
        {
            _repoDriverShift = new ContextRepository<InSQL.DriverShift>();
            _repoDrivers = new ContextRepository<InSQL.Driver>();
            _repoShifts = new ContextRepository<InSQL.Shift>();

            _selectedDriverShift = new DriverShift();
            UpdateBtnVisibility = StateService.StoreType == StoreType.InMemory ? "Hidden" : "Visible";

            LoadData();
        }

        public IDriverShift SelectedDriverShift
        {
            get => _selectedDriverShift;
            set => SetProperty(ref _selectedDriverShift, value);
        }

        public ObservableCollection<IDriverShift> DriverShifts
        {
            get => _DriverShifts;
            set => SetProperty(ref _DriverShifts, value);
        }

        public ObservableCollection<Guid> DriversIds
        {
            get => _driversIds;
            set => SetProperty(ref _driversIds, value);
        }

        public ObservableCollection<Guid> ShiftIds
        {
            get => _shiftsIds;
            set => SetProperty(ref _shiftsIds, value);
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
                DriverShifts = new ObservableCollection<IDriverShift>(InMemoryContext.Instance.DriverShifts);
                DriversIds =
                    new ObservableCollection<Guid>(
                        InMemoryContext.Instance.Drivers.Select(x => x.DriverId));
                ShiftIds =
                    new ObservableCollection<Guid>(
                        InMemoryContext.Instance.Shifts.Select(x => x.ShiftId));
            }
            else
            {
                DriverShifts = new ObservableCollection<IDriverShift>(_repoDriverShift.GetAll());
                DriversIds =
                    new ObservableCollection<Guid>(_repoDrivers.GetAll().Select(x => x.DriverId));
                ShiftIds =
                    new ObservableCollection<Guid>(_repoShifts.GetAll().Select(x => x.ShiftId));
            }
        }

        public void AddDriverShift()
        {
            if (StateService.StoreType == StoreType.InMemory)
            {
                var newDriverShift = SelectedDriverShift.Clone() as DriverShift;
                newDriverShift.DriverShiftId = Guid.NewGuid();

                DriverShifts.Add(newDriverShift);
                InMemoryContext.Instance.DriverShifts.Add(newDriverShift);
            }
            else
            {
                var newDriverShift = SelectedDriverShift.Clone() as InSQL.DriverShift;
                newDriverShift.DriverShiftId = Guid.NewGuid();

                DriverShifts.Add(newDriverShift);
                _repoDriverShift.Create(newDriverShift);
            }
        }

        public void UpdateDriverShift()
        {
            if (StateService.StoreType == StoreType.InDatabase)
            {
                _repoDriverShift.Save();
            }
        }

        public void RemoveDriverShift()
        {
            if (StateService.StoreType == StoreType.InMemory)
            {
                InMemoryContext.Instance.DriverShifts = InMemoryContext.Instance.DriverShifts
                    .Where(x => x.DriverShiftId != SelectedDriverShift.DriverShiftId).ToList();
                DriverShifts.Remove(SelectedDriverShift);
                SelectedDriverShift = new DriverShift();
            }
            else
            {
                _repoDriverShift.Remove((InSQL.DriverShift) SelectedDriverShift);
                DriverShifts.Remove(SelectedDriverShift);
                SelectedDriverShift = new InSQL.DriverShift();
            }
        }
    }
}