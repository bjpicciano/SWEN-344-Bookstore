using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SQLite;


namespace SWEN_344_Bookstore.Models {

    public class Book {
        public int BookId { get; set; }
        public string Author { get; set; }
        public float Price { get; set; }
        public string Name { get; set; }

        /*
         * We may not need constructors because of how
         * the fields are set up/how we use the database
        */


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
        private Book Book { get; }
        private int Stock { get; set; }
        public bool IsEnabled { get; set; }

        public InventoryBook (Book book) {
            this.Book = book;
            this.Stock = 0;
            this.IsEnabled = false;
        }

        public int GetStock (SQLiteConnection db) {
            db.Update;
            return this.Stock;
        }

        public void AddToStock (SQLiteConnection db, string name, string author, float price, int id)
        {
            var s = new Book { Name = name, Author = author, Price = price, BookId = id};
            db.Insert(s);



            //this.Book += val;
        }

        public void RemoveFromStockStock(int val)
        {
            this.Stock -= val;
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