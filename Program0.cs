// using System.Data;
// using Dapper;
// using csharpstarterapp.Data;
// using csharpstarterapp.Models;
// //SQL Server (System.Data.SqlClient): 
// //Oracle (System.Data.OracleClient): For connecting to Oracle databases
// using Microsoft.Data.SqlClient;
// using Microsoft.Extensions.Configuration;

// namespace csharpstarterapp{

// /// <summary>
// /// The Program0 class serves as the entry point for the application. It demonstrates the use of Dapper 
// /// and Entity Framework (EF) for interacting with a SQL Server database, including database queries, 
// /// insertions, and data retrievals. 
// /// </summary>
// internal class Program0
// {
//     /// <summary>
//     /// The Main method is the program's entry point. It sets up the configuration, connects to the database, 
//     /// and performs several operations using both Dapper and Entity Framework:
//     /// 1. Testing the database connection by querying the current date.
//     /// 2. Inserting a new computer record into the database using Dapper and Entity Framework.
//     /// 3. Retrieving computer records from the database and printing them to the console.
//     /// </summary>
//     /// <param name="args">Command-line arguments (not used in this example).</param>
//     static void Main(string[] args)
//     {
//         // Set up the configuration by reading from the appsettings.json file
//         IConfiguration config = new ConfigurationBuilder()
//             .AddJsonFile("appsettings.json")
//             .Build();

//         // Initialize Dapper and Entity Framework data contexts
//         DataContextDapper dapper = new DataContextDapper(config);
//         DataContextEF entityFramework = new DataContextEF(config);
       
//         // 1. Test database connection by querying the current date using Dapper
//         string sqlCommand = "SELECT GETDATE()";
//         DateTime rightNow = dapper.LoadDataSingle<DateTime>(sqlCommand);
//         Console.WriteLine(rightNow.ToString());

//         // Create a new Computer object to populate the database
//         Computer myComputer = new Computer(){
//             Motherboard = "Z690",
//             HasWifi = true,
//             HasLTE = false,
//             ReleaseDate = DateTime.Now,
//             Price = 943.87m,
//             VideoCard = "RTX 2060"
//         };

//         // 2. Insert the computer record into the database using Dapper
//         string sql = @"INSERT INTO StarterAppSchema.Computer(
//             Motherboard,
//             HasWifi,
//             HasLTE,
//             ReleaseDate,
//             Price,
//             VideoCard
//         ) VALUES ('" + myComputer.Motherboard
//             + "', '" + myComputer.HasWifi
//             + "', '" + myComputer.HasLTE
//             + "', '" + myComputer.ReleaseDate
//             + "', '" + myComputer.Price
//             + "', '" + myComputer.VideoCard
//             + "')";
        
//         // Execute the insertion query and get the number of affected rows
//         int result = dapper.ExecuteSqlWithRowCount(sql);
//         Console.WriteLine(result); // Should print 1 if a row was inserted successfully

//         // Insert the same computer record using Entity Framework
//         entityFramework.Add(myComputer);
//         entityFramework.SaveChanges();

//         // 3. Select and retrieve computer records from the database using Dapper
//         string sqlSelect = @"
//         SELECT
//             Computer.Motherboard,
//             Computer.HasWifi,
//             Computer.HasLTE,
//             Computer.ReleaseDate,
//             Computer.Price,
//             Computer.VideoCard
//         FROM StarterAppSchema.Computer";
        
//         // Load multiple records using Dapper and print them to the console
//         IEnumerable<Computer> computers = dapper.LoadData<Computer>(sqlSelect);
//         foreach (Computer singleComputer in computers) {
//             Console.WriteLine("'" + singleComputer.Motherboard
//             + "','" + singleComputer.HasWifi
//             + "','" + singleComputer.HasLTE
//             + "','" + singleComputer.ReleaseDate
//             + "','" + singleComputer.Price
//             + "','" + singleComputer.VideoCard + "'");
//         }

//         // Select and retrieve records using Entity Framework and print them to the console
//         IEnumerable<Computer>? computersEf = entityFramework.Computer?.ToList<Computer>();
//         if (computersEf != null) {
//             foreach (Computer singleComputer in computersEf) {
//                 Console.WriteLine("'" + singleComputer.ComputerId
//                 + "','" + singleComputer.Motherboard
//                 + "','" + singleComputer.HasWifi
//                 + "','" + singleComputer.HasLTE
//                 + "','" + singleComputer.ReleaseDate
//                 + "','" + singleComputer.Price
//                 + "','" + singleComputer.VideoCard + "'");
//             }
//         }
//     }
// }
// }
