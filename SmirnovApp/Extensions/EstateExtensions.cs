using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmirnovApp.Context;
using SmirnovApp.Model.DbModels;

namespace SmirnovApp.Extensions
{
    public static class EstateExtensions
    {
        public static async Task CopyPropertiesAsync(this Estate to, Estate from, AppDbContext db)
        {
            to.Name = from.Name;
            to.Cost = from.Cost;
            to.Area = from.Area;
            to.FloorsCount = from.FloorsCount;
            to.Floor = from.Floor;
            to.Address = from.Address;
            to.Type = await db.EstateTypes.FindAsync(from.Type.Id);
            to.Owner = await db.Owners.FindAsync(from.Owner.Id);
        }
    }
}
