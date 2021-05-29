using System;
using System.Collections.Generic;
using System.Text;

namespace SmirnovApp.Model.DbModels
{
    /// <summary>
    /// Пользователь системы.
    /// </summary>
    public class User
    {
        public int Id { get; set; }

        /// <summary>
        /// Логин.
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// Пароль.
        /// </summary>
        public string Password { get; set; }

        public bool HasCredentials(string login, string password) => string.Equals(Login, login) && string.Equals(Password, password);
    }
}
