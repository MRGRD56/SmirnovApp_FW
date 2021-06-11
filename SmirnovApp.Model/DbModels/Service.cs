using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SmirnovApp.Model.DbModels
{
    /// <summary>
    /// Услуга.
    /// </summary>
    [Table("Services")]
    public class Service : NotifyPropertyChanged, ICategoryble, ICloneable, ICopyable<Service>
    {
        private string _name;
        private decimal _cost;

        public int Id { get; set; }

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

        [DataType("money")]
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

        public ServiceCategory ServiceCategory { get; set; }
        public ServiceCategory GetServiceCategory() => ServiceCategory;

        public object Clone()
        {
            return new Service
            {
                Id = Id,
                Name = Name,
                Cost = Cost
            };
        }
        
        public void CopyPropertiesFrom(Service source)
        {
            Name = source.Name;
            Cost = source.Cost;
        }

        public List<Contract> Contracts { get; set; } = new List<Contract>();
    }
}
