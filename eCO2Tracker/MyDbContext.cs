using Microsoft.EntityFrameworkCore;
using eCO2Tracker.Models;
using System.Reflection;

namespace eCO2Tracker
{
    public class MyDbContext : DbContext
    {
        private readonly IConfiguration _configuration;
        
        public MyDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = _configuration.GetConnectionString("MyConnection");
            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<ShopItem>()
                .Property(p => p.ItemType)
                .HasConversion(
                    v => v.ToString(),
                    v => (ItemType)Enum.Parse(typeof(ItemType), v));
        }

        public DbSet<Department> Departments { get; set; }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<ShopItem> ShopItems { get; set; }

        public DbSet<Voucher> Vouchers { get; set; }

    }
}
