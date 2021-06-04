using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using SmirnovApp.Model.DbModels;
using SmirnovApp.ViewModels.WindowsViewModels;

namespace SmirnovApp.Views.Windows
{
    /// <summary>
    /// Interaction logic for ClientEditDialogWindow.xaml
    /// </summary>
    public partial class ClientEditDialogWindow : Window
    {
        private ClientEditDialogWindowViewModel ViewModel => (ClientEditDialogWindowViewModel)DataContext;

        public Client Client => ViewModel.Client;

        /// <summary>
        /// Создаёт новое окно для создания клиента указанного типа.
        /// </summary>
        /// <param name="clientType"></param>
        public ClientEditDialogWindow(Type clientType)
        {
            InitializeComponent();
            DataContext = new ClientEditDialogWindowViewModel(clientType);
        }

        /// <summary>
        /// Создаёт новое окно для редактирования клиента.
        /// </summary>
        /// <param name="clientToEdit"></param>
        public ClientEditDialogWindow(Client clientToEdit)
        {
            InitializeComponent();
            DataContext = new ClientEditDialogWindowViewModel(clientToEdit);
        }
    }
}
