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
    /// Interaction logic for ServiceEditDialogWindow.xaml
    /// </summary>
    public partial class ServiceEditDialogWindow : Window, IBaseEditDialogWindow<Service, ServiceEditDialogWindowViewModel>
    {
        public ServiceEditDialogWindow()
        {
            InitializeComponent();
            DataContext = new ServiceEditDialogWindowViewModel();
        }

        public ServiceEditDialogWindow(Service serviceToEdit)
        {
            InitializeComponent();
            DataContext = new ServiceEditDialogWindowViewModel(serviceToEdit);
        }

        public ServiceEditDialogWindowViewModel ViewModel => (ServiceEditDialogWindowViewModel) DataContext;
        public Service Item => ViewModel.Item;
    }
}
