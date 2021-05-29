using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SmirnovApp.Common
{
    public static class MBox
    {
        public static MessageBoxResult ShowError(string text, string caption = "Ошибка", MessageBoxButton buttons = MessageBoxButton.OK)
        {
            return MessageBox.Show(text, caption, buttons, MessageBoxImage.Error);
        }

        public static MessageBoxResult ShowOkCancel(string text, string caption = "Подтвердите действие", MessageBoxImage image = MessageBoxImage.Question)
        {
            return MessageBox.Show(text, caption, MessageBoxButton.OKCancel, image);
        }

        public static MessageBoxResult ShowOk(string text, string caption = "Подтвердите действие",
            MessageBoxImage image = MessageBoxImage.Information)
        {
            return MessageBox.Show(text, caption, MessageBoxButton.OK, image);
        }
    }
}
