using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SmirnovApp.Model.DbModels
{
    /// <summary>
    /// Клиент.
    /// </summary>
    [Table("Clients")]
    public class Client : Person
    {
        /// <summary>
        /// Дата обращения.
        /// </summary>
        public DateTime ApplicationDate { get; set; }
    }
}
