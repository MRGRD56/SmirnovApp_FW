using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SmirnovApp.Model.DbModels
{
    /// <summary>
    /// Пользователь системы.
    /// </summary>
    [Table("Users")]
    public class User : InitialData
    {
        /// <summary>
        /// Логин.
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// Пароль.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Направление: юридическое или риэлторское. Определяет, с какими услугами работает пользователь.
        /// </summary>
        public ServiceCategory ServicesDirection { get; set; }

        public string RoleName
        {
            get
            {
                switch (ServicesDirection)
                {
                    case ServiceCategory.Legal:
                        return "Юрист";
                    case ServiceCategory.Realtor:
                        return "Риэлтор";
                    default:
                        throw new ArgumentOutOfRangeException(nameof(ServicesDirection));
                }
            }
        }

        /// <summary>
        /// Проверяет, имеет ли пользователь указанные данные для входа.
        /// </summary>
        /// <param name="login">Проверяемый логин.</param>
        /// <param name="password">Проверяемый пароль.</param>
        /// <returns></returns>
        public bool HasCredentials(string login, string password) => string.Equals(Login, login) && string.Equals(Password, password);
		
		public override object Clone()
        {
            return new User
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
				Login = Login,
				Password = Password,
				ServicesDirection = ServicesDirection
            };
        }
    }
}
