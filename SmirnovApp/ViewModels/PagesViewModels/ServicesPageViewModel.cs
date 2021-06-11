using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto.Digests;
using SmirnovApp.Common;
using SmirnovApp.Context;
using SmirnovApp.Model.DbModels;
using SmirnovApp.Views.Windows;

namespace SmirnovApp.ViewModels.PagesViewModels
{
    public class ServicesPageViewModel : ItemsListViewModel<Service>, ICrudViewModel
    {
        public ServicesPageViewModel() : base(typeof(Contract))
        {
            
        }

        public ICommand AddCommand => new Command(async parameter =>
        {
            var dialog = new ServiceEditDialogWindow();
            if (dialog.ShowDialog() != true) return;

            var service = dialog.Item;
            using (var db = new AppDbContext())
            {
                await db.Services.AddAsync(service);
                await db.SaveChangesAsync();
                Items.Add(service);
            }
        });

        public ICommand EditCommand => new Command(async parameter =>
        {
            var dialog = new ServiceEditDialogWindow(SelectedItem);
            if (dialog.ShowDialog() != true) return;

            var service = dialog.Item;
            using (var db = new AppDbContext())
            {
                var dbService = await db.Services.FindAsync(service.Id);

                dbService.CopyPropertiesFrom(service);
                await db.SaveChangesAsync();

                SelectedItem.CopyPropertiesFrom(dbService);
            }
        }, parameter => SelectedItem != null);

        public ICommand RemoveCommand => new Command(async parameter =>
        {
            using (var db = new AppDbContext())
            {
                var dependentContracts = SelectedItem.Contracts;

                var hasDependentContracts = dependentContracts.Any();

                var deletionWarning = hasDependentContracts
                    ? $"\n⚠ Будет удалено договоров: {dependentContracts.Count}."
                    : "\nСвязанных договоров нет.";

                var mbox = MessageBox.Show($"Удалить услугу №{SelectedItem.Id}?{deletionWarning}", "Удаление услуги",
                    MessageBoxButton.OKCancel, hasDependentContracts ? MessageBoxImage.Warning : MessageBoxImage.Question);
                if (mbox != MessageBoxResult.OK) return;

                if (hasDependentContracts)
                {
                    db.Contracts.RemoveRange(dependentContracts);
                }

                var dbService = await db.Services.FindAsync(SelectedItem.Id);
                db.Services.Remove(dbService);
                await db.SaveChangesAsync();
                Items.Remove(SelectedItem);
            }
        }, parameter => SelectedItem != null);
    }
}
