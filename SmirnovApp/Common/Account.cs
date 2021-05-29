using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmirnovApp.Model.DbModels;

namespace SmirnovApp.Common
{
    internal static class Account
    {
        internal static User CurrentUser { get; private set; }

        internal static bool IsAuthorized => CurrentUser is not null;

        internal static void Login(User user)
        {
            CurrentUser = user;
            LoggedIn?.Invoke(null, new EventArgs());
        }

        internal static void Logout()
        {
            CurrentUser = null;
            LoggedOut?.Invoke(null, new EventArgs());
        }

        public static event EventHandler LoggedIn;
        public static event EventHandler LoggedOut;
    }
}
