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
    public class ShiftsViewModel : BaseViewModel
    {
        private IShift _selectedShift;
        private ObservableCollection<IShift> _Shifts;
        private string _updateBtnVisibility;

        private readonly ContextRepository<InSQL.Shift> _repo;
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

        public ShiftsViewModel()
        {
            _repo = new ContextRepository<InSQL.Shift>();
            _selectedShift = new InMemory.Shift();
            UpdateBtnVisibility = StateService.StoreType == StoreType.InMemory ? "Hidden" : "Visible";

            LoadData();
        }

        public void LoadData()
        {
            if (StateService.StoreType == StoreType.InMemory)
            {
                Shifts = new ObservableCollection<IShift>(InMemoryContext.Instance.Shifts);
            }
            else
            {
                Shifts = new ObservableCollection<IShift>(_repo.GetAll());
            }
        }

        public void AddShift()
        {
            var newShift = SelectedShift.Clone() as InMemory.Shift;

            Shifts.Add(newShift);

            if (StateService.StoreType == StoreType.InMemory)
            {
                InMemoryContext.Instance.Shifts.Add(newShift);
            }
            else
            {
                _repo.Create((InSQL.Shift)SelectedShift);
            }
        }

        public void UpdateShift()
        {
            if (StateService.StoreType == StoreType.InDatabase)
            {
                _repo.Save();
            }
        }

        public void RemoveShift()
        {
            Shifts.Remove(SelectedShift);

            if (StateService.StoreType == StoreType.InMemory)
            {
                InMemoryContext.Instance.Shifts.Remove((InMemory.Shift)SelectedShift);
                SelectedShift = new InMemory.Shift();
            }
            else
            {
                _repo.Remove((InSQL.Shift)SelectedShift);
                SelectedShift = new InSQL.Shift();
            }
        }
    }
}
