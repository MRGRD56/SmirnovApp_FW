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

namespace SmirnovApp.ViewModels.PagesViewModels
{
    public class ClientsPageViewModel
    {
        public ObservableCollection<IndividualClient> IndividualClients { get; } = 
            new ObservableCollection<IndividualClient>();

        public ObservableCollection<LegalEntityClient> LegalEntityClients { get; } = 
            new ObservableCollection<LegalEntityClient>();

        public ObservableCollection<Client> Clients { get; } = new ObservableCollection<Client>();

        private readonly SynchronizationContext _syncContext = SynchronizationContext.Current;

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
    }
}
