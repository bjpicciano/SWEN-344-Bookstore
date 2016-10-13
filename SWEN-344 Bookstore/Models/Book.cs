using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SWEN_344_Bookstore.Models {
    public class Book {
        private int Bid { get; set; }
        private string Author { get; set; }
        private List<string> Reviews { get; set; }

        /**
         * We may not need constructors because of how
         * the fields are set up/how we use the database
         **/
        public Book (int bid,string author) {
            this.Bid = bid;
            this.Author = author;
        }

        public void SetAuthor (string author) {
            this.Author = author;
        }

        public List<string> GetReviews () {
            return this.Reviews;
        }

        public void AddReview (string review) {
            this.Reviews.Add(review);
        }
    }

    public class InventoryBook {
        private Book _book;
        private int Stock;
        public bool IsEnabled;

        public InventoryBook (Book book) {
            this._book = book;
            this.Stock = 0;
            this.IsEnabled = false;
        }

        public int GetStock () {
            return this.Stock;
        }

        public Book GetBook()
        {
            return this._book;
        }    

        public void incStock()
        {
            this.Stock++;
        }

        public void decStock()
        {
            this.Stock--;
        }

        public void Enable () {
            this.IsEnabled = true;
        }


        public void Disable () {
            this.IsEnabled = false;
        }
    }
}