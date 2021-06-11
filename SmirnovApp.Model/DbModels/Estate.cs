using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SmirnovApp.Model.DbModels
{
    /// <summary>
    /// Имущество.
    /// </summary>
    [Table("Estates")]
    public class Estate : NotifyPropertyChanged, ICloneable
    {
        private string _name;
        private decimal _cost;
        private int _area;
        private int _floorsCount;
        private int _floor;
        private string _address;
        private EstateType _type;
        private Owner _owner;
        public int Id { get; set; }

        /// <summary>
        /// Название.
        /// </summary>
        public string Name
        {
            get => _name;
            set
            {
                if (value == _name) return;
                _name = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Стоимость.
        /// </summary>
        public decimal Cost
        {
            get => _cost;
            set
            {
                if (value == _cost) return;
                _cost = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Площадь в м^2.
        /// </summary>
        public int Area
        {
            get => _area;
            set
            {
                if (value == _area) return;
                _area = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Количество этажей.
        /// </summary>
        public int FloorsCount
        {
            get => _floorsCount;
            set
            {
                if (value == _floorsCount) return;
                _floorsCount = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Этаж.
        /// </summary>
        public int Floor
        {
            get => _floor;
            set
            {
                if (value == _floor) return;
                _floor = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Адрес, включая квартиру, если дом многоквартирный.
        /// </summary>
        public string Address
        {
            get => _address;
            set
            {
                if (value == _address) return;
                _address = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Вид недвижимости.
        /// </summary>
        public virtual EstateType Type
        {
            get => _type;
            set
            {
                if (Equals(value, _type)) return;
                _type = value;
                OnPropertyChanged();
            }
        }

        public int TypeId { get; set; }

        /// <summary>
        /// Владелец.
        /// </summary>
        public virtual Owner Owner
        {
            get => _owner;
            set
            {
                if (Equals(value, _owner)) return;
                _owner = value;
                OnPropertyChanged();
            }
        }

        public int OwnerId { get; set; }

        public List<Contract> Contracts { get; set; } = new List<Contract>();

        public object Clone()
        {
            return new Estate
            {
                Id = Id,
                Name = Name,
                Cost = Cost,
                Area = Area,
                FloorsCount = FloorsCount,
                Floor = Floor,
                Address = Address,
                Type = Type,
                TypeId = TypeId,
                Owner = Owner,
                OwnerId = OwnerId
            };
        }
    }
}
