using eCO2Tracker.Models;
using eCO2Tracker.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using MySqlConnector;
using System.ComponentModel.DataAnnotations;

namespace App.Pages.User
{
    public class CreateModel : PageModel
    {
       
        public void OnGet()
        {
        }

        [BindProperty]
        public string FirstName { get; set; }

        [BindProperty]
        public string LastName { get; set; }

        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public string Password { get; set; }

       
        public Task OnPost()
        {
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(Password);
            using (MySqlConnection connection = new MySqlConnection("Server=localhost; User=root; Pwd=; Port=3306; Database=userdb"))
            {
                string query = "INSERT INTO users (FirstName, LastName, Email, Password, CreatedDate, UpdatedDate) " +
                               "VALUES (@FirstName, @LastName, @Email, @Password, @CreatedDate, @UpdatedDate);";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@FirstName", FirstName);
                    command.Parameters.AddWithValue("@LastName", LastName);
                    command.Parameters.AddWithValue("@Email", Email);
                    command.Parameters.AddWithValue("@Password",hashedPassword);
                    command.Parameters.AddWithValue("@CreatedDate", DateTime.UtcNow);
                    command.Parameters.AddWithValue("@UpdatedDate", DateTime.UtcNow);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }

            return Task.CompletedTask;
        }
    }

}

