using Microsoft.Data.SqlClient;
using System;

namespace Issue415
{
    class Program
    {
        static void Main(string[] args)
        {
            InsertSpatials();
            QuerySpatials();
            Console.WriteLine("Press any key to exit.");
            Console.ReadLine();
        }

        private static void InsertSpatials()
        {
            using (var connection = new SqlConnection("Server=.;Database=SqlClientIssue415;Integrated Security=SSPI;"))
            {
                // Open the connection
                connection.Open();

                // Create a command
                using (var command = connection.CreateCommand())
                {
                    // Set the SQL text
                    command.CommandText = "INSERT INTO [dbo].[TestTable] ([Geography], [Geometry]) VALUES (@Geography, @Geometry);";

                    // Geography column
                    command.Parameters.AddWithValue("@Geography", "POLYGON ((0 0, 50 0, 50 50, 0 50, 0 0))");

                    // Geometry column
                    command.Parameters.AddWithValue("@Geometry", "LINESTRING (-122.36 47.656, -122.343 47.656)");

                    // Execute the command
                    command.ExecuteNonQuery();
                }
            }
        }

        private static void QuerySpatials()
        {
            using (var connection = new SqlConnection("Server=.;Database=SqlClientIssue415;Integrated Security=SSPI;"))
            {
                // Open the connection
                connection.Open();

                // Create a command
                using (var command = connection.CreateCommand())
                {
                    // Set the SQL text
                    command.CommandText = "SELECT * FROM [dbo].[TestTable];";

                    // Create a reader
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Geography column
                            var geography = reader.GetValue(1);

                            // Geometry column
                            var geometry = reader.GetValue(2);
                        }
                    }
                }
            }
        }
    }
}
