using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;
using SmirnovApp.Common;
using SmirnovApp.Model;

namespace SmirnovApp.ViewModels
{
    /// <summary>
    /// Представляет базовую ViewModel.
    /// </summary>
    public class BaseViewModel : NotifyPropertyChanged
    {
        /// <summary>
        /// Команда навигации к новому экземпляру типа, передаваемого в качестве параметра.
        /// </summary>
        public Command NavigateCommand => new Command(parameter =>
        {
            var type = (Type)parameter;
            Navigation.Navigate(type);
        });

        /// <summary>
        /// Команда перехода назад.
        /// </summary>
        public Command GoBackCommand => new Command(_ =>
        {
            Navigation.GoBack();
        }, _ => Navigation.CanGoBack);

        /// <summary>
        /// Определяет возможность перехода назад.
        /// </summary>
        public bool CanGoBack => Navigation.CanGoBack;

        /// <summary>
        /// Команда открытия нового окна типа как диалогового. Тип окна задаётся параметром.
        /// </summary>
        public Command ShowDialogWindowCommand => new Command(parameter =>
        {
            var type = (Type)parameter;
            var window = (Window)Activator.CreateInstance(type);
            window?.ShowDialog();
        });
    }
}
