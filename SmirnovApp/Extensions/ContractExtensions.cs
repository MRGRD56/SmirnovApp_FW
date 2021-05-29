using Microsoft.EntityFrameworkCore;
using SmirnovApp.Context;
using SmirnovApp.Model.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmirnovApp.Extensions
{
    public static class ContractExtensions
    {
        /// <summary>
        /// Копирует значения свойств из <paramref name="from"/> в <paramref name="to"/>.
        /// </summary>
        /// <param name="to"></param>
        /// <param name="from"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public static async Task CopyPropertiesAsync(this Contract to, Contract from, AppDbContext db)
        {
            to.Amount = from.Amount;
            to.Client = await db.Clients.FindAsync(from.Client.Id);
            to.Date = from.Date;
            to.Employee = await db.Employees.FindAsync(from.Employee.Id);
            to.Estate = (await db.Estates.Include(x => x.Owner).ToListAsync()).SingleOrDefault(x => x.Id == from.Estate.Id);
            to.Name = from.Name;
            to.Service = await db.Services.FindAsync(from.Service.Id);
            to.Status = from.Status;
        }
    }
}
