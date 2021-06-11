using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using SmirnovApp.Extensions;
using SmirnovApp.Model.DbModels;

namespace SmirnovApp.ViewModels.WindowsViewModels
{
    public class OwnerEditDialogWindowViewModel : BaseEditDialogViewModel<Owner>
    {
        public OwnerEditDialogWindowViewModel()
        {
            Item = new Owner();
            IsEdit = false;
        }

        public OwnerEditDialogWindowViewModel(Owner owner)
        {
            Item = (Owner) owner.Clone();
            IsEdit = true;
        }

        public override string WindowTitle => (IsEdit ? "Редактирование" : "Добавление") + " владельца";

        /// <summary>
        /// Проверяет объект владельца на валидность.
        /// </summary>
        /// <param name="errors">Ошибки валидации владельца.</param>
        /// <returns>Валиден ли объект.</returns>
        private bool ValidateOwner(out List<string> errors)
        {
            errors = new List<string>();

            if (string.IsNullOrWhiteSpace(Item.LastName))
            {
                errors.Add("Фамилия");
            }
            if (string.IsNullOrWhiteSpace(Item.FirstName))
            {
                errors.Add("Имя");
            }
            if (string.IsNullOrWhiteSpace(Item.Patronymic))
            {
                errors.Add("Отчество");
            }
            if (Item.BirthDate == default)
            {
                errors.Add("Дата рождения");
            }
            if (string.IsNullOrWhiteSpace(Item.PassportSeries))
            {
                errors.Add("Серия паспорта");
            }
            if (string.IsNullOrWhiteSpace(Item.PassportNumber))
            {
                errors.Add("Номер паспорта");
            }
            if (string.IsNullOrWhiteSpace(Item.PassportIssuedBy))
            {
                errors.Add("Кем выдан паспорт");
            }
            if (Item.PassportIssueDate == default)
            {
                errors.Add("Дата выдачи паспорта");
            }
            if (string.IsNullOrWhiteSpace(Item.RegistrationAddress))
            {
                errors.Add("Адрес регистрации");
            }
            if (string.IsNullOrWhiteSpace(Item.LivingAddress))
            {
                errors.Add("Адрес проживания");
            }
            if (string.IsNullOrWhiteSpace(Item.Phone))
            {
                errors.Add("Номер телефона");
            }
            if (Item.ApplicationDate == default)
            {
                errors.Add("Адрес обращения");
            }

            return !errors.Any();
        }

        private bool Validate()
        {
            if (ValidateOwner(out var errors)) return true;

            var text = Validation.GetValidationErrorText(errors, "Не все поля заполнены: ");
            MessageBox.Show(text, "Заполните все поля", MessageBoxButton.OK, MessageBoxImage.Error);
            return false;
        }

        protected override bool OkBeforeClose(Window window)
        {
            return Validate();
        }
    }
}
