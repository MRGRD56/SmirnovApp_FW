using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using SmirnovApp.Common;
using SmirnovApp.Context;
using SmirnovApp.Model.DbModels;
using SmirnovApp.Views.Windows;

namespace SmirnovApp.ViewModels.PagesViewModels
{
    public class OwnersPageViewModel : ItemsListViewModel<Owner>, ICrudViewModel
    {
        public OwnersPageViewModel() : base(typeof(Estate))
        {
            
        }

        public ICommand AddCommand => new Command(async parameter =>
        {
            var dialog = new OwnerEditDialogWindow();
            if (dialog.ShowDialog() != true) return;
            var owner = dialog.Item;

            using (var db = new AppDbContext())
            {
                await db.Owners.AddAsync(owner);
                await db.SaveChangesAsync();

                Items.Add(owner);
            }
        });

        public ICommand EditCommand => new Command(async parameter =>
        {
            var dialog = new OwnerEditDialogWindow(SelectedItem);
            if (dialog.ShowDialog() != true) return;
            var owner = dialog.Item;

            using (var db = new AppDbContext())
            {
                var dbOwner = await db.Owners.FindAsync(SelectedItem.Id);

                dbOwner.CopyPropertiesFrom(owner);
                await db.SaveChangesAsync();
                
                SelectedItem.CopyPropertiesFrom(dbOwner);
            }
        }, parameter => SelectedItem != null);

        public ICommand RemoveCommand => new Command(async parameter =>
        {
            using (var db = new AppDbContext())
            {
                var dependentEstates = SelectedItem.Estates;

                var hasDependentEstates = dependentEstates.Any();

                var deletionWarning = hasDependentEstates
                    ? $"\n⚠ Будет удалено имуществ: {dependentEstates.Count}."
                    : "\nСвязанного имущества нет.";

                var mbox = MessageBox.Show($"Удалить владельца №{SelectedItem.Id}?{deletionWarning}", "Удаление владельца",
                    MessageBoxButton.OKCancel, hasDependentEstates ? MessageBoxImage.Warning : MessageBoxImage.Question);
                if (mbox != MessageBoxResult.OK) return;

                if (hasDependentEstates)
                {
                    db.Estates.RemoveRange(dependentEstates);
                }

                var dbOwner = await db.Owners.FindAsync(SelectedItem.Id);
                db.Owners.Remove(dbOwner);
                await db.SaveChangesAsync();
                Items.Remove(SelectedItem);
            }
        }, parameter => SelectedItem != null);
    }
}
