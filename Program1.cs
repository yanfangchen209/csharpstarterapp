// using System.Data;
// using Dapper;
// using csharpstarterapp.Data;
// using csharpstarterapp.Models;
// //SQL Server (System.Data.SqlClient): //Oracle (System.Data.OracleClient): For connecting to Oracle databases
// using Microsoft.Data.SqlClient;
// using Microsoft.Extensions.Configuration;
// using System.Text.Json;
// using Newtonsoft.Json;
// using Newtonsoft.Json.Serialization;

// namespace csharpstarterapp{

    
// internal class Program
// {


//     static void Main(string[] args)
//     {

//         IConfiguration config = new ConfigurationBuilder()
//             .AddJsonFile("appsettings.json")
//             .Build();

//         DataContextDapper dapper = new DataContextDapper(config);


//         //data to populate database
//         Computer myComputer = new Computer(){
//             Motherboard = "Z690",
//             HasWifi = true,
//             HasLTE = false,
//             ReleaseDate = DateTime.Now,
//             Price = 943.87m,
//             VideoCard = "RTX 2060"
//         };
//         //insert records query
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


//         /*read/write file section*/

//         //use built in method to write "sql" to log.txt file, it will overwrite previous existing files
//         //File.WriteAllText("log.txt", "\n" + sql + "\n");

//         //use built in method to write "sql" to log.txt file, it will append new content to existing content
//         // using StreamWriter openFile = new("log.txt", append: true);
//         // openFile.WriteLine("\n" + sql + "\n");
//         // openFile.Close();

//         // string fileText = File.ReadAllText("log.txt");
//         // Console.WriteLine(fileText);




//         /*JSON section*/
//         string computersJson = File.ReadAllText("Computers.json");
//         //Console.WriteLine(computersJson);


//         //deserilaze json string to c# objects
//         JsonSerializerOptions options = new JsonSerializerOptions(){
//             PropertyNamingPolicy = JsonNamingPolicy.CamelCase
//         };
//         //use built in System.Text.Json.JsonSerializer to deserialize json data, NewtonSoft also has JsonSerializer
//         IEnumerable<Computer>? computersSystemText = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<Computer>>(computersJson, options);
//         if (computersSystemText != null) {
//             foreach (Computer computer in computersSystemText) {
//                // Console.WriteLine(computer.Motherboard);
//             }
//         }
//         //install library Newtonsoft.Json to deserialize json data
//         IEnumerable<Computer>? computersNewtonSoft = JsonConvert.DeserializeObject<IEnumerable<Computer>>(computersJson);
//         if (computersNewtonSoft != null) {
//             foreach (Computer computer in computersNewtonSoft) {
//                 //Console.WriteLine(computer.Motherboard);
//                     string sql2 = @"INSERT INTO StarterAppSchema.Computer(
//                         Motherboard,
//                         HasWifi,
//                         HasLTE,
//                         ReleaseDate,
//                         Price,
//                         VideoCard
//                     ) VALUES ('" + EscapeSingleQuote(computer.Motherboard)
//                         + "', '" + computer.HasWifi
//                         + "', '" + computer.HasLTE
//                         + "', '" + computer.ReleaseDate
//                         + "', '" + computer.Price
//                         + "', '" + EscapeSingleQuote(computer.VideoCard)
//                         + "')";
//                     dapper.ExecuteSql(sql2);
                    
//             }
//         }
        
//         //serialize c# objects to Json data using System.Text.Json.JsonSerializer, need "option" to handle camelcase property name
//         string computersCopySystemText = System.Text.Json.JsonSerializer.Serialize<IEnumerable<Computer>>(computersSystemText!, options);
              
//         //serialize c# objects to Json data using NewtonSoft, need "settings" to handle camelcase property name
//         JsonSerializerSettings  settings=  new JsonSerializerSettings()
//         {
//             ContractResolver = new CamelCasePropertyNamesContractResolver()
//         };
//         string computersCopyNewtonsoft = JsonConvert.SerializeObject(computersNewtonSoft, settings);

//         //write json data to files
//         File.WriteAllText("computersCopySystemText.txt", computersCopySystemText);
//         File.WriteAllText("computersCopyNewtonSoft.txt", computersCopyNewtonsoft);

//         static string EscapeSingleQuote(string input)
//         {
//             string output = input.Replace("'", "''");
//             return output;
//         }


//     }
// }

// }
