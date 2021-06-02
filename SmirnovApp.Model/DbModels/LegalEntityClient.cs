using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SmirnovApp.Model.DbModels
{
    /// <summary>
    /// Клиент-юридическое лицо.
    /// </summary>
    public class LegalEntityClient : Client
    {
        #region СКРЫТИЕ ЧЛЕНОВ РОДИТЕЛЬСКОГО КЛАССА Person

        private new string FirstName
        {
            get => base.FirstName;
            set => base.FirstName = value;
        }

        private new string LastName
        {
            get => base.LastName;
            set => base.LastName = value;
        }

        private new string Patronymic
        {
            get => base.Patronymic;
            set => base.Patronymic = value;
        }

        private new DateTime BirthDate
        {
            get => base.BirthDate;
            set => base.BirthDate = value;
        }

        private new string LivingAddress
        {
            get => base.LivingAddress;
            set => base.LivingAddress = value;
        }

        private new DateTime PassportIssueDate
        {
            get => base.PassportIssueDate;
            set => base.PassportIssueDate = value;
        }

        private new string PassportIssuedBy
        {
            get => base.PassportIssuedBy;
            set => base.PassportIssuedBy = value;
        }

        private new string PassportNumber
        {
            get => base.PassportNumber;
            set => base.PassportNumber = value;
        }

        private new string PassportSeries
        {
            get => base.PassportSeries;
            set => base.PassportSeries = value;
        }

        private new string RegistrationAddress
        {
            get => base.RegistrationAddress;
            set => base.RegistrationAddress = value;
        }

        #endregion

        /// <summary>
        /// Название юридического лица.
        /// </summary>
        public string Name { get; set; }

        public override string FullName => Name;

        /// <summary>
        /// Директор юридического лица.
        /// </summary>
        public Person Director => new IndividualClient
        {
            FirstName = FirstName,
            LastName = LastName,
            Patronymic = Patronymic,
            BirthDate = BirthDate,
            ApplicationDate = ApplicationDate,
            LivingAddress = LivingAddress,
            PassportIssueDate = PassportIssueDate,
            PassportIssuedBy = PassportIssuedBy,
            PassportNumber = PassportNumber,
            PassportSeries = PassportSeries,
            RegistrationAddress = RegistrationAddress
        };
    }
}
