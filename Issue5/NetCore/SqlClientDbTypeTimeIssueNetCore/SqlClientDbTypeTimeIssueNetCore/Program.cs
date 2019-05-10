using Microsoft.Data.SqlClient;
using System;
using System.Data;

namespace SqlClientDbTypeTimeIssueNetCore
{
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
