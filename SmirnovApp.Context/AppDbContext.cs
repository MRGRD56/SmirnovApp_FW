using Microsoft.EntityFrameworkCore;
using System;
using SmirnovApp.Model.DbModels;

namespace SmirnovApp.Context
{
    public class AppDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(@"Server=localhost\SQLEXPRESS;Database=SmirnovAppFw;Trusted_connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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
        public virtual DbSet<Client> Clients { get; set; }

        /// <summary>
        /// Договоры.
        /// </summary>
        public virtual DbSet<Contract> Contracts { get; set; }

        /// <summary>
        /// Сотрудники.
        /// </summary>
        public virtual DbSet<Employee> Employees { get; set; }

        /// <summary>
        /// Недвижимость.
        /// </summary>
        public virtual DbSet<Estate> Estates { get; set; }

        /// <summary>
        /// Типы недвижимости.
        /// </summary>
        public virtual DbSet<EstateType> EstateTypes { get; set; }

        /// <summary>
        /// Владельцы.
        /// </summary>
        public virtual DbSet<Owner> Owners { get; set; }

        /// <summary>
        /// Люди.
        /// </summary>
        public virtual DbSet<Person> Persons { get; set; }

        /// <summary>
        /// Должности.
        /// </summary>
        public virtual DbSet<Position> Positions { get; set; }

        /// <summary>
        /// Услуги.
        /// </summary>
        public virtual DbSet<Service> Services { get; set; }

        /// <summary>
        /// Пользователи приложения.
        /// </summary>
        public virtual DbSet<User> Users { get; set; }
    }
}
