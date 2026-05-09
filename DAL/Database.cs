using Microsoft.Data.Sqlite;
namespace LMS.API.DAL
{   
    public  static class Database
    {
        public static string ConnectionString = "Data Source=Data/library.db";
        public static SqliteConnection GetConnection()
        {
            var conn = new SqliteConnection(ConnectionString);
            conn.Open();
            return conn;




        }













    }
}
