using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyLibrary.SQL_Client
{
    /// <summary>
    /// This class contains stuff needed for making a connections,
    /// keeping the connection, and running the query.
    /// - Singleton, only run one connection from this class.
    ///     just to keep things simple.
    /// </summary>
    public class MyLittleSqlClient
    {
        /// <summary>
        /// Bool if you want to see connection log and stuff like that. 
        /// </summary>
        public static bool Log = false;

        /// <summary>
        /// Singleton
        /// </summary>
        public static MyLittleSqlClient TheInstance;
        public MySqlConnection DBConn;

        /// <summary>
        /// Constructor internal to keep singleton. 
        /// </summary>
        internal MyLittleSqlClient()
        {

        }

        /// <summary>
        /// Get the default config for the testing codes.
        /// </summary>
        /// <returns></returns>
        public static IDictionary<string, string> GetDefultConnectionConfig()
        {
            return new Dictionary<string, string>()
            {
                {"Server", "localhost" },
                { "UserID", "root"},
                { "DataBase", "myschema" },
                { "PassWord", "PassWord" }
            };
        }

        /// <summary>
        /// Return an instance of the connection. 
        /// </summary>
        /// <returns></returns>
        public static MyLittleSqlClient GetInstance()
        {
            if (TheInstance != null) return MyLittleSqlClient.TheInstance;

            var config = GetDefultConnectionConfig();
            var builder = new MySqlConnectionStringBuilder();
            builder.Server = config["Server"];
            builder.UserID = config["UserID"];
            builder.Database = config["DataBase"];
            builder.Password = config["PassWord"];
            var connectionstring = builder.ConnectionString;
            var dbconn = new MySqlConnection(connectionstring);
            dbconn.Open();
            var res = new MyLittleSqlClient();
            res.DBConn = dbconn;
            return res;
        }

        /// <summary>
        /// Get an instance of the db connection. 
        /// </summary>
        /// <returns></returns>
        public static Task<MyLittleSqlClient> GetInstanceAsync()
        {
            var res = Task<MyLittleSqlClient>.Run
                (
                    () =>
                    {
                        return GetInstance();
                    }
                );
            return res;

        }
    }

    /// <summary>
    /// This class contains method that build the connection to the databse.
    ///
    /// </summary>
    public class SQLConnectionDemo
    {
        /// <summary>
        /// This is an example of connecting to a mysql database.
        /// </summary>
        public static void Demo()
        {
            try
            {
                var Server = "localhost";
                var Database = "myschema";
                var Uid = "root";
                var Password = "password";

                // Initialize that databse, using connection string builder.

                var builder = new MySqlConnectionStringBuilder();
                builder.Server = Server;
                builder.UserID = Uid;
                builder.Password = Password;
                builder.Database = Database;
                Console.WriteLine("This is the connection string: " +
                    builder.ToString());

                var dbconn = new MySqlConnection(builder.ToString());
                Console.WriteLine(dbconn);
                Console.WriteLine("Connect and select everything from the database.");

                var query = "SELECT * FROM mytable";
                var mysqlcommand = new MySqlCommand(query, dbconn);
                Console.WriteLine("Opening connection to the database. ");
                dbconn.Open(); //Do this stage as late as possible.
                MySqlDataReader reader = mysqlcommand.ExecuteReader();

                while (reader.Read())
                {
                    int id = (int)reader["id"];
                    string col1 = reader["col1"].ToString();
                    string col2 = reader["col2"].ToString();
                    string col3 = reader["col3"].ToString();
                    Console.WriteLine($"{id}, {col1}, {col2}, {col3}");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }

    internal class SQLClientCode
    {
    }
}