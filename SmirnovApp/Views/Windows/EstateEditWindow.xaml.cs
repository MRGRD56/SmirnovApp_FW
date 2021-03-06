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
    /// Interaction logic for EstateEditDialogWindow.xaml
    /// </summary>
    public partial class EstateEditDialogWindow : Window, IBaseEditDialogWindow<Estate, EstateEditDialogWindowViewModel>
    {
        public EstateEditDialogWindow()
        {
            InitializeComponent();
            DataContext = new EstateEditDialogWindowViewModel();
        }

        public EstateEditDialogWindow(Estate estate)
        {
            InitializeComponent();
            DataContext = new EstateEditDialogWindowViewModel(estate);
        }

        public EstateEditDialogWindowViewModel ViewModel => (EstateEditDialogWindowViewModel) DataContext;
        public Estate Item => ViewModel.Item;
    }
}
