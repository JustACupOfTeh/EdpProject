using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace App.Data
{
    public class UserContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {
          

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("user");
        }
        public void AddUser(User user)
        {
            using (SqlConnection connection = new SqlConnection("Server=localhost; User=root; Pwd=; Port=3306; Database=users"))
            {
                string query = "INSERT INTO [users] (FirstName, LastName, Email, Password, CreatedDate, UpdatedDate) " +
                               "VALUES (@FirstName, @LastName, @Email, @Password, @CreatedDate, @UpdatedDate);";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@FirstName", user.FirstName);
                    command.Parameters.AddWithValue("@LastName", user.LastName);
                    command.Parameters.AddWithValue("@Email", user.Email);
                    command.Parameters.AddWithValue("@Password", user.Password);
                    command.Parameters.AddWithValue("@CreatedDate", user.CreatedDate);
                    command.Parameters.AddWithValue("@UpdatedDate", user.UpdatedDate);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}

