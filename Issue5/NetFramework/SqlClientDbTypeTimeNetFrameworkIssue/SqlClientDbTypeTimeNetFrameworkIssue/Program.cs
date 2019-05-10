using Microsoft.Data.SqlClient;
using System;
using System.Data;

namespace SqlClientDbTypeTimeNetFrameworkIssue
{
    /*
     * System.BadImageFormatException: 'Could not load file or assembly 'Microsoft.Data.SqlClient, Version=1.0.19128.1, Culture=neutral, PublicKeyToken=23ec7fc2d6eaa4a5' or one of its dependencies. An attempt was made to load a program with an incorrect format.'
     * Issue Encountered: https://github.com/dotnet/SqlClient/issues/7
     */

    class Program
    {
        private const string m_connectionString = "Server=(local);Database=Test;Integrated Security=True;";

        static void Main(string[] args)
        {
            TriggerTimeIssue();

            Console.ReadLine();
        }

        static void TriggerTimeIssue()
        {
            using (var connection = new SqlConnection(m_connectionString))
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "INSERT INTO [dbo].[TimeTable] ([Time]) VALUES (@Time);";

                    var parameter = command.CreateParameter();
                    parameter.ParameterName = "@Time";
                    parameter.Value = DateTime.UtcNow.TimeOfDay;
                    parameter.DbType = DbType.Time;
                    command.Parameters.Add(parameter);

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
