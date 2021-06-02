using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SmirnovApp.Model.DbModels
{
    /// <summary>
    /// Клиент.
    /// </summary>
    public abstract class Client : Person
    {
        public DateTime ApplicationDate { get; set; }
    }
}
