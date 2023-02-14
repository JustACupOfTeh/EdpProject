using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySqlConnector;

namespace eCO2Tracker.Pages.Admin
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public string Password { get; set; }

        private readonly IConfiguration _configuration;

        public LoginModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            using (var connection = new MySqlConnection(_configuration.GetConnectionString("MyConnection")))
            {
                await connection.OpenAsync();

                var command = new MySqlCommand("SELECT * FROM users WHERE Email = @email", connection);
                command.Parameters.AddWithValue("@email", Email);

                using (var reader = await command.ExecuteReaderAsync())
                {

                    if (await reader.ReadAsync())
                    {
                        var passwordHash = reader.GetString("Password");

                        if (BCrypt.Net.BCrypt.Verify(Password, passwordHash))
                        {
                            if (reader.GetString("Role") == "Admin")
                            {
                                return RedirectToPage("/Index");
                            }

                        }
                    }
                }
            }

           
            return Page();
        }
    }
}

