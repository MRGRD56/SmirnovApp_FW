using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using SmirnovApp.Common;
using SmirnovApp.Model.DbModels;
using SmirnovApp.Views.Windows;

namespace SmirnovApp.ViewModels.WindowsViewModels
{
    public class ClientEditDialogWindowViewModel : BaseViewModel
    {
        public Type ClientType { get; }

        public Client Client { get; }

        public bool IsEdit { get; }

        public string LegalEntityName { get; set; }

        public bool IsIndividualClient => Client is IndividualClient;
        public bool IsLegalEntityClient => Client is LegalEntityClient;

        public string WindowTitle => (IsEdit ? "Редактирование" : "Добавление") + 
                                     (IsIndividualClient ? " физического" : " юридического") + " лица";

        public ClientEditDialogWindowViewModel(Type newClientType)
        {
            ClientType = newClientType;
            ValidateClientType(ClientType);
            Client = (Client)Activator.CreateInstance(newClientType);
            IsEdit = false;
        }
        
        public ClientEditDialogWindowViewModel(Client clientToEdit)
        {
            ClientType = clientToEdit.GetType();
            ValidateClientType(ClientType);
            Client = (Client)clientToEdit.Clone();
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

            if (Client is LegalEntityClient legalEntityClient && string.IsNullOrWhiteSpace(legalEntityClient.Name))
            {
                errors.Add("Название юрид. лица");
            }
            if (string.IsNullOrWhiteSpace(Client.LastName))
            {
                errors.Add("Фамилия");
            }
            if (string.IsNullOrWhiteSpace(Client.FirstName))
            {
                errors.Add("Имя");
            }
            if (string.IsNullOrWhiteSpace(Client.Patronymic))
            {
                errors.Add("Отчество");
            }
            if (Client.BirthDate == default)
            {
                errors.Add("Дата рождения");
            }
            if (string.IsNullOrWhiteSpace(Client.PassportSeries))
            {
                errors.Add("Серия паспорта");
            }
            if (string.IsNullOrWhiteSpace(Client.PassportNumber))
            {
                errors.Add("Номер паспорта");
            }
            if (string.IsNullOrWhiteSpace(Client.PassportIssuedBy))
            {
                errors.Add("Кем выдан паспорт");
            }
            if (Client.PassportIssueDate == default)
            {
                errors.Add("Дата выдачи паспорта");
            }
            if (string.IsNullOrWhiteSpace(Client.RegistrationAddress))
            {
                errors.Add("Адрес регистрации");
            }
            if (string.IsNullOrWhiteSpace(Client.LivingAddress))
            {
                errors.Add("Адрес проживания");
            }
            if (Client.ApplicationDate == default)
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

        public Command OkCommand => new Command(parameter =>
        {
            var window = (ClientEditDialogWindow) parameter;
            
            if (Client is LegalEntityClient legalEntityClient)
            {
                legalEntityClient.Name = LegalEntityName;
            }

            if (!Validate()) return;

            window.DialogResult = true;
            window.Close();
        });

        public Command CancelCommand => new Command(parameter =>
        {
            var window = (ClientEditDialogWindow)parameter;

            window.DialogResult = false;
            window.Close();
        });
    }
}
