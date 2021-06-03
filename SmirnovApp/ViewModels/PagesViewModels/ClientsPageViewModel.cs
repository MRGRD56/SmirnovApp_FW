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
using SmirnovApp.Common;
using SmirnovApp.Views.Windows;

namespace SmirnovApp.ViewModels.PagesViewModels
{
    public class ClientsPageViewModel : BaseViewModel
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
                        Clients.Add(client);
                        if (client is IndividualClient individualClient)
                        {
                            IndividualClients.Add(individualClient);
                        }
                        if (client is LegalEntityClient legalEntityClient)
                        {
                            LegalEntityClients.Add(legalEntityClient);
                        }
                    }, null);
                });
            }
        }

        public Command AddCommand => new Command(_ =>
        {
            var dialogWindow = new ClientEditDialogWindow(CurrentTabClientType);
            dialogWindow.ShowDialog();
        });

        public Command EditCommand => new Command(_ =>
        {
            var dialogWindow = new ClientEditDialogWindow(SelectedClient);
            dialogWindow.ShowDialog();
        }, _ => SelectedClient != null);

        public Command RemoveCommand => new Command(async _ =>
        {
            var mbox = MessageBox.Show($"Удалить клиента №{SelectedClient.Id}?", "Удаление", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            if (mbox != MessageBoxResult.OK) return;

            using (var db = new AppDbContext())
            {
                var dbClint = await db.Clients.FindAsync(SelectedClient.Id);
                db.Remove(dbClint);
                Clients.Remove(SelectedClient);
                await db.SaveChangesAsync();
            }
        }, _ => SelectedClient != null);
    }
}
