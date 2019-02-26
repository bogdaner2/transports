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
    public class TransportsViewModel : BaseViewModel
    {
        private readonly ContextRepository<InSQL.TechPassport> _repoPassports;

        private readonly ContextRepository<InSQL.Transport> _repoTransport;
        private ITransport _selectedTransport;
        private ObservableCollection<Guid> _techPassportsIds;
        private ObservableCollection<ITransport> _transports;

        private string _updateBtnVisibility;

        public TransportsViewModel()
        {
            _repoTransport = new ContextRepository<InSQL.Transport>();
            _repoPassports = new ContextRepository<InSQL.TechPassport>();
            _selectedTransport = new Transport();
            UpdateBtnVisibility = StateService.StoreType == StoreType.InMemory ? "Hidden" : "Visible";

            LoadData();
        }

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

        public ObservableCollection<Guid> TechPassportsIds
        {
            get => _techPassportsIds;
            set => SetProperty(ref _techPassportsIds, value);
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
                Transports = new ObservableCollection<ITransport>(InMemoryContext.Instance.Transports);
                TechPassportsIds =
                    new ObservableCollection<Guid>(
                        InMemoryContext.Instance.TechPassports.Select(x => x.TechPassportId));
            }
            else
            {
                Transports = new ObservableCollection<ITransport>(_repoTransport.GetAll());
                TechPassportsIds =
                    new ObservableCollection<Guid>(_repoPassports.GetAll().Select(x => x.TechPassportID));
            }
        }

        public void AddTransport()
        {
            if (StateService.StoreType == StoreType.InMemory)
            {
                var newTransport = SelectedTransport.Clone() as Transport;

                newTransport.TransportId = Guid.NewGuid();

                Transports.Add(newTransport);
                InMemoryContext.Instance.Transports.Add(newTransport);
            }
            else
            {
                var newTransport = SelectedTransport.Clone() as InSQL.Transport;

                newTransport.TransportId = Guid.NewGuid();

                _repoTransport.Create(newTransport);
            }
        }

        public void UpdateTransport()
        {
            if (StateService.StoreType == StoreType.InDatabase) _repoPassports.Save();
        }

        public void RemoveTransport()
        {
            if (StateService.StoreType == StoreType.InMemory)
            {
                InMemoryContext.Instance.Transports = InMemoryContext.Instance.Transports
                    .Where(x => x.TransportId != SelectedTransport.TransportId)
                    .ToList();
                Transports.Remove(SelectedTransport);
                SelectedTransport = new Transport();
            }
            else
            {
                _repoTransport.Remove((InSQL.Transport) SelectedTransport);
                Transports.Remove(SelectedTransport);
                SelectedTransport = new InSQL.Transport();
            }
        }
    }
}