using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SmirnovApp.Model.DbModels
{
    /// <summary>
    /// Сотрудник.
    /// </summary>
    [Table("Employees")]
    public class Employee : Person
    {
        /// <summary>
        /// Зарплата.
        /// </summary>
        [DataType("money")]
        public decimal Salary { get; set; }

        /// <summary>
        /// Номер телефона.
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Должность.
        /// </summary>
        public virtual Position Position { get; set; }
        public int PositionId { get; set; }

        ///// <summary>
        ///// Клиент.
        ///// </summary>
        //public Client Client { get; set; }
        //public int? ClientId;

        ///// <summary>
        ///// Владелец.
        ///// </summary>
        //public Owner Owner { get; set; }
        //public int? OwnerId;

        public override object Clone()
        {
            return new Employee
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
                Phone = Phone,
                Position = Position,
                PositionId = PositionId,
                Salary = Salary
            };
        }
    }
}
