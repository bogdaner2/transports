using System;
using System.Collections.ObjectModel;
using System.Linq;
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
        private ObservableCollection<Guid> _techPassportsIds;

        private string _updateBtnVisibility;

        private readonly ContextRepository<InSQL.Transport> _repoTransport;
        private readonly ContextRepository<InSQL.TechPassport> _repoPassports;

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

        public TransportsViewModel()
        {
            _repoTransport = new ContextRepository<InSQL.Transport>();
            _repoPassports = new ContextRepository<InSQL.TechPassport>();
            _selectedTransport = new InMemory.Transport();
            UpdateBtnVisibility = StateService.StoreType == StoreType.InMemory ? "Hidden" : "Visible";

            LoadData();
        }

        public void LoadData()
        {
            if (StateService.StoreType == StoreType.InMemory)
            {
                Transports = new ObservableCollection<ITransport>(InMemoryContext.Instance.Transports);
                TechPassportsIds = new ObservableCollection<Guid>(InMemoryContext.Instance.TechPassports.Select(x => x.TechPassportId));
            }
            else
            {
                Transports = new ObservableCollection<ITransport>(_repoTransport.GetAll());
                TechPassportsIds = new ObservableCollection<Guid>(_repoPassports.GetAll().Select(x => x.TechPassportID));
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
                _repoTransport.Create((InSQL.Transport)SelectedTransport);
            }
        }

        public void UpdateTransport()
        {
            if (StateService.StoreType == StoreType.InDatabase)
            {
                _repoPassports.Save();
            }
        }

        public void RemoveTransport()
        {
            Transports.Remove(SelectedTransport);

            if (StateService.StoreType == StoreType.InMemory)
            {
                InMemoryContext.Instance.Transports.Remove((InMemory.Transport)SelectedTransport);

                SelectedTransport = new InMemory.Transport();
            }
            else
            {
                _repoTransport.Remove((InSQL.Transport)SelectedTransport);

                SelectedTransport = new InSQL.Transport();
            }
        }
    }
}
