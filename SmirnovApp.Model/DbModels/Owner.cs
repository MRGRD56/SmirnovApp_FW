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
        /// <summary>
        /// Номер телефона.
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Дата обращения.
        /// </summary>
        public DateTime ApplicationDate { get; set; } = DateTime.Now;

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
