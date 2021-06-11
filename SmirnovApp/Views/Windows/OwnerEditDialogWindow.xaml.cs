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
    /// Interaction logic for OwnerEditDialogWindow.xaml
    /// </summary>
    public partial class OwnerEditDialogWindow : Window, IBaseEditDialogWindow<Owner, OwnerEditDialogWindowViewModel>
    {
        public OwnerEditDialogWindow()
        {
            InitializeComponent();
            DataContext = new OwnerEditDialogWindowViewModel();
        }

        public OwnerEditDialogWindow(Owner ownerToEdit)
        {
            InitializeComponent();
            DataContext = new OwnerEditDialogWindowViewModel(ownerToEdit);
        }

        public OwnerEditDialogWindowViewModel ViewModel => (OwnerEditDialogWindowViewModel) DataContext;
        public Owner Item => ViewModel.Item;
    }
}
