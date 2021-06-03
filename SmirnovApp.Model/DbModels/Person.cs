using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SmirnovApp.Model.DbModels
{
    /// <summary>
    /// Человек.
    /// </summary>
    public abstract class Person
    {
        public int Id { get; set; }

        /// <summary>
        /// Фамилия.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Имя.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Отчество.
        /// </summary>
        public string Patronymic { get; set; }

        /// <summary>
        /// Дата рождения.
        /// </summary>
        [DataType("date")]
        public DateTime BirthDate { get; set; }

        /// <summary>
        /// ФИО.
        /// </summary>
        public virtual string FullName => $"{LastName} {FirstName} {Patronymic}";

        /// <summary>
        /// Серия паспорта.
        /// </summary>
        public string PassportSeries { get; set; }

        /// <summary>
        /// Номер паспорта.
        /// </summary>
        public  string PassportNumber { get; set; }

        /// <summary>
        /// Серия и номер паспорта.
        /// </summary>
        public string PassportFullNumber => $"{PassportSeries} {PassportNumber}";

        /// <summary>
        /// Кем выдан.
        /// </summary>
        public string PassportIssuedBy { get; set; }

        /// <summary>
        /// Когда выдан.
        /// </summary>
        public DateTime PassportIssueDate { get; set; }

        /// <summary>
        /// Дата выдачи и кем выдан.
        /// </summary>
        public string PassportIssued => $"{PassportIssueDate:dd.MM.yyyy}, {PassportIssuedBy}";

        /// <summary>
        /// Адрес проживания.
        /// </summary>
        public string LivingAddress { get; set; }

        /// <summary>
        /// Адрес регистрации.
        /// </summary>
        public string RegistrationAddress { get; set; }
    }
}
