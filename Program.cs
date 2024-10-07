using System.Data;
using Dapper;
using csharpstarterapp.Data;
using csharpstarterapp.Models;
//SQL Server (System.Data.SqlClient): //Oracle (System.Data.OracleClient): For connecting to Oracle databases
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace csharpstarterapp{

    
internal class Program
{


    static void Main(string[] args)
    {
        IConfiguration config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

        DataContextDapper dapper = new DataContextDapper(config);
        DataContextEF entityFramework = new DataContextEF(config);
       
        //test for database connection
        string sqlCommand = "SELECT GETDATE()";
        //1.Query for reading from database, Dapper provide extension method QuerySingle, Query, Execute,
        DateTime rightNow = dapper.LoadDataSingle<DateTime>(sqlCommand);
        Console.WriteLine(rightNow.ToString());


        //data to populate database
        Computer myComputer = new Computer(){
            Motherboard = "Z690",
            HasWifi = true,
            HasLTE = false,
            ReleaseDate = DateTime.Now,
            Price = 943.87m,
            VideoCard = "RTX 2060"
        };
        //insert records query
        string sql = @"INSERT INTO StarterAppSchema.Computer(
            Motherboard,
            HasWifi,
            HasLTE,
            ReleaseDate,
            Price,
            VideoCard
        ) VALUES ('" + myComputer.Motherboard
            + "', '" + myComputer.HasWifi
            + "', '" + myComputer.HasLTE
            + "', '" + myComputer.ReleaseDate
            + "', '" + myComputer.Price
            + "', '" + myComputer.VideoCard
            + "')";
        //2.user dapper to run insertion operation, Execute() return num of rows affacted, 
        int result = dapper.ExecuteSqlWithRowCount(sql);
        Console.WriteLine(result);//1


        //use entityframe for insertion data to db
        entityFramework.Add(myComputer);
        entityFramework.SaveChanges();



        //sql select
        string sqlSelect = @"
        SELECT
            Computer.Motherboard,
            Computer.HasWifi,
            Computer.HasLTE,
            Computer.ReleaseDate,
            Computer.Price,
            Computer.VideoCard
        FROM StarterAppSchema.Computer";
        //IEnumerable is very efficient
        //List<Computer> computers = dbConnection.Query<Computer>(sqlSelect).ToList();
        //3. dapper select multiple records
        IEnumerable<Computer> computers = dapper.LoadData<Computer>(sqlSelect);
        foreach(Computer singleComputer in computers) {
            Console.WriteLine("'" + singleComputer.Motherboard
            + "','" + singleComputer.HasWifi
            + "','" + singleComputer.HasLTE
            + "','" + singleComputer.ReleaseDate
            + "','" + singleComputer.Price
            + "','" + singleComputer.VideoCard + "'");

        }


        // use entity framework to select records from db
        IEnumerable<Computer>? computersEf = entityFramework.Computer?.ToList<Computer>();
        if (computersEf != null) {
            foreach(Computer singleComputer in computersEf) {
                Console.WriteLine("'" + singleComputer.ComputerId
                + "','" + singleComputer.Motherboard
                + "','" + singleComputer.HasWifi
                + "','" + singleComputer.HasLTE
                + "','" + singleComputer.ReleaseDate
                + "','" + singleComputer.Price
                + "','" + singleComputer.VideoCard + "'");

            }
        }

    }
}

}
