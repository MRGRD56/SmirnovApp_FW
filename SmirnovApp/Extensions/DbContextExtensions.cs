using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SmirnovApp.Extensions
{
    public static class DbContextExtensions
    {
        /// <summary>
        /// Загружает сущности указанных типов.
        /// </summary>
        /// <returns></returns>
        public static void LoadEntities(this DbContext db, params Type[] typesToLoad)
        {
            var dbType = db.GetType();

            //Метод DbContext.Set<T>();
            var setMethod = dbType.GetMethod("Set", new Type[0])
                            ?? throw new Exception("DbContextExtensions.LoadEntities: setMethod is null");

            var entityFrameworkQueryableExtensionsType = typeof(EntityFrameworkQueryableExtensions);

            //Метод static async EntityFrameworkQueryableExtensions.Load<TSource>(this IQueryable<TSource> source);
            var loadMethod = entityFrameworkQueryableExtensionsType.GetMethod("Load")
                                  ?? throw new Exception("DbContextExtensions.LoadEntities: loadMethod is null");

            foreach (var type in typesToLoad)
            {
                //Выполняем для каждого типа.
                //db.Set<T>().Load(); Где T - обобщённый тип type.

                //Метод DbContext.Set<[type]>()
                var setGenericMethod = setMethod.MakeGenericMethod(type);

                //DbSet<[type]>, коллекция, которая будет загружена.
                var itemsToLoad = setGenericMethod.Invoke(db, null)
                                  ?? throw new Exception("DbContextExtensions.LoadEntities: itemsToLoad is null");

                //Метод EntityFrameworkQueryableExtensions.LoadAsync<[type]>(...)
                var loadGenericMethod = loadMethod.MakeGenericMethod(type);

                //Выполняем метод Load.
                loadGenericMethod.Invoke(null, new[] { itemsToLoad });
            }
        }

        /// <summary>
        /// Асинхронно загружает сущности указанных типов.
        /// </summary>
        /// <returns></returns>
        public static async Task LoadEntitiesAsync(this DbContext db, params Type[] typesToLoad)
        {
            var dbType = db.GetType();

            //Метод DbContext.Set<T>();
            var setMethod = dbType.GetMethod("Set", new Type[0])
                            ?? throw new NullReferenceException("DbContextExtensions.LoadEntitiesAsync: setMethod is null");

            var entityFrameworkQueryableExtensionsType = typeof(EntityFrameworkQueryableExtensions);

            //Метод static async EntityFrameworkQueryableExtensions.LoadAsync<TSource>(this IQueryable<TSource> source, CancellationToken cancellationToken = default);
            var loadAsyncMethod = entityFrameworkQueryableExtensionsType.GetMethod("LoadAsync")
                                  ?? throw new NullReferenceException("DbContextExtensions.LoadEntitiesAsync: loadAsyncMethod is null");

            foreach (var type in typesToLoad)
            {
                //Выполняем для каждого типа.
                //await db.Set<T>().LoadAsync(); Где T - обобщённый тип type.

                //Метод DbContext.Set<[type]>()
                var setGenericMethod = setMethod.MakeGenericMethod(type);

                //DbSet<[type]>, коллекция, которая будет загружена.
                var itemsToLoad = setGenericMethod.Invoke(db, null)
                                  ?? throw new NullReferenceException("DbContextExtensions.LoadEntitiesAsync: itemsToLoad is null");

                //Метод EntityFrameworkQueryableExtensions.LoadAsync<[type]>(...)
                var loadAsyncGenericMethod = loadAsyncMethod.MakeGenericMethod(type);

                //Выполняем метод LoadAsync и получаем Task.
                var loadAsyncMethodTask = (Task)loadAsyncGenericMethod.Invoke(null, new[] { itemsToLoad, default(CancellationToken) })
                                          ?? throw new NullReferenceException("DbContextExtensions.LoadEntitiesAsync: loadAsyncMethod is null");

                //Ожидаем полученный из LoadAsync() Task.
                await loadAsyncMethodTask.ConfigureAwait(false);
            }
        }
    }
}
