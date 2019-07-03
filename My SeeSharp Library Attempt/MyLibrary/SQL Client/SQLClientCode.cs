using MySql.Data.MySqlClient;
using System;

namespace MyLibrary.SQL_Client
{
    /// <summary>
    /// This class contains method that build the connection to the databse.
    ///
    /// </summary>
    public class SQLClientBuilder
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