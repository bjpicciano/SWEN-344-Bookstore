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
    public class SQLite_Database
    {
        private static SQLite_Database singletonInstance;

        public static SQLite_Database getInstance()
        {
            if(singletonInstance == null)
            {
                singletonInstance = new SQLite_Database();
            }
            return singletonInstance;
        }

        private SQLiteConnection dbConnection;

        private SQLite_Database()
        {
            OpenDatabase();
        }



        private void OpenDatabase()
        {
            Console.WriteLine("SQLite Database");
            dbConnection = new SQLiteConnection(@"Data Source=C:\Swen344BookStore.sqlite; Version=3; Integrated Security=True");
            dbConnection.Open();
        }

        public void InsertInventoryBook(int quantity, Boolean enabled, int reviewid)
        {
            SQLiteCommand insert = new SQLiteCommand("insert into InventoryBook(Quantity, Enabled, ReviewID) values (" + quantity + ", " + enabled + ", " + reviewid + ")", dbConnection);
            insert.ExecuteNonQuery();
        }

        public Object[] GetInventoryBook(int InventoryBookID)
        {
            string query = "SELECT * FROM InventoryBook where InventoryBookID == " + InventoryBookID;
            SQLiteCommand command = new SQLiteCommand(query, dbConnection);
            SQLiteDataReader rdr = command.ExecuteReader();
            Object[] toReturn = new Object[4];
            try
            {
                toReturn[0] = InventoryBookID;
                toReturn[1] = rdr.GetInt32(1);
                toReturn[2] = rdr.GetBoolean(2);
                toReturn[3] = rdr.GetInt32(3);
            }
            catch (Exception ex)
            {
                return null;
            }
            rdr.Close();
            return toReturn;
        }
    }
}
