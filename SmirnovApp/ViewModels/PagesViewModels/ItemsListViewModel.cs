using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SmirnovApp.Context;
using SmirnovApp.Extensions;

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
            
                var items = db.Set<T>();
                await items.ForEachAsync(x =>
                {
                    _syncContext.Send(_ => Items.Add(x), null);
                });
            }
        }
    }
}
