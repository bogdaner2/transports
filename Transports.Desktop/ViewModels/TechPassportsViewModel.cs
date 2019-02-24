using System.Collections.ObjectModel;
using Transports.Core.Contexts;
using Transports.Core.Interfaces.Models;
using InMemory = Transports.Core.Models.InMemory;
using InSQL = Transports.Core.Models.SQL;
using Transports.Core.Repositories;
using Transports.Core.Services;
using Transports.Desktop.MVVM;

namespace Transports.Desktop.ViewModels
{
    public class TechPassportsViewModel : BaseViewModel
    {
        private ITechPassport _selectedTechPassport;
        private ObservableCollection<ITechPassport> _TechPassports;
        private string _updateBtnVisibility;

        private readonly ContextRepository<InSQL.TechPassport> _repo;
        public ITechPassport SelectedTechPassport
        {
            get => _selectedTechPassport;
            set => SetProperty(ref _selectedTechPassport, value);
        }

        public ObservableCollection<ITechPassport> TechPassports
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
            _repo = new ContextRepository<InSQL.TechPassport>();
            _selectedTechPassport = new InMemory.TechPassport();
            UpdateBtnVisibility = StateService.StoreType == StoreType.InMemory ? "Hidden" : "Visible";

            LoadData();
        }

        public void LoadData()
        {
            if (StateService.StoreType == StoreType.InMemory)
            {
                TechPassports = new ObservableCollection<ITechPassport>(InMemoryContext.Instance.TechPassports);
            }
            else
            {
                TechPassports = new ObservableCollection<ITechPassport>(_repo.GetAll());
            }
        }

        public void AddTechPassport()
        {
            var newTechPassport = SelectedTechPassport.Clone() as InMemory.TechPassport;

            TechPassports.Add(newTechPassport);

            if (StateService.StoreType == StoreType.InMemory)
            {
                InMemoryContext.Instance.TechPassports.Add(newTechPassport);
            }
            else
            {
                _repo.Create((InSQL.TechPassport)SelectedTechPassport);
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
                InMemoryContext.Instance.TechPassports.Remove((InMemory.TechPassport)SelectedTechPassport);
            }

            _repo.Remove((InSQL.TechPassport)SelectedTechPassport);

        }
    }
}
