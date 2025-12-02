using System;
using System.Data.SqlClient;

namespace SampleApp
{
    public class DatabaseHelper
    {
        private readonly string connectionString =
            "Server=localhost;Database=TestDB;Trusted_Connection=True;";

        // ❌ Vulnerable SQL Injection (CodeQL will detect this)
        public string GetUserByName_Insecure(string username)
        {
            string query = "SELECT * FROM Users WHERE Username = '" + username + "'";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    return reader.HasRows ? "User found!" : "No user found.";
                }
            }
        }

        // ✅ Secure version (parameterized query)
        public string GetUserByName_Secure(string username)
        {
            string query = "SELECT * FROM Users WHERE Username = @username";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@username", username);

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    return reader.HasRows ? "User found!" : "No user found.";
                }
            }
        }
    }
}
