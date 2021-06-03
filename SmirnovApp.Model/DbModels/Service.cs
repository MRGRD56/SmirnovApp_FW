using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SmirnovApp.Model.DbModels
{
    /// <summary>
    /// Услуга.
    /// </summary>
    public class Service : ICategoryble
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [DataType("money")]
        public decimal Cost { get; set; }
        
        public ServiceCategory ServiceCategory { get; set; }
        public ServiceCategory GetServiceCategory() => ServiceCategory;
    }
}
