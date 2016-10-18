using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SQLite;

namespace SWEN_344_Bookstore.Database
{
    public class SQLite_DB
    {
        public void SQLiteDB()
        {
            Console.WriteLine("Swen344 SQLite Database");

            SQLiteConnection connection = new SQLiteConnection(@"Data Source=C:\Swen344.sqlite;Version=3;");
            string dbQuery = "SELECT * FROM [dbo].[Table]";

            SQLiteCommand command = new SQLiteCommand(dbQuery, connection);
            connection.Open();

            SQLiteDataReader reader = command.ExecuteReader();
            try
            {
                Console.WriteLine("-----------------------------------------");
                while (reader.Read())
                {
                    Console.WriteLine("-----------------------------------------");

                }
                Console.WriteLine("-----------------------------------------");
            
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                reader.Close();
                connection.Close();
            }
        }
    }
}