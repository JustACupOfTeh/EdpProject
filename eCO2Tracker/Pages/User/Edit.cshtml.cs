using eCO2Tracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySqlConnector;

namespace eCO2Tracker.Pages.User
{
    public class EditModel : PageModel
    {
        private readonly IConfiguration configuration;

        public EditModel(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        [BindProperty]
        public UserView User { get; set; }

        public IActionResult OnGet(string id)
        {
            string connectionString = configuration.GetConnectionString("DefaultConnection");
            string query = $"SELECT * FROM userdb.users WHERE Id='{id}';";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            User = new UserView
                            {
                                Id = reader.GetString("Id"),
                                FirstName = reader.GetString("FirstName"),
                                LastName = reader.GetString("LastName"),
                                Email = reader.GetString("Email"),
                                Password = reader.GetString("Password"),
                            };
                        }
                        else
                        {
                            return RedirectToPage("/UserList");
                        }
                    }
                }
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            string connectionString = configuration.GetConnectionString("DefaultConnection");
            string query = $"UPDATE userdb.users SET FirstName=@FirstName, LastName=@LastName, Email=@Email, Password=@Password, WHERE Id=@Id;";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", User.Id);
                    command.Parameters.AddWithValue("@FirstName", User.FirstName);
                    command.Parameters.AddWithValue("@LastName", User.LastName);
                    command.Parameters.AddWithValue("@Email", User.Email);
                    command.Parameters.AddWithValue("@Password", User.Password);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        return RedirectToPage("/UserList");
                    }
                }
            }

            return Page();
        }
    }
}
