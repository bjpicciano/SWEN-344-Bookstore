using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SQLite;
using Database_Test;
using SWEN_344_Bookstore.Database;


namespace SWEN_344_Bookstore.Models {

    public class Book {
        private static SWEN_344_Bookstore.Database.RestAccess restAPI = SWEN_344_Bookstore.Database.RestAccess.GetInstance();
        public int BookId { get; set; }
        public string Author { get; set; }
        public float Price { get; set; }
        public string Name { get; set; }
        public string desc { get; set; }

        public override string ToString()
        {
            return "Book! Name: " + Name + ", Author: " + Author + ", Price: " + Price + ", Desc: " + desc + ", ID: " + BookId;
        }

        /*
         * We may not need constructors because of how
         * the fields are set up/how we use the database
        */

        public static Book GetBook(int bookID)
        {
            return restAPI.GetBook(bookID);
        }

        public static List<Book> GetBooks()
        {
            return restAPI.GetBooks();
        }

        public static Boolean UpdateBook(int bookId, String author, float price, String name, String description)
        {
            return restAPI.UpdateBook(bookId, author, price, name, description);
        }

        public static int CreateBook(String author, float price, String name, String description)
        {
            return restAPI.CreateBook(author, price, name, description);
        }

        public class ClassBook
        {
            public int ClassId { get; set; }
            public int BookId { get; set; }
        }




        // Database call
        public void AddReview(string review) {
        }
    }

    public class Review
    {
        public String review;
        public String date;
    }

    public class InventoryBook {
        private int Book { get; set; }
        private int Quantity { get; set; }
        public bool IsEnabled { get; set; }
        public List<Review> reviews;

        public InventoryBook() {
            this.Quantity = 0;
            this.IsEnabled = false;
        }

        public override string ToString() {
            return "InventoryBook! BookID: " + Book + ", Quantity: " + Quantity + ", IsEnabled: " + IsEnabled;
        }

        public void AddReview(Review review)
        {
            reviews.Add(review);
        }

        public int GetStock(/*SQLiteConnection db*/) {
            // db.Update;
            return this.Quantity;
        }

        public void AddToStock(int quantity)
        {
            Quantity += quantity;
        }

        public void SetEnabled(Boolean enabled)
        {
            IsEnabled = enabled;
        }

        public void RemoveFromStock(int val)
        {
            this.Quantity -= val;
        }

        public void Enable() {
            this.IsEnabled = true;
        }

        public void Disable() {
            this.IsEnabled = false;
        }

        public int GetBook()
        {
            return Book;
        }

        public void SetBook(int i)
        {
            Book = i;
        }

        internal void incStock()
        {
            throw new NotImplementedException();
        }
    }
    public class ShoppingCartBook
    {
        public int bookID { get; set; }
        public string Date { get; set; }
        public int UserID { get; set; }

        public ShoppingCartBook(int bookID)
        {
            this.bookID = bookID;
        }


    }

    public class Transaction
    {
        public int PurchaseDate { get; set; }
        public int UserID { get; set; }
        public int bookID { get; set; }
        public int BookStoreID { get; set; }
        public string Date { get; set; }
        public int Price { get; set; }

        public Transaction(int bookID)
        {
            this.bookID = bookID;
        }

        public static bool PurchaseShoppingCart(List<ShoppingCartBook> sBooks) {
            SQLite_Database localAccess = SQLite_Database.GetInstance();
            RestAccess remoteAccess = RestAccess.GetInstance();

            //Loop through each book and create a transaction
            foreach (var sBook in sBooks) {
                var price = remoteAccess.GetBook(sBook.bookID).Price; //Get book's price
                localAccess.CreateTransaction(sBook.UserID, sBook.bookID, sBook.Date, price);
            }

            return false;
        }
    }
}
