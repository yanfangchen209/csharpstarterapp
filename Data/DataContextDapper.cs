
using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

/* 
IDbConnection is an interface from the ADO.NET library  in the System.Data namespace that defines the basic functionality 
required for a database connection. It provides a generic way to work with database connections, 
allowing you to use various types of database connections (like SQL Server, Oracle, MySQL) 
without being tied to a specific implementation.
SqlConnection is a class that implements the IDbConnection interface
*/
namespace csharpstarterapp.Data {
    public class DataContextDapper{
        //location for database and credential
        //private IConfiguration _config;
        private string _connectionString;
        public DataContextDapper(IConfiguration config)
        {
            //This operator tells the compiler that we are sure the value will not be null, thus suppressing the warning.
            _connectionString = config.GetConnectionString("DefaultConnection")!;

        }



        public IEnumerable<T> LoadData<T>(string sql) {
            IDbConnection dbConnection = new SqlConnection(_connectionString);
            return dbConnection.Query<T>(sql);
        }

        public T LoadDataSingle<T>(string sql) {
            IDbConnection dbConnection = new SqlConnection(_connectionString);
            return dbConnection.QuerySingle<T>(sql);
        }

        public bool ExecuteSql(string sql) {
            IDbConnection dbConnection = new SqlConnection(_connectionString);
            return dbConnection.Execute(sql) > 0;
        }
        
        
        public int ExecuteSqlWithRowCount(string sql) {
            IDbConnection dbConnection = new SqlConnection(_connectionString);
            return dbConnection.Execute(sql);
        }



        




        

    }
}
