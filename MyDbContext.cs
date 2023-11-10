using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_DatabaseConnection.Models;

namespace WPF_DatabaseConnection
{
    internal class MyDbContext:DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<User> Users { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Database=(localDb)\\WPF_Examen-advanced;Trusted_Connection=true;TrustServerCertificate=true;MultipleActiveResultSets=true");
        }
    }
}
