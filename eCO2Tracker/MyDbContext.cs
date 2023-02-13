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
            //Define User_ShopItems table
            modelBuilder
                .Entity<User_ShopItems>()
                .HasKey(
                    us => new
                    {
                        us.UserID,
                        us.ItemID
                    });

            //Declare user to be part of User_ShopItem
            modelBuilder
                .Entity<User_ShopItems>()
                .HasOne(u => u.User)
                .WithMany(us => us.User_ShopItems)
                .HasForeignKey(u => u.UserID);

            //Declare shopitem to be part of User_ShopItem
            modelBuilder
                .Entity<User_ShopItems>()
                .HasOne(s => s.ShopItem)
                .WithMany(us => us.User_ShopItems)
                .HasForeignKey(s => s.ItemID);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<User> Users { get; set; }

        public DbSet<ShopItem> ShopItems { get; set; }

        public DbSet<Voucher> Vouchers { get; set; }

        public DbSet<User_ShopItems> UserItems { get; set; }
        public DbSet<Lifestyles> Lifestyle { get; set; }
        public DbSet<TrackingCLASS> TrackingDB { get; set; }

        public DbSet<Activity> Activities { get; set; }
    }
}
