using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using SmirnovApp.Common;
using SmirnovApp.Model.DbModels;
using SmirnovApp.Views.Windows;

namespace SmirnovApp.ViewModels.WindowsViewModels
{
    public sealed class ClientEditDialogWindowViewModel : BaseEditDialogViewModel<Client>
    {
        public Type ClientType { get; }

        public string LegalEntityName { get; set; }

        public bool IsIndividualClient => Item is IndividualClient;
        public bool IsLegalEntityClient => Item is LegalEntityClient;

        public override string WindowTitle => (IsEdit ? "Редактирование" : "Добавление") + 
                                     (IsIndividualClient ? " физического" : " юридического") + " лица";

        public ClientEditDialogWindowViewModel(Type newClientType)
        {
            ClientType = newClientType;
            ValidateClientType(ClientType);
            Item = (Client)Activator.CreateInstance(newClientType);
            IsEdit = false;
        }
        
        public ClientEditDialogWindowViewModel(Client clientToEdit)
        {
            ClientType = clientToEdit.GetType();
            ValidateClientType(ClientType);
            Item = (Client)clientToEdit.Clone();
            IsEdit = true;

            if (clientToEdit is LegalEntityClient legalEntityClient)
            {
                LegalEntityName = legalEntityClient.Name;
            }
        }

        private void ValidateClientType(Type clientType)
        {
            if (clientType != typeof(LegalEntityClient) && clientType != typeof(IndividualClient))
            {
                throw new ArgumentException("ClientType must be LegalEntityClient or IndividualClient",
                    nameof(clientType));
            }
        }

        /// <summary>
        /// Проверяет объект клиента на валидность.
        /// </summary>
        /// <param name="errors">Ошибки валидации клиента.</param>
        /// <returns>Валиден ли объект.</returns>
        private bool ValidateClient(out List<string> errors)
        {
            errors = new List<string>();

            if (Item is LegalEntityClient legalEntityClient && string.IsNullOrWhiteSpace(legalEntityClient.Name))
            {
                errors.Add("Название юрид. лица");
            }
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
            if (Item.ApplicationDate == default)
            {
                errors.Add("Адрес обращения");
            }

            return !errors.Any();
        }

        private bool Validate()
        {
            if (ValidateClient(out var errors)) return true;
            
            var text = "Не все поля заполнены:";
            text += string.Join("", errors.Select(error => $"\n• {error}"));
            MessageBox.Show(text, "Заполните все поля", MessageBoxButton.OK, MessageBoxImage.Error);
            return false;
        }

        protected override bool OkBeforeClose(Window window)
        {
            if (Item is LegalEntityClient legalEntityClient)
            {
                legalEntityClient.Name = LegalEntityName;
            }

            return Validate();
        }
    }
}
