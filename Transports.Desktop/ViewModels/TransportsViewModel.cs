using System.Collections.ObjectModel;
using Transports.Core.Contexts;
using Transports.Core.Models;
using Transports.Core.Repositories;
using Transports.Core.Services;
using Transports.Desktop.MVVM;

namespace Transports.Desktop.ViewModels
{
    public class TransportsViewModel : BaseViewModel
    {
        private Transport _selectedTransport;
        private ObservableCollection<Transport> _transports;
        private ObservableCollection<TechPassport> _techPassports;

        private string _updateBtnVisibility;

        private readonly ContextRepository<Transport> _repo;
        public Transport SelectedTransport
        {
            get => _selectedTransport;
            set => SetProperty(ref _selectedTransport, value);
        }

        public ObservableCollection<Transport> Transports
        {
            get => _transports;
            set => SetProperty(ref _transports, value);
        }

        public ObservableCollection<TechPassport> TechPassports
        {
            get => _techPassports;
            set => SetProperty(ref _techPassports, value);
        }

        public string UpdateBtnVisibility
        {
            get => _updateBtnVisibility;
            set => SetProperty(ref _updateBtnVisibility, value);
        }

        public TransportsViewModel()
        {
            _repo = new ContextRepository<Transport>();
            _selectedTransport = new Transport();
            UpdateBtnVisibility = StateService.StoreType == StoreType.InMemory ? "Hidden" : "Visible";

            LoadData();
        }

        public void LoadData()
        {
            if (StateService.StoreType == StoreType.InMemory)
            {
                Transports = new ObservableCollection<Transport>(InMemoryContext.Instance.Transports);
                TechPassports = new ObservableCollection<TechPassport>(InMemoryContext.Instance.TechPassports);
            }
            else
            {
                Transports = new ObservableCollection<Transport>(_repo.GetAll());
            }
        }

        public void AddTransport()
        {
            var newTransport = SelectedTransport.Clone() as Transport;

            Transports.Add(newTransport);

            if (StateService.StoreType == StoreType.InMemory)
            {
                InMemoryContext.Instance.Transports.Add(newTransport);
            }
            else
            {
                _repo.Create(SelectedTransport);
            }
        }

        public void UpdateTransport()
        {
            if (StateService.StoreType == StoreType.InDatabase)
            {
                _repo.Save();
            }
        }

        public void RemoveTransport()
        {
            Transports.Remove(SelectedTransport);

            if (StateService.StoreType == StoreType.InMemory)
            {
                InMemoryContext.Instance.Transports.Remove(SelectedTransport);
            }

            _repo.Remove(SelectedTransport);

            _selectedTransport = new Transport();
        }
    }
}
