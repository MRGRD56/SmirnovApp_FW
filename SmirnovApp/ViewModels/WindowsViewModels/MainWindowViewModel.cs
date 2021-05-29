using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace SmirnovApp.ViewModels.WindowsViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        private string _windowTitle = "";

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
            if (e.Content is Page page)
            {
                WindowTitle = page.Title;
            }

            OnPropertyChanged(nameof(CanGoBack));
        }
    }
}
