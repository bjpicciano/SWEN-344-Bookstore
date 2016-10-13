using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SWEN_344_Bookstore.Models {
    public class Book {
        private int b_id { get; set; }
        private string author { get; set; }
        private List<string> reviews { get; set; }

        /**
         * We may not need constructors because of how
         * the fields are set up
         **/
        public Book (int b_id,string author) {
            this.b_id = b_id;
            this.author = author;
            
        }
        public void setBookName (string bookName)
        {
            this.bookName = bookName;
        }

        public void setAuthor (string author) {
            this.author = author;
        }

        public List<string> getReviews () {
            return this.reviews;
        }

        public void addReview (string review) {
            this.reviews.Add(review);
        }
    }

    public class InventoryBook {
        private Book book;
        private int stock;
        public bool isEnabled;

        public InventoryBook(Book book) {
            this.book = book;
            this.stock = 0;
            this.isEnabled = false;
        }

        public void enable() {
            this.isEnabled = true;
        }

        public void disable() {
            this.isEnabled = false;
        }
    }
}