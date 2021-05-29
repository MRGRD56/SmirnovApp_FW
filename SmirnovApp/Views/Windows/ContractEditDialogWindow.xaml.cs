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
    /// Interaction logic for ContractEditDialogWindow.xaml
    /// </summary>
    public partial class ContractEditDialogWindow : Window
    {
        private ContractEditDialogWindowViewModel ViewModel => (ContractEditDialogWindowViewModel)DataContext;

        public Contract Contract => ViewModel.Contract;

        public ContractEditDialogWindow()
        {
            InitializeComponent();
            DataContext = new ContractEditDialogWindowViewModel();
        }

        public ContractEditDialogWindow(Contract contract)
        {
            InitializeComponent();
            DataContext = new ContractEditDialogWindowViewModel(contract);
        }
    }
}
