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
    public class ShiftsViewModel : BaseViewModel
    {
        private readonly ContextRepository<InSQL.Shift> _repo;
        private IShift _selectedShift;
        private ObservableCollection<IShift> _Shifts;
        private string _updateBtnVisibility;

        public ShiftsViewModel()
        {
            _repo = new ContextRepository<InSQL.Shift>();
            _selectedShift = new Shift();
            UpdateBtnVisibility = StateService.StoreType == StoreType.InMemory ? "Hidden" : "Visible";

            LoadData();
        }

        public IShift SelectedShift
        {
            get => _selectedShift;
            set => SetProperty(ref _selectedShift, value);
        }

        public ObservableCollection<IShift> Shifts
        {
            get => _Shifts;
            set => SetProperty(ref _Shifts, value);
        }

        public string UpdateBtnVisibility
        {
            get => _updateBtnVisibility;
            set => SetProperty(ref _updateBtnVisibility, value);
        }

        public void LoadData()
        {
            if (StateService.StoreType == StoreType.InMemory)
                Shifts = new ObservableCollection<IShift>(InMemoryContext.Instance.Shifts);
            else
                Shifts = new ObservableCollection<IShift>(_repo.GetAll());
        }

        public void AddShift()
        {
            var newShift = SelectedShift.Clone() as Shift;

            Shifts.Add(newShift);

            if (StateService.StoreType == StoreType.InMemory)
                InMemoryContext.Instance.Shifts.Add(newShift);
            else
                _repo.Create((InSQL.Shift) SelectedShift);
        }

        public void UpdateShift()
        {
            if (StateService.StoreType == StoreType.InDatabase) _repo.Save();
        }

        public void RemoveShift()
        {
            Shifts.Remove(SelectedShift);

            if (StateService.StoreType == StoreType.InMemory)
            {
                InMemoryContext.Instance.Shifts = InMemoryContext.Instance.Shifts
                    .Where(x => x.ShiftId != SelectedShift.ShiftId).ToList();
                SelectedShift = new Shift();
            }
            else
            {
                _repo.Remove((InSQL.Shift) SelectedShift);
                SelectedShift = new InSQL.Shift();
            }
        }
    }
}