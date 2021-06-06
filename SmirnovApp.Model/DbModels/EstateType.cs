using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SmirnovApp.Model.DbModels
{
    /// <summary>
    /// Вид недвижимости.
    /// </summary>
    [Table("EstateTypes")]
    public class EstateType
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
