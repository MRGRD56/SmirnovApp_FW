using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore;
using SmirnovApp.Common;
using SmirnovApp.Context;
using SmirnovApp.Model.DbModels;
using SmirnovApp.Views.Windows;

namespace SmirnovApp.ViewModels.WindowsViewModels
{
    public sealed class ContractEditDialogWindowViewModel : BaseEditDialogViewModel<Contract>
    {
        public override string WindowTitle => IsEdit ? "Редактирование договора" : "Добавление договора";

        public List<ContractStatus> Statuses { get; set; }
        public List<Client> Clients { get; set; }
        public List<Employee> Employees { get; set; }
        public List<Estate> Estates { get; set; }
        public List<Service> Services { get; set; }

        public ContractEditDialogWindowViewModel()
        {
            Initialize(null);
            IsEdit = false;
        }

        public ContractEditDialogWindowViewModel(Contract contract)
        {
            Initialize(contract);
            IsEdit = true;
        }

        private void Initialize(Contract contract)
        {
            Statuses = Enum.GetValues(typeof(ContractStatus)).Cast<ContractStatus>().ToList();

            using (var db = new AppDbContext())
            {
                db.Owners.Load();

                Clients = db.Clients.ToList();
                Employees = db.Employees.ToList();
                Estates = db.Estates.ToList();
                Services = db.GetAvailableServices(Account.CurrentUser);
            
                if (contract == null)
                {
                    Item = new Contract();
                }
                else
                {
                    Item = (Contract) contract?.Clone();
                    Item.Client = Clients.Single(x => x.Id == Item.Client.Id);
                    Item.Employee = Employees.Single(x => x.Id == Item.Employee.Id);
                    Item.Estate = Estates.Single(x => x.Id == Item.Estate.Id);
                    Item.Service = Services.Single(x => x.Id == Item.Service.Id);
                }
            }
        }

        protected override bool OkBeforeClose(Window window)
        {
            var errors = new List<string>();
            if (string.IsNullOrWhiteSpace(Item.Name))
            {
                errors.Add("Название договора");
            }
            if (Item.Amount == default)
            {
                errors.Add("Сумма");
            }
            if (Item.Date == default)
            {
                errors.Add("Дата договора");
            }
            if (Item.Client == null)
            {
                errors.Add("Клиент");
            }
            if (Item.Employee == null)
            {
                errors.Add("Сотрудник");
            }
            if (Item.Estate == null)
            {
                errors.Add("Имущество");
            }
            if (Item.Service == null)
            {
                errors.Add("Услуга");
            }

            if (errors.Any())
            {
                var errorsText = $"Следующие поля не заполнены:\n{string.Join(";\n", errors.Select(x => $"• {x}"))}.";
                MessageBox.Show(errorsText, "Не все поля заполнены", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;
        }
    }
}
