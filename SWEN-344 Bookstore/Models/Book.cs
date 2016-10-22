using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SQLite;


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
            return "Name: " + Name + " Author:" + Author + " Price:" + Price + " Desc:" + desc + " ID:" + BookId;
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
        public void AddReview (string review) {
        }
    }

    public class InventoryBook {
        private Book Book { get; set; }
        private int Quantity { get; set; }
        public bool IsEnabled { get; set; }
        public List<String> reviews;

        public InventoryBook () {
            this.Quantity = 0;
            this.IsEnabled = false;
        }

        public void AddReview(String review)
        {
            reviews.Add(review);
        }

        public int GetStock (/*SQLiteConnection db*/) {
           // db.Update;
            return this.Quantity;
        }

        public void AddToStock (int quantity)
        {
            Quantity += quantity;
        }

        public void SetEnabled(Boolean enabled)
        {
            IsEnabled = enabled;
        }

        public void RemoveFromStockStock(int val)
        {
            this.Quantity -= val;
        }

        public void Enable () {
            this.IsEnabled = true;
        }

        public void Disable () {
            this.IsEnabled = false;
        }

        internal Book GetBook()
        {
            throw new NotImplementedException();
        }

        internal void incStock()
        {
            throw new NotImplementedException();
        }
    }
}