using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Xml.Serialization;
using System.Data.SQLite;

namespace Database_Test
{
    class SQLite_Database
    {

        public void SQLiteDB()
        {
            Console.WriteLine("SQLite Database");

            SQLiteConnection connection = new SQLiteConnection(@"Data Source=C:\Users\Ryan\Desktop\Swen344BookStore.sqlite; Version=3; Integrated Security=True");

            string query = "SELECT * FROM InventoryBook";

            SQLiteCommand command = new SQLiteCommand(query, connection);
            connection.Open();


            SQLiteCommand newCommand = new SQLiteCommand("insert into InventoryBook(InventoryBookID, Quantity, Enabled, ReviewID) values (1,3,\"TRUE\",1)", connection);
            newCommand.ExecuteNonQuery();
            SQLiteDataReader reader = command.ExecuteReader();
            try
            {
                Console.WriteLine(" ----------------------------------------------------");
                while (reader.Read())
                {
                    System.Diagnostics.Debug.WriteLine(reader.NextResult());
                    Console.WriteLine(" ----------------------------------------------------");
                }
                Console.WriteLine(" ----------------------------------------------------");
                //Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                //Console.ReadLine();
            }
            finally
            {
                reader.Close();
                connection.Close();
            }
        }
    }
}
