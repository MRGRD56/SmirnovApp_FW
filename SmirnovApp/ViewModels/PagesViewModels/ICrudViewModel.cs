using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SmirnovApp.ViewModels.PagesViewModels
{
    /// <summary>
    /// Представляет ViewModel, реализующую функции добавления, редактирования и удаления.
    /// </summary>
    public interface ICrudViewModel
    {
        /// <summary>
        /// Команда добавления.
        /// </summary>
        ICommand AddCommand { get; }

        /// <summary>
        /// Команда редактирования.
        /// </summary>
        ICommand EditCommand { get; }

        /// <summary>
        /// Команда удаления.
        /// </summary>
        ICommand RemoveCommand { get; }
    }
}
