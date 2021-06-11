using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SmirnovApp.Model.DbModels
{
    /// <summary>
    /// Владелец.
    /// </summary>
    [Table("Owners")]
    public class Owner : Person, ICopyable<Owner>
    {
        private DateTime _applicationDate = DateTime.Now;
        private string _phone;

        /// <summary>
        /// Номер телефона.
        /// </summary>
        public string Phone
        {
            get => _phone;
            set
            {
                if (value == _phone) return;
                _phone = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Дата обращения.
        /// </summary>
        public DateTime ApplicationDate
        {
            get => _applicationDate;
            set
            {
                if (value.Equals(_applicationDate)) return;
                _applicationDate = value;
                OnPropertyChanged();
            }
        }

        public List<Estate> Estates { get; set; } = new List<Estate>();

        public override object Clone()
        {
            return new Owner
            {
                Id = Id,
                LastName = LastName,
                FirstName = FirstName,
                Patronymic = Patronymic,
                BirthDate = BirthDate,
                PassportSeries = PassportSeries,
                PassportNumber = PassportNumber,
                PassportIssuedBy = PassportIssuedBy,
                PassportIssueDate = PassportIssueDate,
                LivingAddress = LivingAddress,
                RegistrationAddress = RegistrationAddress,
                ApplicationDate = ApplicationDate,
                Phone = Phone
            };
        }

        public void CopyPropertiesFrom(Owner source)
        {
            LastName = source.LastName;
            FirstName = source.FirstName;
            Patronymic = source.Patronymic;
            BirthDate = source.BirthDate;
            PassportSeries = source.PassportSeries;
            PassportNumber = source.PassportNumber;
            PassportIssuedBy = source.PassportIssuedBy;
            PassportIssueDate = source.PassportIssueDate;
            LivingAddress = source.LivingAddress;
            RegistrationAddress = source.RegistrationAddress;
            ApplicationDate = source.ApplicationDate;
            Phone = source.Phone;
        }
    }
}
