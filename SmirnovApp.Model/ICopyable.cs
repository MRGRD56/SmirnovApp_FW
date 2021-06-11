using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmirnovApp.Model
{
    /// <summary>
    /// Поддерживает копирование значений свойств из другого объекта типа <typeparamref name="TSource"/>.
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    public interface ICopyable<in TSource>
    {
        /// <summary>
        /// Копирует значения свойств из <paramref name="source"/>.
        /// </summary>
        /// <param name="source"></param>
        void CopyPropertiesFrom(TSource source);
    }
}
