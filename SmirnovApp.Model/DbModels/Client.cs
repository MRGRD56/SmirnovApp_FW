using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SmirnovApp.Model.DbModels
{
    /// <summary>
    /// Клиент.
    /// </summary>
    public abstract class Client : InitialData, ICloneable, ICopyable<Client>
    {
        private DateTime _applicationDate = DateTime.Now;

        public DateTime ApplicationDate
        {
            get => _applicationDate;
            set
            {
                if (value.Equals(_applicationDate)) return;
                _applicationDate = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Копирует значения свойств из другого объекта Client.
        /// </summary>
        /// <param name="source">Клиент, из которого будут скопированы значения свойств.</param>
        public abstract void CopyPropertiesFrom(Client source);
    }
}