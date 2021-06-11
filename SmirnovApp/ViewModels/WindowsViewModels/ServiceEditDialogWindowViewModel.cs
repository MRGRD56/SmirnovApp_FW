using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using SmirnovApp.Common;
using SmirnovApp.Extensions;
using SmirnovApp.Model.DbModels;

namespace SmirnovApp.ViewModels.WindowsViewModels
{
    public sealed class ServiceEditDialogWindowViewModel : BaseEditDialogViewModel<Service>
    {
        public ServiceEditDialogWindowViewModel()
        {
            Item = new Service
            {
                ServiceCategory = Account.CurrentUser.ServicesDirection
            };

            IsEdit = false;
        }

        public ServiceEditDialogWindowViewModel(Service service)
        {
            Item = (Service) service.Clone();
            IsEdit = true;
        }

        public override string WindowTitle => (IsEdit ? "Редактирование" : "Добавление") + " услуги";
        
        protected override bool OkBeforeClose(Window window)
        {
            var errors = new List<string>();
            if (string.IsNullOrWhiteSpace(Item.Name))
            {
                errors.Add("Укажите название");
            }
            if (Item.Cost == default)
            {
                errors.Add("Укажите стоимость");
            }

            if (!errors.Any()) return true;

            var message = Validation.GetValidationErrorText(errors, "Не все поля заполнены");
            MessageBox.Show(message, "Заполните все поля", MessageBoxButton.OK, MessageBoxImage.Error);

            return false;
        }
    }
}
