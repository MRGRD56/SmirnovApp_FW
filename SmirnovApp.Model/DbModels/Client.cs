using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SmirnovApp.Model.DbModels
{
    /// <summary>
    /// Клиент.
    /// </summary>
    public abstract class Client : Person, ICloneable
    {
        public DateTime ApplicationDate { get; set; }

        public abstract object Clone();
    }
}
