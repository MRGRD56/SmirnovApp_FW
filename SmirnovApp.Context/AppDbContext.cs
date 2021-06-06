using Microsoft.EntityFrameworkCore;
using System;
using SmirnovApp.Model.DbModels;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.IO;

namespace SmirnovApp.Context
{
    public class AppDbContext : DbContext
    {
        private static string GetConnectionString()
        {
            const string defaultConnectionString =
                @"Server=localhost\SQLEXPRESS;Database=SmirnovAppFw;Trusted_connection=True;";

            string connectionString;

            try
            {
                connectionString = File.ReadAllText("_connection.txt");

                if (string.IsNullOrWhiteSpace(connectionString))
                {
                    connectionString = defaultConnectionString;
                }
            }
            catch
            {
                connectionString = defaultConnectionString;
            }

            return connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(GetConnectionString());
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            ////modelBuilder.Entity<Person>().ToTable("Persons");
            //modelBuilder.Entity<Owner>().ToTable("Owners");
            //modelBuilder.Entity<Employee>().ToTable("Employees");
            ////modelBuilder.Entity<Client>().ToTable("Clients");
            //modelBuilder.Entity<IndividualClient>().ToTable("IndividualClients");
            //modelBuilder.Entity<LegalEntityClient>().ToTable("LegalEntityClients");

            modelBuilder.Entity<Contract>()
                .HasOne(e => e.Client)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Contract>()
                .HasOne(e => e.Employee)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);
        }

        /// <summary>
        /// Клиенты.
        /// </summary>
        public DbSet<Client> Clients { get; set; }

        /// <summary>
        /// Клиенты-физические лица.
        /// </summary>
        public DbSet<IndividualClient> IndividualClients { get; set; }

        /// <summary>
        /// Клиенты-юридические лица.
        /// </summary>
        public DbSet<LegalEntityClient> LegalEntityClients { get; set; }

        /// <summary>
        /// Договоры.
        /// </summary>
        public DbSet<Contract> Contracts { get; set; }

        /// <summary>
        /// Сотрудники.
        /// </summary>
        public DbSet<Employee> Employees { get; set; }

        /// <summary>
        /// Недвижимость.
        /// </summary>
        public DbSet<Estate> Estates { get; set; }

        /// <summary>
        /// Типы недвижимости.
        /// </summary>
        public DbSet<EstateType> EstateTypes { get; set; }

        /// <summary>
        /// Владельцы.
        /// </summary>
        public DbSet<Owner> Owners { get; set; }

        /// <summary>
        /// Люди.
        /// </summary>
        public DbSet<Person> Persons { get; set; }

        /// <summary>
        /// Должности.
        /// </summary>
        public DbSet<Position> Positions { get; set; }

        /// <summary>
        /// Услуги.
        /// </summary>
        public DbSet<Service> Services { get; set; }

        /// <summary>
        /// Пользователи приложения.
        /// </summary>
        public DbSet<User> Users { get; set; }

        public List<Contract> GetAvailableContracts(User user) =>
            Contracts.Where(x => x.Service.ServiceCategory == user.ServicesDirection).ToList();

        public async Task<List<Contract>> GetAvailableContractsAsync(User user) =>
            await Contracts.Where(x => x.Service.ServiceCategory == user.ServicesDirection).ToListAsync();


        public List<Service> GetAvailableServices(User user) =>
            Services.Where(x => x.ServiceCategory == user.ServicesDirection).ToList();

        public async Task<List<Service>> GetAvailableServicesAsync(User user) =>
            await Services.Where(x => x.ServiceCategory == user.ServicesDirection).ToListAsync();
    }
}
