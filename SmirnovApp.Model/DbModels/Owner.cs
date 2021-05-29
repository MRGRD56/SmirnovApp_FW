using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SmirnovApp.Model.DbModels
{
    /// <summary>
    /// Владелец.
    /// </summary>
    [Table("Owners")]
    public class Owner : Person
    {
        /// <summary>
        /// Номер телефона.
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Дата обращения.
        /// </summary>
        public DateTime ApplicationDate { get; set; }

        public List<Estate> Estates { get; set; } = new List<Estate>();
    }
}
