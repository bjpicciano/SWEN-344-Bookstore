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
            if (singletonInstance == null)
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
            String command = "insert into InventoryBook(BookID, BookStoreID, Quantity, Enabled) values (@BOOKID, 1, @QUAN, @ENAB)";
            SQLiteCommand insert = new SQLiteCommand(command, dbConnection);
            insert.Parameters.Add(new SQLiteParameter("@BOOKID", bookid));
            insert.Parameters.Add(new SQLiteParameter("@QUAN", quantity));
            insert.Parameters.Add(new SQLiteParameter("@ENAB", enabled));
            insert.ExecuteNonQuery();
            return true;
        }

        
        public Boolean UpdateInventoryBook(int IBookID, int bookid, int quantity, Boolean enabled)
        {
            try
            {
                SQLiteCommand insert = new SQLiteCommand("UPDATE InventoryBook SET BookID = " + bookid + ", Quantity = " + quantity + ", Enabled = " + enabled + " WHERE InventoryBookID = " + IBookID, dbConnection);
                insert.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
                return false;
            }
        }

        public Boolean UpdateInventoryBookFromBookID(int bookid, int quantity, Boolean enabled) {
            try {
                SQLiteCommand insert = new SQLiteCommand("UPDATE InventoryBook SET BookID = " + bookid + ", Quantity = " + quantity + ", Enabled = " + enabled + " WHERE BookID = " + bookid, dbConnection);
                insert.ExecuteNonQuery();
                return true;
            } catch (Exception ex) {
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
                return false;
            }
        }

        public Boolean CreateReview(int InvBookID, int userid, String review)
        {
            Exception except;
            try
            {
                SQLiteCommand insert = new SQLiteCommand("insert into Review(InventoryBookID, UserID, BookStoreID, Date, Review) values (" + InvBookID + ", " + userid + ", 1,\"" + DateTimeSQLite(DateTime.Now) + "\", \"" + review + "\")", dbConnection);
                insert.ExecuteNonQuery();

                return true;
            }
            catch (Exception ex)
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
                book.AddToStock(rdr.GetInt32(3));
                book.SetEnabled(rdr.GetBoolean(4));
                book.SetBook(rdr.GetInt32(1));
                book.reviews = GetReviews(rdr.GetInt32(1));
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
            rdr.Read();
            toReturn.AddToStock(rdr.GetInt32(3));
            toReturn.SetEnabled(rdr.GetBoolean(4));
            toReturn.SetBook(rdr.GetInt32(1));
            toReturn.reviews = GetReviews(rdr.GetInt32(0));
            toReturn.ibid = rdr.GetInt32(0);
            rdr.Close();
            return toReturn;
        }

        public InventoryBook GetInventoryBookByRemoteBookID(int RemoteBookID) {
            string query = "SELECT * FROM InventoryBook where BookID == " + RemoteBookID;
            SQLiteCommand command = new SQLiteCommand(query, dbConnection);
            SQLiteDataReader rdr = command.ExecuteReader();
            InventoryBook toReturn = new InventoryBook();
            rdr.Read();
                toReturn.AddToStock(rdr.GetInt32(3));
                toReturn.SetEnabled(rdr.GetBoolean(4));
                toReturn.SetBook(rdr.GetInt32(1));
                toReturn.reviews = GetReviews(rdr.GetInt32(0));
            toReturn.ibid = rdr.GetInt32(0);
            rdr.Close();
            return toReturn;
        }

        public List<Review> GetReviews(int InvBookID)
        {
            string query = "SELECT * FROM Review where InventoryBookID == " + InvBookID;
            List<Review> reviews = new List<Review>();
            SQLiteCommand command = new SQLiteCommand(query, dbConnection);
            SQLiteDataReader rdr = command.ExecuteReader();
            try
            {
                Review r = new Review();
                while (rdr.Read())
                {
                    r = new Review();
                    r.review = rdr.GetString(5);
                    r.date = rdr.GetString(4);
                    reviews.Add(r);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return reviews;
        }

        public List<ShoppingCartBook> GetUsersShoppingCart(int UserID)
        {
            string query = "SELECT * FROM ShoppingCartBook where UserId == " + UserID;
            List<ShoppingCartBook> shoppingcart = new List<ShoppingCartBook>();
            SQLiteCommand command = new SQLiteCommand(query, dbConnection);
            SQLiteDataReader rdr = command.ExecuteReader();
            try
            {
                ShoppingCartBook s = new ShoppingCartBook(UserID);
                while (rdr.Read())
                {
                    s = new ShoppingCartBook(UserID);
                    s.bookID = rdr.GetInt32(2);
                    //s.Date = rdr.GetString(4);
                    shoppingcart.Add(s);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return shoppingcart;
        }


        // Zane staarted this but may not need it from daniels additions
       /* public ShoppingCartBook AddToShoppingCart(int UserID)
        {
            var addBook = new ShoppingCartBook(UserID);

            // if Inventory contains inventoryBook, increment its stock by 1
            Boolean exists = false;
            foreach (InventoryBook ib in Inventory)
            {
                if (ib.GetBook() == book.BookId)
                {
                    ib.incStock();
                    exists = true;
                }
            }
            // else add it to Inventory
            if (exists == false)
            {
                this.AddToShoppingCart.Add();
            }
        }
        */


        public List<Transaction> GetTransactionsByUserID(int UserID)
        {
            string query = "SELECT * FROM TransactionBook where UserId == " + UserID;
            List<Transaction> trans = new List<Transaction>();
            SQLiteCommand command = new SQLiteCommand(query, dbConnection);
            SQLiteDataReader rdr = command.ExecuteReader();
            try
            {
                Transaction t = new Transaction(UserID);
                while (rdr.Read())
                {
                    t = new Transaction(rdr.GetInt32(3));
                    t.UserID = rdr.GetInt32(2);
                    t.BookStoreID = rdr.GetInt32(1);
                    t.Date = rdr.GetString(4);
                    t.Price = rdr.GetFloat(5);

                    trans.Add(t);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return trans;

        }

        public Boolean CreateTransaction(int userID, int bookID, float price) {
            String command = "INSERT INTO TransactionBook(BookStoreID, UserID, BookID, Date, Price) VALUES (1, @USERID, @BOOKID, @DATE, @PRICE)";
            SQLiteCommand insert = new SQLiteCommand(command, dbConnection);
            insert.Parameters.Add(new SQLiteParameter("@USERID", userID));
            insert.Parameters.Add(new SQLiteParameter("@BOOKID", bookID));
            insert.Parameters.Add(new SQLiteParameter("@DATE", DateTimeSQLite(DateTime.Now)));
            insert.Parameters.Add(new SQLiteParameter("@PRICE", price));

            insert.ExecuteNonQuery();
            return true;
        }

        public Boolean AddToShoppingCart(int bookid)
        {
            String command = "insert into ShoppingCartBook(BookID, BookStoreID, Date, UserID) values (@BOOKID, 1, 'March', 54)";
            SQLiteCommand insert = new SQLiteCommand(command, dbConnection);
            insert.Parameters.Add(new SQLiteParameter("@BOOKID", bookid));

            insert.ExecuteNonQuery();
            return true;
        }

        public Boolean RemoveFromShoppingCartBook(int bookid)
        {
            String command = "DELETE FROM ShoppingCartBook WHERE bookid == " + bookid;
            SQLiteCommand delete = new SQLiteCommand(command, dbConnection);
            delete.ExecuteNonQuery();

            return true;
        }
    }

}


