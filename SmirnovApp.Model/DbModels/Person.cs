using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SmirnovApp.Model.DbModels
{
    /// <summary>
    /// Человек.
    /// </summary>
    public abstract class Person : NotifyPropertyChanged
    {
        private string _lastName;
        private string _firstName;
        private string _patronymic;
        private DateTime _birthDate = DateTime.Now;
        private string _passportSeries;
        private string _passportNumber;
        private string _passportIssuedBy;
        private DateTime _passportIssueDate = DateTime.Now;
        private string _livingAddress;
        private string _registrationAddress;
        public int Id { get; set; }

        /// <summary>
        /// Фамилия.
        /// </summary>
        public string LastName
        {
            get => _lastName;
            set
            {
                if (value == _lastName) return;
                _lastName = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(FullName));
                OnPropertyChanged("Director");
            }
        }

        /// <summary>
        /// Имя.
        /// </summary>
        public string FirstName
        {
            get => _firstName;
            set
            {
                if (value == _firstName) return;
                _firstName = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(FullName));
                OnPropertyChanged("Director");
            }
        }

        /// <summary>
        /// Отчество.
        /// </summary>
        public string Patronymic
        {
            get => _patronymic;
            set
            {
                if (value == _patronymic) return;
                _patronymic = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(FullName));
                OnPropertyChanged("Director");
            }
        }

        /// <summary>
        /// Дата рождения.
        /// </summary>
        [DataType("date")]
        public DateTime BirthDate
        {
            get => _birthDate;
            set
            {
                if (value.Equals(_birthDate)) return;
                _birthDate = value;
                OnPropertyChanged();
                OnPropertyChanged("Director");
            }
        }

        /// <summary>
        /// ФИО.
        /// </summary>
        public virtual string FullName => $"{LastName} {FirstName} {Patronymic}";

        /// <summary>
        /// Серия паспорта.
        /// </summary>
        public string PassportSeries
        {
            get => _passportSeries;
            set
            {
                if (value == _passportSeries) return;
                _passportSeries = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(PassportFullNumber));
                OnPropertyChanged("Director");
            }
        }

        /// <summary>
        /// Номер паспорта.
        /// </summary>
        public string PassportNumber
        {
            get => _passportNumber;
            set
            {
                if (value == _passportNumber) return;
                _passportNumber = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(PassportFullNumber));
                OnPropertyChanged("Director");
            }
        }

        /// <summary>
        /// Серия и номер паспорта.
        /// </summary>
        public string PassportFullNumber => $"{PassportSeries} {PassportNumber}";

        /// <summary>
        /// Кем выдан.
        /// </summary>
        public string PassportIssuedBy
        {
            get => _passportIssuedBy;
            set
            {
                if (value == _passportIssuedBy) return;
                _passportIssuedBy = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(PassportIssued));
                OnPropertyChanged("Director");
            }
        }

        /// <summary>
        /// Когда выдан.
        /// </summary>
        public DateTime PassportIssueDate
        {
            get => _passportIssueDate;
            set
            {
                if (value.Equals(_passportIssueDate)) return;
                _passportIssueDate = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(PassportIssued));
                OnPropertyChanged("Director");
            }
        }

        /// <summary>
        /// Дата выдачи и кем выдан.
        /// </summary>
        public string PassportIssued => $"{PassportIssueDate:dd.MM.yyyy}, {PassportIssuedBy}";

        /// <summary>
        /// Адрес проживания.
        /// </summary>
        public string LivingAddress
        {
            get => _livingAddress;
            set
            {
                if (value == _livingAddress) return;
                _livingAddress = value;
                OnPropertyChanged();
                OnPropertyChanged("Director");
            }
        }

        /// <summary>
        /// Адрес регистрации.
        /// </summary>
        public string RegistrationAddress
        {
            get => _registrationAddress;
            set
            {
                if (value == _registrationAddress) return;
                _registrationAddress = value;
                OnPropertyChanged();
                OnPropertyChanged("Director");
            }
        }
    }
}
