using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using SmirnovApp.Context;
using SmirnovApp.Extensions;
using SmirnovApp.Model.DbModels;
using SmirnovApp.ViewModels.PagesViewModels;

namespace SmirnovApp.ViewModels.WindowsViewModels
{
    public class EstateEditDialogWindowViewModel : BaseEditDialogViewModel<Estate>
    {
        public List<EstateType> EstateTypes { get; private set; }
        public List<Owner> Owners { get; private set; }

        public EstateEditDialogWindowViewModel()
        {
            Initialize();
            Item = new Estate();
            IsEdit = false;
        }

        public EstateEditDialogWindowViewModel(Estate estate)
        {
            Initialize();
            Item = (Estate) estate.Clone();
            IsEdit = true;

            Item.Type = EstateTypes.FirstOrDefault(type => Item.Type?.Id == type.Id);
            Item.Owner = Owners.FirstOrDefault(owner => Item.Owner?.Id == owner.Id);
        }

        private void Initialize()
        {
            using (var db = new AppDbContext())
            {
                EstateTypes = db.EstateTypes.ToList();
                Owners = db.Owners.ToList();
            }
        }

        public override string WindowTitle => (IsEdit ? "Редактирование" : "Добавление") + " имущества";

        private bool Validate()
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(Item.Name))
            {
                errors.Add("Название");
            }
            if (Item.Cost == default)
            {
                errors.Add("Стоимость");
            }
            if (Item.Area == default)
            {
                errors.Add("Площадь");
            }
            if (Item.FloorsCount == default)
            {
                errors.Add("Этажность");
            }
            if (Item.Floor == default)
            {
                errors.Add("Этаж");
            }
            if (string.IsNullOrWhiteSpace(Item.Address))
            {
                errors.Add("Адрес");
            }
            if (Item.Type == null)
            {
                errors.Add("Тип");
            }
            if (Item.Owner == null)
            {
                errors.Add("Владелец");
            }

            if (!errors.Any()) return true;

            var message = Validation.GetValidationErrorText(errors, "Не все поля заполнены: ");
            MessageBox.Show(message, "Заполните все поля", MessageBoxButton.OK, MessageBoxImage.Error);

            return false;
        }

        protected override bool OkBeforeClose(Window window)
        {
            return Validate();
        }
    }
}
