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
    public class TechPassportsViewModel : BaseViewModel
    {
        private readonly ContextRepository<InSQL.TechPassport> _repo;
        private ITechPassport _selectedTechPassport;
        private ObservableCollection<ITechPassport> _TechPassports;
        private string _updateBtnVisibility;

        public TechPassportsViewModel()
        {
            _repo = new ContextRepository<InSQL.TechPassport>();
            _selectedTechPassport = new TechPassport();
            UpdateBtnVisibility = StateService.StoreType == StoreType.InMemory ? "Hidden" : "Visible";

            LoadData();
        }

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

        public void LoadData()
        {
            if (StateService.StoreType == StoreType.InMemory)
                TechPassports = new ObservableCollection<ITechPassport>(InMemoryContext.Instance.TechPassports);
            else
                TechPassports = new ObservableCollection<ITechPassport>(_repo.GetAll());
        }

        public void AddTechPassport()
        {
            var newTechPassport = SelectedTechPassport.Clone() as TechPassport;

            newTechPassport.TechPassportId = Guid.NewGuid();

            TechPassports.Add(newTechPassport);

            if (StateService.StoreType == StoreType.InMemory)
                InMemoryContext.Instance.TechPassports.Add(newTechPassport);
            else
                _repo.Create((InSQL.TechPassport) SelectedTechPassport);
        }

        public void UpdateTechPassport()
        {
            if (StateService.StoreType == StoreType.InDatabase) _repo.Save();
        }

        public void RemoveTechPassport()
        {
            if (StateService.StoreType == StoreType.InMemory)
            {
                InMemoryContext.Instance.TechPassports = InMemoryContext.Instance.TechPassports
                    .Where(x => x.TechPassportId != SelectedTechPassport.TechPassportId)
                    .ToList();
                TechPassports.Remove(SelectedTechPassport);
                SelectedTechPassport = new TechPassport();
            }
            else
            {
                _repo.Remove((InSQL.TechPassport) SelectedTechPassport);
                TechPassports.Remove(SelectedTechPassport);
                SelectedTechPassport = new InSQL.TechPassport();
            }
        }
    }
}