using Microsoft.EntityFrameworkCore;
using SmirnovApp.Context;
using SmirnovApp.Model.DbModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using SmirnovApp.Common;
using SmirnovApp.Views.Windows;

namespace SmirnovApp.ViewModels.PagesViewModels
{
    public class ClientsPageViewModel : BaseViewModel, ICrudViewModel
    {
        public ObservableCollection<IndividualClient> IndividualClients { get; } = 
            new ObservableCollection<IndividualClient>();

        public ObservableCollection<LegalEntityClient> LegalEntityClients { get; } = 
            new ObservableCollection<LegalEntityClient>();

        public ObservableCollection<Client> Clients { get; } = new ObservableCollection<Client>();


        public int CurrentTab
        {
            get => _currentTab;
            set
            {
                _currentTab = value;
                OnPropertyChanged(nameof(SelectedClient));
            }
        }

        private Type CurrentTabClientType
        {
            get
            {
                switch (CurrentTab)
                {
                    case 0:
                        return typeof(IndividualClient);
                    case 1:
                        return typeof(LegalEntityClient);
                    default:
                        throw new Exception("CurrentTab must be 0 or 1");
                }
            }
        }

        public IndividualClient SelectedIndividualClient
        {
            get => _selectedIndividualClient;
            set
            {
                _selectedIndividualClient = value;
                OnPropertyChanged(nameof(SelectedClient));
            }
        }

        public LegalEntityClient SelectedLegalEntityClient
        {
            get => _selectedLegalEntityClient;
            set
            {
                _selectedLegalEntityClient = value;
                OnPropertyChanged(nameof(SelectedClient));
            }
        }

        private Client SelectedClient
        {
            get
            {
                switch (CurrentTab)
                {
                    case 0:
                        return SelectedIndividualClient;
                    case 1:
                        return SelectedLegalEntityClient;
                    default:
                        throw new Exception("CurrentTab must be 0 or 1");
                }
            }
        }


        private readonly SynchronizationContext _syncContext = SynchronizationContext.Current;
        private int _currentTab;
        private IndividualClient _selectedIndividualClient;
        private LegalEntityClient _selectedLegalEntityClient;

        public ClientsPageViewModel()
        {
            LoadData();
        }

        private async void LoadData()
        {
            IndividualClients.Clear();
            LegalEntityClients.Clear();
            Clients.Clear();

            using (var db = new AppDbContext())
            {
                await db.IndividualClients.LoadAsync();
                await db.LegalEntityClients.LoadAsync();

                await db.Clients.ForEachAsync(client => 
                {
                    _syncContext.Send(_ =>
                    {
                        AddClientToCollection(client);
                    }, null);
                });
            }
        }

        private void AddClientToCollection(Client client)
        {
            Clients.Add(client);
            if (client is IndividualClient individualClient)
            {
                IndividualClients.Add(individualClient);
            }
            if (client is LegalEntityClient legalEntityClient)
            {
                LegalEntityClients.Add(legalEntityClient);
            }
        }

        private void RemoveClientFromCollection(Client client)
        {
            Clients.Remove(client);
            if (client is IndividualClient individualClient)
            {
                IndividualClients.Remove(individualClient);
            }
            if (client is LegalEntityClient legalEntityClient)
            {
                LegalEntityClients.Remove(legalEntityClient);
            }
        }

        /// <summary>
        /// Команда добавления клиента.
        /// </summary>
        public ICommand AddCommand => new Command(async _ =>
        {
            var dialogWindow = new ClientEditDialogWindow(CurrentTabClientType);
            var dialogResult = dialogWindow.ShowDialog();
            if (dialogResult != true) return;
            var client = dialogWindow.Item;
            using (var db = new AppDbContext())
            {
                await db.Clients.AddAsync(client);
                await db.SaveChangesAsync();
                AddClientToCollection(client);
            }
        });

        public ICommand EditCommand => new Command(async _ =>
        {
            var dialogWindow = new ClientEditDialogWindow(SelectedClient);
            var dialogResult = dialogWindow.ShowDialog();
            if (dialogResult != true) return;
            var client = dialogWindow.Item;
            using (var db = new AppDbContext())
            {
                var dbClient = await db.Clients.FindAsync(client.Id);
                dbClient.CopyPropertiesFrom(client);
                await db.SaveChangesAsync();
                SelectedClient.CopyPropertiesFrom(dbClient);
            }
        }, _ => SelectedClient != null);

        public ICommand RemoveCommand => new Command(async _ =>
        {
            var mbox = MessageBox.Show($"Удалить клиента №{SelectedClient.Id}?", 
                "Удаление", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            if (mbox != MessageBoxResult.OK) return;

            using (var db = new AppDbContext())
            {
                var dbClint = await db.Clients.FindAsync(SelectedClient.Id);
                db.Remove(dbClint);
                RemoveClientFromCollection(SelectedClient);
                await db.SaveChangesAsync();
            }
        }, _ => SelectedClient != null);
    }
}
