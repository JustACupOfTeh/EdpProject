using Microsoft.EntityFrameworkCore;
using eCO2Tracker.Models;

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

        public DbSet<Department> Departments { get; set; }

        public DbSet<Employee> Employees { get; set; }
    }
}
