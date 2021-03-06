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
using System.Windows.Navigation;
using System.Windows.Shapes;
using SmirnovApp.Common;
using SmirnovApp.ViewModels.WindowsViewModels;
using SmirnovApp.Views.Pages;

namespace SmirnovApp.Views.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainWindowViewModel ViewModel => (MainWindowViewModel) DataContext;

        public MainWindow()
        {
            InitializeComponent();
            Navigation.Navigate<LoginPage>();
        }

        private void OnFrameNavigated(object sender, NavigationEventArgs e) => ViewModel.OnNavigated(sender, e);
    }
}
