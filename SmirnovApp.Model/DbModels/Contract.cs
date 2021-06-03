using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Contracts;
using System.Text;

namespace SmirnovApp.Model.DbModels
{
    /// <summary>
    /// Договор.
    /// </summary>
    public class Contract : NotifyPropertyChanged, ICloneable, ICategoryble
    {
        private string _name;
        private decimal _amount;
        private DateTime _date = DateTime.Now;
        private ContractStatus _status;
        private Client _client;
        private Employee _employee;
        private Service _service;
        private Estate _estate;

        public int Id { get; set; }

        /// <summary>
        /// Название.
        /// </summary>
        public string Name
        {
            get => _name; 
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Сумма.
        /// </summary>
        [DataType("money")]
        public decimal Amount
        {
            get => _amount; 
            set
            {
                _amount = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Дата сделки.
        /// </summary>
        public DateTime Date
        {
            get => _date; 
            set
            {
                _date = value;
                OnPropertyChanged();
            }
        }
        /// <summary>
        /// Статус договора.
        /// </summary>
        public ContractStatus Status
        {
            get => _status; 
            set
            {
                _status = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Клиент.
        /// </summary>
        public virtual Client Client
        {
            get => _client; 
            set
            {
                _client = value;
                OnPropertyChanged();
            }
        }
        public int ClientId { get; set; }

        /// <summary>
        /// Сотрудник.
        /// </summary>
        public virtual Employee Employee
        {
            get => _employee; 
            set
            {
                _employee = value;
                OnPropertyChanged();
            }
        }
        public int EmployeeId { get; set; }

        /// <summary>
        /// Услуга.
        /// </summary>
        public virtual Service Service
        {
            get => _service;
            set
            {
                _service = value;
                OnPropertyChanged();
            }
        }
        public int ServiceId { get; set; }

        /// <summary>
        /// Имущество.
        /// </summary>
        public virtual Estate Estate
        {
            get => _estate;
            set
            {
                _estate = value;
                OnPropertyChanged();
            }
        }
        public int EstateId { get; set; }

        public ServiceCategory GetServiceCategory() => Service.ServiceCategory;

        public object Clone()
        {
            return new Contract
            {
                Id = Id,
                Name = Name,
                Amount = Amount,
                Date = Date,
                Status = Status,
                Client = Client,
                ClientId = ClientId,
                Employee = Employee,
                EmployeeId = EmployeeId,
                Service = Service,
                ServiceId = ServiceId,
                Estate = Estate,
                EstateId = EstateId
            };
        }
    }
}
