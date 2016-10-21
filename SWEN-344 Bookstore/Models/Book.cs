﻿using System;
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
        
        public static Boolean UpdateBook(int bookId, String author, float price, String name)
        {
            return restAPI.UpdateBook(bookId, author, price, name).Result;
        }

        public static int CreateBook(String author, float price, String name)
        {
            return restAPI.CreateBook(author, price, name).Result;
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
        private Book Book { get; }
        private int Stock { get; set; }
        public bool IsEnabled { get; set; }

        public InventoryBook (Book book) {
            this.Book = book;
            this.Stock = 0;
            this.IsEnabled = false;
        }

        public int GetStock (/*SQLiteConnection db*/) {
           // db.Update;
            return this.Stock;
        }

        public void AddToStock (int quantity)
        {
            Stock += quantity;
        }

        public void SetEnabled(Boolean enabled)
        {
            IsEnabled = enabled;
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