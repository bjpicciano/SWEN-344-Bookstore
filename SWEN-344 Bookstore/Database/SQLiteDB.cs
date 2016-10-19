using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Xml.Serialization;
using System.Data.SQLite;
using SWEN_344_Bookstore.Models;
using System.Collections;

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
            dbConnection = new SQLiteConnection(@"Data Source=C:\testDBFolder\Swen344BookStore.sqlite; Version=3; Integrated Security=True");
            dbConnection.Open();
        }

        public void InsertInventoryBook(int quantity, Boolean enabled, int reviewid)
        {
            SQLiteCommand insert = new SQLiteCommand("insert into InventoryBook(Quantity, Enabled, ReviewID) values (" + quantity + ", \"" + enabled + "\", " + reviewid + ")", dbConnection);
            insert.ExecuteNonQuery();
        }
        
        public void InsertReview(int reviewid, int userid, String review)
        {
            SQLiteCommand insert = new SQLiteCommand("insert into Review(ReviewID, UserID, Date, Review) values (" + reviewid + ", " + userid + ", " + DateTimeSQLite(DateTime.Now) + ", " + review + ")", dbConnection);
            insert.ExecuteNonQuery();
        }

        private string DateTimeSQLite(DateTime datetime)
        {
            string dateTimeFormat = "{0}-{1}-{2} {3}:{4}:{5}.{6}";
            return string.Format(dateTimeFormat, datetime.Year, datetime.Month, datetime.Day, datetime.Hour, datetime.Minute, datetime.Second, datetime.Millisecond);
        }

        public InventoryBook GetInventoryBook(int InventoryBookID)
        {
            string query = "SELECT * FROM InventoryBook where InventoryBookID == " + InventoryBookID;
            SQLiteCommand command = new SQLiteCommand(query, dbConnection);
            SQLiteDataReader rdr = command.ExecuteReader();
            Book readBook = new Book();
            InventoryBook toReturn = new InventoryBook(readBook);
            try
            {
                readBook.BookId = InventoryBookID;
                toReturn.AddToStock(rdr.GetInt32(1));
                toReturn.SetEnabled(rdr.GetBoolean(2));
                ArrayList reviews = GetReviews(rdr.GetInt32(3));
                for(int i = 0; i < reviews.Count; i++)
                {
                    readBook.AddReview((String) reviews[i]);
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            rdr.Close();
            return toReturn;
        }

        public ArrayList GetReviews(int ReviewID)
        {
            string query = "SELECT * FROM Reviews where ReviewID == " + ReviewID;
            ArrayList reviews = new ArrayList();
            SQLiteCommand command = new SQLiteCommand(query, dbConnection);
            SQLiteDataReader rdr = command.ExecuteReader();
            try
            {
                reviews.Add(rdr.GetString(3));
                while (rdr.Read())
                {
                    reviews.Add(rdr.GetString(3));
                }
            }catch(Exception ex)
            {
                
            }

            return reviews;
        }
    }
}
