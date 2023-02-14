using App.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySqlConnector;

namespace App.Pages.User
{
    public class IndexModel : PageModel
    {
        public IList<UserView> Users { get; set; }

        public void OnGet()
        {
            string connectionString = "server=localhost;database=userdb;uid=root;pwd=;";
            string query = "SELECT Id, FirstName, LastName, Email, Password, CreatedDate, UpdatedDate FROM userdb.users;";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        Users = new List<UserView>();

                        while (reader.Read())
                        {
                            UserView user = new UserView();
                            user.Id = reader["Id"].ToString();
                            user.FirstName = reader["FirstName"].ToString();
                            user.LastName = reader["LastName"].ToString();
                            user.Email = reader["Email"].ToString();
                            user.Password = reader["Password"].ToString();
                            user.CreatedDate = (DateTime)reader["CreatedDate"];
                            user.UpdatedDate = (DateTime)reader["UpdatedDate"];

                            Users.Add(user);
                        }
                    }
                }
            }
        }


        public IActionResult OnPost(string action, string id)
        {
            if (action == "delete")
            {
                string connectionString = "server=localhost;database=userdb;uid=root;pwd=;";
                string query = $"DELETE FROM userdb.users WHERE Id='{id}';";

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }

            return RedirectToPage();
        }
    }
}
