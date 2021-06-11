using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using SmirnovApp.Common;
using SmirnovApp.Context;
using SmirnovApp.Extensions;
using SmirnovApp.Model.DbModels;
using SmirnovApp.Views.Windows;

namespace SmirnovApp.ViewModels.PagesViewModels
{
    public class EstatesPageViewModel : ItemsListViewModel<Estate>, ICrudViewModel
    {
        public EstatesPageViewModel() : base(typeof(Owner), typeof(EstateType), typeof(Contract))
        {

        }

        public ICommand AddCommand => new Command(async parameter =>
        {
            var dialog = new EstateEditDialogWindow();
            if (dialog.ShowDialog() != true) return;
            var estate = dialog.Item;

            using (var db = new AppDbContext())
            {
                estate.Owner = await db.Owners.FindAsync(estate.Owner.Id);
                estate.Type = await db.EstateTypes.FindAsync(estate.Type.Id);

                await db.Estates.AddAsync(estate);
                await db.SaveChangesAsync();

                Items.Add(estate);
            }
        });

        public ICommand EditCommand => new Command(async parameter =>
        {
            var dialog = new EstateEditDialogWindow(SelectedItem);
            if (dialog.ShowDialog() != true) return;
            var estate = dialog.Item;

            using (var db = new AppDbContext())
            {
                var dbOwner = await db.Estates.FindAsync(SelectedItem.Id);

                await dbOwner.CopyPropertiesAsync(estate, db);
                await db.SaveChangesAsync();

                await SelectedItem.CopyPropertiesAsync(dbOwner, db);
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

                var mbox = MessageBox.Show($"Удалить имущество №{SelectedItem.Id}?{deletionWarning}", "Удаление имущества",
                    MessageBoxButton.OKCancel, hasDependentContracts ? MessageBoxImage.Warning : MessageBoxImage.Question);
                if (mbox != MessageBoxResult.OK) return;

                if (hasDependentContracts)
                {
                    db.Contracts.RemoveRange(dependentContracts);
                }

                var dbEstate = await db.Estates.FindAsync(SelectedItem.Id);
                db.Estates.Remove(dbEstate);
                await db.SaveChangesAsync();
                Items.Remove(SelectedItem);
            }
        }, parameter => SelectedItem != null);
    }
}
