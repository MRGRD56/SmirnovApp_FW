using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using SmirnovApp.Common;

namespace SmirnovApp.ViewModels.WindowsViewModels
{
    public abstract class BaseEditDialogViewModel<TItem>
    {
        public abstract string WindowTitle { get; }

        public TItem Item { get; protected set; }

        public bool IsEdit { get; protected set; }

        /// <summary>
        /// Метод, вызываемый при нажатии OK перед закрытием окна.
        /// </summary>
        /// <param name="window"></param>
        /// <returns>Значение, обозначающее, будет ли диалоговое окно успешно закрыто.</returns>
        protected abstract bool OkBeforeClose(Window window);

        public ICommand OkCommand => new Command(parameter =>
        {
            var window = (Window) parameter;

            if (!OkBeforeClose(window)) return;

            window.DialogResult = true;
            window.Close();
        });

        public ICommand CancelCommand => new Command(parameter =>
        {
            var window = (Window) parameter;

            window.DialogResult = false;
            window.Close();
        });
    }
}
