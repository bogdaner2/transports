using System.Collections.ObjectModel;
using Transports.Core.Contexts;
using Transports.Core.Models;
using Transports.Core.Repositories;
using Transports.Core.Services;
using Transports.Desktop.MVVM;

namespace Transports.Desktop.ViewModels
{
    public class TechPassportsViewModel : BaseViewModel
    {
        private TechPassport _selectedTechPassport;
        private ObservableCollection<TechPassport> _TechPassports;
        private string _updateBtnVisibility;

        private readonly ContextRepository<TechPassport> _repo;
        public TechPassport SelectedTechPassport
        {
            get => _selectedTechPassport;
            set => SetProperty(ref _selectedTechPassport, value);
        }

        public ObservableCollection<TechPassport> TechPassports
        {
            get => _TechPassports;
            set => SetProperty(ref _TechPassports, value);
        }

        public string UpdateBtnVisibility
        {
            get => _updateBtnVisibility;
            set => SetProperty(ref _updateBtnVisibility, value);
        }

        public TechPassportsViewModel()
        {
            _repo = new ContextRepository<TechPassport>();
            _selectedTechPassport = new TechPassport();
            UpdateBtnVisibility = StateService.StoreType == StoreType.InMemory ? "Hidden" : "Visible";

            LoadData();
        }

        public void LoadData()
        {
            if (StateService.StoreType == StoreType.InMemory)
            {
                TechPassports = new ObservableCollection<TechPassport>(InMemoryContext.Instance.TechPassports);
            }
            else
            {
                TechPassports = new ObservableCollection<TechPassport>(_repo.GetAll());
            }
        }

        public void AddTechPassport()
        {
            var newTechPassport = SelectedTechPassport.Clone() as TechPassport;

            TechPassports.Add(newTechPassport);

            if (StateService.StoreType == StoreType.InMemory)
            {
                InMemoryContext.Instance.TechPassports.Add(newTechPassport);
            }
            else
            {
                _repo.Create(SelectedTechPassport);
            }
        }

        public void UpdateTechPassport()
        {
            if (StateService.StoreType == StoreType.InDatabase)
            {
                _repo.Save();
            }
        }

        public void RemoveTechPassport()
        {
            TechPassports.Remove(SelectedTechPassport);

            if (StateService.StoreType == StoreType.InMemory)
            {
                InMemoryContext.Instance.TechPassports.Remove(SelectedTechPassport);
            }

            _repo.Remove(SelectedTechPassport);

            _selectedTechPassport = new TechPassport();
        }
    }
}
