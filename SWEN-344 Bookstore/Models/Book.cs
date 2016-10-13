using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SWEN_344_Bookstore.Models {
    public class Book {
        private int Bid { get; set; }
        private string Author { get; set; }
        private List<string> Reviews { get; }

        /**
         * We may not need constructors because of how
         * the fields are set up/how we use the database
         **/
        public Book (int bid,string author) {
            this.Bid = bid;
            this.Author = author;
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

        public int GetStock () {
            return this.Stock;
        }

        public void AddToStock (int val)
        {
            this.Stock += val;
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
    }
}