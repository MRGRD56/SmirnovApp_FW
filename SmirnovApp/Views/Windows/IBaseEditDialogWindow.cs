using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using SmirnovApp.ViewModels.WindowsViewModels;

namespace SmirnovApp.Views.Windows
{
    public interface IBaseEditDialogWindow<out TItem, out TViewModel> where TViewModel : BaseEditDialogViewModel<TItem>
    {
        TViewModel ViewModel { get; }

        TItem Item { get; }
    }
}
