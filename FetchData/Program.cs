using System;
using System.IO;
using System.Text;
using Npgsql;

class Program
{
    static void Main(string[] args)
    {
        // Connection string: Update with your PostgreSQL details
        string connectionString = "Host=localhost;Port=5432;Username=postgres;Password=lamis;Database=lamisplus";

        // Query to fetch data
        string query = "SELECT client_code, testing_setting, date_visit FROM hts_client";

        // Corrected path to save the CSV file (ensure the folder exists)
        string csvFilePath = @"C:\Users\user1\Desktop\Hts_client_Report.csv";

        try
        {
            // Establish a connection to the database
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                Console.WriteLine("Connected to the PostgreSQL database!");

                // Execute the query
                using (var command = new NpgsqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        Console.WriteLine("Writing data to CSV...");

                        // Create or overwrite the CSV file
                        using (var writer = new StreamWriter(csvFilePath, false, Encoding.UTF8))
                        {
                            // Write the header row to the CSV
                            writer.WriteLine("client_code,testing_setting,date_visit");

                            // Write rows to the CSV
                            while (reader.Read())
                            {
                                string clientCode = reader["client_code"].ToString();
                                string testingSetting = reader["testing_setting"].ToString();
                                string dateVisit = reader["date_visit"].ToString();

                                // Write a single row as a CSV line
                                writer.WriteLine($"{clientCode},{testingSetting},{dateVisit}");
                            }
                        }

                        Console.WriteLine($"Data successfully written to {csvFilePath}");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}


