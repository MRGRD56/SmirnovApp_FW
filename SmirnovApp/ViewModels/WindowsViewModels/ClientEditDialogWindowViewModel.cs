using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmirnovApp.Common;
using SmirnovApp.Model.DbModels;

namespace SmirnovApp.ViewModels.WindowsViewModels
{
    public class ClientEditDialogWindowViewModel : BaseViewModel
    {
        public Type ClientType { get; }

        public Client Client { get; }

        public bool IsEdit { get; }

        public string WindowTitle => (IsEdit ? "Редактирование" : "Добавление") + " клиента";

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
        }

        private void ValidateClientType(Type clientType)
        {
            if (clientType != typeof(LegalEntityClient) && clientType != typeof(IndividualClient))
            {
                throw new ArgumentException("ClientType must be LegalEntityClient or IndividualClient",
                    nameof(clientType));
            }
        }

        public Command OkCommand => new Command(_ =>
        {

        });

        public Command CancelCommand => new Command(_ =>
        {

        });
    }
}
