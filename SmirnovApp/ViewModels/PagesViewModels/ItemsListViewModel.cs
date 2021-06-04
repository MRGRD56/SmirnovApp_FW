using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using SmirnovApp.Common;
using SmirnovApp.Context;
using SmirnovApp.Extensions;
using SmirnovApp.Model.DbModels;

namespace SmirnovApp.ViewModels.PagesViewModels
{
    public class ItemsListViewModel<T> : BaseViewModel where T : class
    {
        private readonly Type[] _typesToLoad;
        public ObservableCollection<T> Items { get; set; }

        private readonly SynchronizationContext _syncContext = SynchronizationContext.Current;

        public ItemsListViewModel(params Type[] typesToLoad)
        {
            _typesToLoad = typesToLoad;
            Items = new ObservableCollection<T>();
            LoadItems();
        }

        /// <summary>
        /// Загружает коллекцию элементов из базы данных.
        /// </summary>
        private async void LoadItems()
        {
            using (var db = new AppDbContext())
            {
                //Если есть коллекции, которые нужно загрузить, загружаем.
                if (_typesToLoad?.Any() == true)
                {
                    await db.LoadEntitiesAsync(_typesToLoad);
                }

                var items = await db.Set<T>().ToListAsync();
                items = items.Where(item =>
                {
                    if (item is ICategoryble categoryble && Account.IsAuthorized)
                    {
                        return categoryble.GetServiceCategory() == Account.CurrentUser.ServicesDirection;
                    }

                    return true;
                }).ToList();

                items.ForEach(x =>
                {
                    _syncContext.Send(_ => Items.Add(x), null);
                });

                ItemsLoaded?.Invoke(this, new EventArgs());
            }
        }

        public event EventHandler ItemsLoaded;
    }
}
