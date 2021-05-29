using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SmirnovApp.Common
{
    internal static class Navigation
    {
        public static Frame Frame => App.MainWindow.Frame;

        /// <summary>
        /// Осуществляет навигацию к объекту <paramref name="page"/>.
        /// </summary>
        /// <param name="page"></param>
        internal static void Navigate(object page)
        {
            Frame.Navigate(page);
        }

        /// <summary>
        /// Осуществляет навигацию к новому экземпляру типа <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        internal static void Navigate<T>()
        {
            var page = Activator.CreateInstance<T>();
            Navigate(page);
        }

        /// <summary>
        /// Осуществляет навигацию к новому экземпляру типа <paramref name="pageType"/>.
        /// </summary>
        /// <param name="pageType"></param>
        internal static void Navigate(Type pageType)
        {
            var page = Activator.CreateInstance(pageType);
            Navigate(page);
        }

        internal static bool CanGoBack => Frame?.CanGoBack == true;

        internal static void GoBack()
        {
            if (CanGoBack)
            {
                Frame.GoBack();
            }
        }
    }
}
