using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using Microsoft.EntityFrameworkCore;
using SmirnovApp.Common;
using SmirnovApp.Context;
using SmirnovApp.Model.DbModels;
using SmirnovApp.Views.Pages;

namespace SmirnovApp.ViewModels.PagesViewModels
{
    public class LoginPageViewModel : BaseViewModel
    {
        private bool _isLoginProgress = false;
        private readonly SynchronizationContext _syncContext;
        public string Login { get; set; } = "";

        private bool IsLoginProgress
        {
            get => _isLoginProgress;
            set
            {
                if (value == _isLoginProgress) return;
                _isLoginProgress = value;
                OnPropertyChanged(nameof(LoginButtonText));
            }
        }

        public string LoginButtonText => IsLoginProgress ? "ВХОД..." : "ВОЙТИ";

        public Command LoginCommand => new Command(async o =>
        {
            IsLoginProgress = true;

            var passwordBox = (PasswordBox)o;

            var login = Login.Trim();
            var password = passwordBox.Password;

            if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password))
            {
                IsLoginProgress = false;
                MBox.ShowOk("Введите логин и пароль", "Вход");
                return;
            }

            await LoginAsync(login, password);
        });

        private async Task LoginAsync(string login, string password)
        {
            using (var db = new AppDbContext())
            {
                var user = await Task.Run(async () =>
                {
                    var users = await db.Users.ToListAsync();
                    return users.SingleOrDefault(x => x.HasCredentials(login, password));
                });

                if (user == null)
                {
                    IsLoginProgress = false;
                    MBox.ShowError("Неверный логин или пароль!");
                    return;
                }

                Account.Login(user);
                Navigation.Navigate<MainMenuPage>();

                IsLoginProgress = false;
            }
        }

        public LoginPageViewModel()
        {
            _syncContext = SynchronizationContext.Current;
        }
    }
}
