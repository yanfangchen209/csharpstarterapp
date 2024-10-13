using System.Data;
using Dapper;
using csharpstarterapp.Data;
using csharpstarterapp.Models;
// SQL Server (System.Data.SqlClient) and Oracle (System.Data.OracleClient) 
// are available for connecting to respective databases.
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Text.Json; // Built-in library for JSON handling.
using Newtonsoft.Json;  // Third-party library for JSON handling.
using Newtonsoft.Json.Serialization;
using AutoMapper; // Library used for object-to-object mapping.

    /*
     * Program1 demonstrates:
     * - Dapper-based database operations.
     * - Reading, writing, and serializing/deserializing JSON files.
     * - Mapping snake_case JSON properties to PascalCase C# properties using both AutoMapper and the JsonPropertyName attribute.
     * - File I/O operations to log SQL queries and handle JSON data.
     * 
     * Key Features:
     * 1. Dapper for database querying and executing SQL commands.
     * 2. JSON deserialization using System.Text.Json and Newtonsoft.Json.
     * 3. AutoMapper and JsonPropertyName to map JSON snake_case fields to PascalCase C# fields.
     */
namespace csharpstarterapp {
    internal class Program1 {

        static void Main(string[] args) {

            // Load configuration from appsettings.json.
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            // Initialize Dapper with the configuration.
            DataContextDapper dapper = new DataContextDapper(config);

            /*
             * Commented-out code below shows how to insert data into the database using Dapper.
             * This code creates a Computer object, constructs an SQL query, and writes it to a log file.
             * The code also shows how to append text to an existing file and read from it.
             */
            // // Data to populate the database.
            // Computer myComputer = new Computer(){
            //     Motherboard = "Z690",
            //     HasWifi = true,
            //     HasLTE = false,
            //     ReleaseDate = DateTime.Now,
            //     Price = 943.87m,
            //     VideoCard = "RTX 2060"
            // };
            // // Insert records query.
            // string sql = @"INSERT INTO StarterAppSchema.Computer(
            //     Motherboard,
            //     HasWifi,
            //     HasLTE,
            //     ReleaseDate,
            //     Price,
            //     VideoCard
            // ) VALUES ('" + myComputer.Motherboard
            //     + "', '" + myComputer.HasWifi
            //     + "', '" + myComputer.HasLTE
            //     + "', '" + myComputer.ReleaseDate
            //     + "', '" + myComputer.Price
            //     + "', '" + myComputer.VideoCard
            //     + "')";

            // /*
            //  * File I/O section:
            //  * - Writes SQL query to a log file (either overwrite or append).
            //  * - Reads content from the log file and prints it.
            //  */
            // // Use built-in method to overwrite the log file with the SQL query.
            // // File.WriteAllText("log.txt", "\n" + sql + "\n");

            // // Use built-in method to append new content to the existing log file.
            // // using StreamWriter openFile = new("log.txt", append: true);
            // // openFile.WriteLine("\n" + sql + "\n");
            // // openFile.Close();

            // // string fileText = File.ReadAllText("log.txt");
            // // Console.WriteLine(fileText);

            /*
             * JSON Handling Section:
             * - Demonstrates how to read JSON data from a file, deserialize it into C# objects using both System.Text.Json and Newtonsoft.Json,
             *   and insert the data into the database using Dapper.
             */
            // string computersJson = File.ReadAllText("Computers.json");

            // // Deserializing JSON string to C# objects using System.Text.Json.
            //Without setting this, System.Text.Json will expect property names in exactly the same case in both the C# model and JSON.
            // JsonSerializerOptions options = new JsonSerializerOptions() {
            //     PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            // };
            // IEnumerable<Computer>? computersSystemText = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<Computer>>(computersJson, options);
            // if (computersSystemText != null) {
            //     foreach (Computer computer in computersSystemText) {
            //        // Console.WriteLine(computer.Motherboard);
            //     }
            // }

            // // Deserializing JSON string to C# objects using Newtonsoft.Json.
            // IEnumerable<Computer>? computersNewtonSoft = JsonConvert.DeserializeObject<IEnumerable<Computer>>(computersJson);
            // if (computersNewtonSoft != null) {
            //     foreach (Computer computer in computersNewtonSoft) {
            //         // Construct SQL query for inserting each computer object into the database.
            //         string sql2 = @"INSERT INTO StarterAppSchema.Computer(
            //             Motherboard,
            //             HasWifi,
            //             HasLTE,
            //             ReleaseDate,
            //             Price,
            //             VideoCard
            //         ) VALUES ('" + EscapeSingleQuote(computer.Motherboard)
            //             + "', '" + computer.HasWifi
            //             + "', '" + computer.HasLTE
            //             + "', '" + computer.ReleaseDate
            //             + "', '" + computer.Price
            //             + "', '" + EscapeSingleQuote(computer.VideoCard)
            //             + "')";
            //         dapper.ExecuteSql(sql2);
            //     }
            // }

            // // Serializing C# objects to JSON using System.Text.Json.
            // string computersCopySystemText = System.Text.Json.JsonSerializer.Serialize<IEnumerable<Computer>>(computersSystemText!, options);
            
            // // Serializing C# objects to JSON using Newtonsoft.Json.
            // JsonSerializerSettings settings = new JsonSerializerSettings() {
            //     ContractResolver = new CamelCasePropertyNamesContractResolver()
            // };
            // string computersCopyNewtonsoft = JsonConvert.SerializeObject(computersNewtonSoft, settings);

            // // Writing serialized JSON data to files.
            // File.WriteAllText("computersCopySystemText.txt", computersCopySystemText);
            // File.WriteAllText("computersCopyNewtonSoft.txt", computersCopyNewtonsoft);

            // // Helper function to escape single quotes in SQL queries.
            // static string EscapeSingleQuote(string input) {
            //     return input.Replace("'", "''");
            // }

            /*
             * JSON Property Mapping Section:
             * - Reads JSON from a file that uses snake_case.
             * - Demonstrates mapping JSON snake_case fields to C# PascalCase fields using both AutoMapper and JsonPropertyName attributes.
             */
            string computerSnakeJson = File.ReadAllText("ComputerSnake.json");

            // // Deserializing snake_case JSON to C# objects using System.Text.Json.
            // IEnumerable<ComputerSnake>? computerSnakeSystemText = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<ComputerSnake>>(computerSnakeJson);

            // // AutoMapper configuration to map snake_case JSON fields to PascalCase C# fields.
            // Mapper mapper = new Mapper(new MapperConfiguration((cfg) => {
            //     cfg.CreateMap<ComputerSnake, Computer>()
            //         .ForMember(destination => destination.ComputerId, options =>
            //             options.MapFrom(source => source.computer_id))
            //         .ForMember(destination => destination.CPUCores, options =>
            //             options.MapFrom(source => source.cpu_cores))
            //         .ForMember(destination => destination.HasLTE, options =>
            //             options.MapFrom(source => source.has_lte))
            //         .ForMember(destination => destination.HasWifi, options =>
            //             options.MapFrom(source => source.has_wifi))
            //         .ForMember(destination => destination.Motherboard, options =>
            //             options.MapFrom(source => source.motherboard))
            //         .ForMember(destination => destination.VideoCard, options =>
            //             options.MapFrom(source => source.video_card))
            //         .ForMember(destination => destination.ReleaseDate, options =>
            //             options.MapFrom(source => source.release_date))
            //         .ForMember(destination => destination.Price, options =>
            //             options.MapFrom(source => source.price));
            // }));

            // // Using AutoMapper to map and print results.
            // if (computerSnakeSystemText != null) {
            //     IEnumerable<Computer> computerResult = mapper.Map<IEnumerable<Computer>>(computerSnakeSystemText);
            //     // Console.WriteLine("Automapper Count: " + computerResult.Count());
            //     foreach (Computer computer in computerResult) {
            //         Console.WriteLine(computer.Motherboard);
            //     }
            // }

            // JSON property mapping using JsonPropertyName attribute.
            IEnumerable<Computer>? computersJsonPropertyMapping = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<Computer>>(computerSnakeJson);
            if (computersJsonPropertyMapping != null) {
                Console.WriteLine("JSON Property Count: " + computersJsonPropertyMapping.Count());
                foreach (Computer computer in computersJsonPropertyMapping) {
                    Console.WriteLine(computer.Motherboard);
                }
            }
        }
    }
}
