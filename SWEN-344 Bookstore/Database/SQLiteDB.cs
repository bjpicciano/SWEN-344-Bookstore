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

        public static SQLite_Database GetInstance()
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
            dbConnection = new SQLiteConnection(@"Data Source=C:\\database\\Swen344BookStore.sqlite; Version=3; Integrated Security=True");
            dbConnection.Open();
        }

        public Boolean CreateInventoryBook(int bookid, int quantity, Boolean enabled)
        {
            try
            {
                SQLiteCommand insert = new SQLiteCommand("insert into InventoryBook(BookID, Quantity, Enabled) values (" + bookid + ", " + quantity + ", \"" + enabled + "\")", dbConnection);
                insert.ExecuteNonQuery();
                return true;
            }catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
                return false;
            }
        }

        public Boolean UpdateInventoryBook(int bookid, int quantity, Boolean enabled)
        {
            try
            {
                SQLiteCommand insert = new SQLiteCommand("UPDATE InventoryBook SET BookID = " + bookid  + ", Quantity = " + quantity + ", Enabled = " + enabled + " WHERE InventoryBookID = " + bookid, dbConnection);
                insert.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
                return false;
            }
        }

        public Boolean CreateReview(int InvBookID, int userid, String review)
        {
            Exception except;
            try
            {
                SQLiteCommand insert = new SQLiteCommand("insert into Review(InventoryBookID, UserID, Date, Review) values (" + InvBookID + ", " + userid + ", \"" + DateTimeSQLite(DateTime.Now) + "\", \"" + review + "\")", dbConnection);
                insert.ExecuteNonQuery();
                return true;
            }catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
                except = ex;
                return false;
            }
            //throw except;
            //return false;
        }

        private string DateTimeSQLite(DateTime datetime)
        {
            string dateTimeFormat = "{0}-{1}-{2} {3}:{4}:{5}.{6}";
            return string.Format(dateTimeFormat, datetime.Year, datetime.Month, datetime.Day, datetime.Hour, datetime.Minute, datetime.Second, datetime.Millisecond);
        }

        public List<InventoryBook> GetInventoryBooks()
        {
            string query = "SELECT * FROM InventoryBook";
            SQLiteCommand command = new SQLiteCommand(query, dbConnection);
            SQLiteDataReader rdr = command.ExecuteReader();
            List<InventoryBook> toReturn = new List<InventoryBook>();
            while (rdr.Read())
            {
                InventoryBook book = new InventoryBook();
                book.AddToStock(rdr.GetInt32(1));
                book.SetEnabled(rdr.GetBoolean(2));
                List<String> reviews = GetReviews(rdr.GetInt32(1));
                for (int i = 0; i < reviews.Count; i++)
                {
                    book.AddReview((String)reviews[i]);
                }
                toReturn.Add(book);
            }
            return toReturn;
        }

        public InventoryBook GetInventoryBook(int InventoryBookID)
        {
            string query = "SELECT * FROM InventoryBook where InventoryBookID == " + InventoryBookID;
            SQLiteCommand command = new SQLiteCommand(query, dbConnection);
            SQLiteDataReader rdr = command.ExecuteReader();
            InventoryBook toReturn = new InventoryBook();
            //try
            rdr.Read();
            {
                toReturn.AddToStock(rdr.GetInt32(1));
                toReturn.SetEnabled(rdr.GetBoolean(2));
                List<String> reviews = GetReviews(rdr.GetInt32(1));
                for(int i = 0; i < reviews.Count; i++)
                {
                    toReturn.AddReview((String) reviews[i]);
                }
            }
            //catch (Exception ex)
            {
                //System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            rdr.Close();
            return toReturn;
        }

        public List<String> GetReviews(int InvBookID)
        {
            string query = "SELECT * FROM Review where InventoryBookID == " + InvBookID;
            List<String> reviews = new List<String>();
            SQLiteCommand command = new SQLiteCommand(query, dbConnection);
            SQLiteDataReader rdr = command.ExecuteReader();
            try
            {
                rdr.Read();
                reviews.Add(rdr.GetString(4));
                while (rdr.Read())
                {
                    reviews.Add(rdr.GetString(4));
                }
            }catch(Exception ex)
            {
                throw ex;
            }

            return reviews;
        }
    }
}
