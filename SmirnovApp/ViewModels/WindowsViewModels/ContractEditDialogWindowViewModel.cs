using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using SmirnovApp.Common;
using SmirnovApp.Context;
using SmirnovApp.Model.DbModels;
using SmirnovApp.Views.Windows;

namespace SmirnovApp.ViewModels.WindowsViewModels
{
    public class ContractEditDialogWindowViewModel : BaseViewModel
    {
        public Contract Contract { get; private set; }

        public bool IsEdit { get; }

        public string WindowTitle => IsEdit ? "Редактирование договора" : "Добавление договора";

        public bool Result { get; private set; }

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
                Services = db.Services.ToList();
            
                if (contract == null)
                {
                    Contract = new Contract();
                }
                else
                {
                    Contract = (Contract) contract?.Clone();
                    Contract.Client = Clients.Single(x => x.Id == Contract.Client.Id);
                    Contract.Employee = Employees.Single(x => x.Id == Contract.Employee.Id);
                    Contract.Estate = Estates.Single(x => x.Id == Contract.Estate.Id);
                    Contract.Service = Services.Single(x => x.Id == Contract.Service.Id);
                }
            }
        }

        public Command OkCommand => new Command(o =>
        {
            var window = (ContractEditDialogWindow) o;

            var errors = new List<string>();
            if (string.IsNullOrWhiteSpace(Contract.Name))
            {
                errors.Add("Название договора");
            }
            if (Contract.Amount == default)
            {
                errors.Add("Сумма");
            }
            if (Contract.Date == default)
            {
                errors.Add("Дата договора");
            }
            if (Contract.Client == null)
            {
                errors.Add("Клиент");
            }
            if (Contract.Employee == null)
            {
                errors.Add("Сотрудник");
            }
            if (Contract.Estate == null)
            {
                errors.Add("Имущество");
            }
            if (Contract.Service == null)
            {
                errors.Add("Услуга");
            }

            if (errors.Any())
            {
                var errorsText = $"Следующие поля не заполнены:\n{string.Join(";\n", errors.Select(x => $"• {x}"))}.";
                MessageBox.Show(errorsText, "Не все поля заполнены", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            window.DialogResult = true;
            window.Close();
        });

        public Command CancelCommand => new Command(o =>
        {
            var window = (ContractEditDialogWindow) o;

            window.DialogResult = false;
            window.Close();
        });
    }
}
