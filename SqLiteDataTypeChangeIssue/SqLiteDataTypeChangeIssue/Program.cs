using Microsoft.Data.Sqlite;
using System;

namespace SqLiteDataTypeChangeIssue
{
    class Program
    {
        private static readonly string connectionString = "Data Source=:memory:;";

        static void Main(string[] args)
        {
            Test();
        }

        static void Test()
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                CreateTable(connection);
                Query(connection);
                Insert(connection);
                Query(connection);
                Delete(connection);
                Query(connection);
            }
        }

        static void Query(SqliteConnection connection)
        {
            Console.WriteLine(new string('-', 50));
            Console.WriteLine("Querying...");
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT Id, ColumnBigInt, ColumnDecimal, ColumnInt, ColumnNumeric FROM [CompleteTable];";
                using (var reader = command.ExecuteReader())
                {
                    var count = 0;
                    for (var ordinal = 0; ordinal < reader.FieldCount; ordinal++)
                    {
                        Console.WriteLine($"{reader.GetName(ordinal)} = {reader.GetFieldType(ordinal).FullName}");
                    }
                    while (reader.Read())
                    {
                        Console.WriteLine($"Id = {reader.GetString(0)}, " +
                            $"ColumnBigInt = {(reader.IsDBNull(1) ? default : reader.GetInt64(1))} " +
                            $"ColumnDecimal = {(reader.IsDBNull(2) ? default : reader.GetDecimal(2))} " +
                            $"ColumnInt = {(reader.IsDBNull(3) ? default : reader.GetInt32(3))} " +
                            $"ColumnNumeric = {(reader.IsDBNull(4) ? default : reader.GetDouble(4))}");
                        count++;
                    }
                    Console.WriteLine($"{count} rows(s) found.");
                }
            }
        }

        static void Insert(SqliteConnection connection)
        {
            Console.WriteLine(new string('-', 50));
            Console.WriteLine("Inserting...");
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "INSERT INTO [CompleteTable] " +
                    "(ColumnBigInt, ColumnDecimal, ColumnInt, ColumnNumeric) " +
                    "VALUES " +
                    "(@ColumnBigInt, @ColumnDecimal, @ColumnInt, @ColumnNumeric); " +
                    "SELECT last_insert_rowid() AS Id;";

                // I will not use the AddWithValue, I need to specify the DbType

                // ColumnBigInt
                var parameter = command.CreateParameter();
                parameter.ParameterName = "@ColumnBigInt";
                parameter.DbType = System.Data.DbType.Int64;
                parameter.Value = long.MaxValue;
                command.Parameters.Add(parameter);

                // ColumnDecimal
                parameter = command.CreateParameter();
                parameter.ParameterName = "@ColumnDecimal";
                parameter.DbType = System.Data.DbType.Decimal;
                parameter.Value = Convert.ToDouble(100.05);
                command.Parameters.Add(parameter);

                // ColumnInt
                parameter = command.CreateParameter();
                parameter.ParameterName = "@ColumnInt";
                parameter.DbType = System.Data.DbType.Int32;
                parameter.Value = int.MaxValue;
                command.Parameters.Add(parameter);

                // ColumnNumeric
                parameter = command.CreateParameter();
                parameter.ParameterName = "@ColumnNumeric";
                parameter.DbType = System.Data.DbType.Double;
                parameter.Value = Convert.ToDouble(100.05);
                command.Parameters.Add(parameter);

                // Execute
                var id = command.ExecuteScalar();

                // Log
                Console.WriteLine($"A new row has been inserted with Id = {id}.");
            }
        }

        static void Delete(SqliteConnection connection)
        {
            Console.WriteLine(new string('-', 50));
            Console.WriteLine("Deleting...");
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "DELETE FROM [CompleteTable];";

                // Execute
                command.ExecuteNonQuery();
            }
        }

        static void CreateTable(SqliteConnection connection)
        {
            using (var command = connection.CreateCommand())
            {
                command.CommandText = @"CREATE TABLE IF NOT EXISTS [CompleteTable] 
                (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT
                    , ColumnBigInt BIGINT
                    , ColumnDecimal DECIMAL
                    , ColumnInt INT
                    , ColumnNumeric NUMERIC
                );";
                command.ExecuteNonQuery();
            }
        }
    }
}
