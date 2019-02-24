using System.Collections.ObjectModel;
using Transports.Core.Contexts;
using Transports.Core.Interfaces.Models;
using Transports.Core.Repositories;
using Transports.Core.Services;
using Transports.Desktop.MVVM;
using InMemory = Transports.Core.Models.InMemory;
using InSQL = Transports.Core.Models.SQL;

namespace Transports.Desktop.ViewModels
{
    public class TransportsViewModel : BaseViewModel
    {
        private ITransport _selectedTransport;
        private ObservableCollection<ITransport> _transports;
        private ObservableCollection<ITechPassport> _techPassports;

        private string _updateBtnVisibility;

        private readonly ContextRepository<InSQL.Transport> _repo;
        public ITransport SelectedTransport
        {
            get => _selectedTransport;
            set => SetProperty(ref _selectedTransport, value);
        }

        public ObservableCollection<ITransport> Transports
        {
            get => _transports;
            set => SetProperty(ref _transports, value);
        }

        public ObservableCollection<ITechPassport> TechPassports
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
            _repo = new ContextRepository<InSQL.Transport>();
            _selectedTransport = new InMemory.Transport();
            UpdateBtnVisibility = StateService.StoreType == StoreType.InMemory ? "Hidden" : "Visible";

            LoadData();
        }

        public void LoadData()
        {
            if (StateService.StoreType == StoreType.InMemory)
            {
                Transports = new ObservableCollection<ITransport>(InMemoryContext.Instance.Transports);
                TechPassports = new ObservableCollection<ITechPassport>(InMemoryContext.Instance.TechPassports);
            }
            else
            {
                Transports = new ObservableCollection<ITransport>(_repo.GetAll());
            }
        }

        public void AddTransport()
        {
            var newTransport = SelectedTransport.Clone();

            Transports.Add((ITransport)newTransport);

            if (StateService.StoreType == StoreType.InMemory)
            {
                InMemoryContext.Instance.Transports.Add((InMemory.Transport)newTransport);
            }
            else
            {
                _repo.Create((InSQL.Transport)SelectedTransport);
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
                InMemoryContext.Instance.Transports.Remove((InMemory.Transport)SelectedTransport);
            }

            _repo.Remove((InSQL.Transport)SelectedTransport);
        }
    }
}
