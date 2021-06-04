using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Navigation;
using SmirnovApp.Common;
using SmirnovApp.Model.DbModels;
using SmirnovApp.Views.Pages;

namespace SmirnovApp.ViewModels.WindowsViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        private string _windowTitle = "";
        private User _currentUser;

        public User CurrentUser
        {
            get => _currentUser;
            set
            {
                if (Equals(value, _currentUser)) return;
                _currentUser = value;
                OnPropertyChanged();
            }
        }

        public MainWindowViewModel()
        {
            Account.LoggedIn += AccountOnLoggedIn;
            Account.LoggedOut += AccountOnLoggedOut;
        }

        private void AccountOnLoggedIn(object sender, EventArgs e)
        {
            CurrentUser = Account.CurrentUser;
        }

        private void AccountOnLoggedOut(object sender, EventArgs e)
        {
            CurrentUser = null;
        }

        public string WindowTitle
        {
            get => _windowTitle;
            set
            {
                if (value == _windowTitle) return;
                _windowTitle = value;
                OnPropertyChanged();
            }
        }

        public void OnNavigated(object sender, NavigationEventArgs e)
        {
            var page = e.Content as Page;
            if (page != null)
            {
                WindowTitle = page.Title;
            }

            if (page is LoginPage && Account.IsAuthorized)
            {
                Account.Logout();
            }

            OnPropertyChanged(nameof(CanGoBack));
        }
    }
}
