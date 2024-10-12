/*
 * This C# program demonstrates how to interact with databases, handle JSON data, and use AutoMapper for object mapping.
 * It showcases how to use Dapper for database operations, how to serialize/deserialize JSON using System.Text.Json and Newtonsoft.Json,
 * and how to map between different object models using AutoMapper and JsonPropertyName attributes.
 * 
 * Dependencies:
 * - Dapper: A lightweight ORM to simplify database operations.
 * - System.Text.Json: A built-in library to handle JSON serialization and deserialization.
 * - Newtonsoft.Json: A popular third-party library for handling JSON.
 * - AutoMapper: A library used to map properties between objects.
 * 
 * Key Features:
 * 1. Database operations using Dapper (LoadDataSingle, ExecuteSqlWithRowCount).
 * 2. JSON deserialization using both System.Text.Json and Newtonsoft.Json.
 * 3. File I/O operations to read and write text or JSON data.
 * 4. Object mapping with AutoMapper for transforming snake_case JSON properties into PascalCase C# properties.
 * 5. JSON property mapping using JsonPropertyName attribute to map snake_case JSON fields directly to PascalCase C# fields.
 * 
 * Usage: This program reads from a configuration file to set up a Dapper database context, reads JSON files, deserializes them (using both `JsonPropertyName` and AutoMapper for property mapping), and performs database inserts.
 */

using System.Data;
using Dapper;
using csharpstarterapp.Data;
using csharpstarterapp.Models;
using Microsoft.Data.SqlClient;  // For SQL Server database connections.
using Microsoft.Extensions.Configuration;
using System.Text.Json;  // For JSON handling.
using Newtonsoft.Json;    // For advanced JSON handling.
using Newtonsoft.Json.Serialization;
using AutoMapper;         // For object mapping.

namespace csharpstarterapp
{
    internal class Program1
    {
        static void Main(string[] args)
        {
            // Load configuration from 'appsettings.json' for database connection details.
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            // Initialize Dapper for database interaction.
            DataContextDapper dapper = new DataContextDapper(config);

            /* Uncomment the following sections to enable:
             * - Database insertion using raw SQL queries and Dapper.
             * - File writing/reading operations.
             * - JSON deserialization using System.Text.Json and Newtonsoft.Json.
             */

            /*
             * JSON section:
             * Deserializes JSON from the file 'Computers.json' using both System.Text.Json and Newtonsoft.Json.
             * Inserts deserialized data into the database using Dapper.
             */

            // Read snake_case JSON data from 'ComputerSnake.json'.
            string computerSnakeJson = File.ReadAllText("ComputerSnake.json");

            /*
             * Example 1: Use AutoMapper to map snake_case fields to PascalCase C# object fields.
             * AutoMapper configures mappings between the ComputerSnake class (snake_case) and the Computer class (PascalCase).
             */
            /*
            Mapper mapper = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ComputerSnake, Computer>()
                    .ForMember(dest => dest.ComputerId, opts => opts.MapFrom(src => src.computer_id))
                    .ForMember(dest => dest.CPUCores, opts => opts.MapFrom(src => src.cpu_cores))
                    .ForMember(dest => dest.HasLTE, opts => opts.MapFrom(src => src.has_lte))
                    .ForMember(dest => dest.HasWifi, opts => opts.MapFrom(src => src.has_wifi))
                    .ForMember(dest => dest.Motherboard, opts => opts.MapFrom(src => src.motherboard))
                    .ForMember(dest => dest.VideoCard, opts => opts.MapFrom(src => src.video_card))
                    .ForMember(dest => dest.ReleaseDate, opts => opts.MapFrom(src => src.release_date))
                    .ForMember(dest => dest.Price, opts => opts.MapFrom(src => src.price));
            }));
            */

            /*
             * Example 2: Deserialize snake_case JSON using System.Text.Json's built-in JsonPropertyName mapping.
             */
            IEnumerable<Computer>? computersJsonPropertyMapping = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<Computer>>(computerSnakeJson);

            if (computersJsonPropertyMapping != null)
            {
                // Outputs the number of deserialized objects and their 'Motherboard' property values.
                Console.WriteLine("JSON Property Count: " + computersJsonPropertyMapping.Count());
                foreach (Computer computer in computersJsonPropertyMapping)
                {
                    Console.WriteLine(computer.Motherboard);
                }
            }
        }
    }
}
