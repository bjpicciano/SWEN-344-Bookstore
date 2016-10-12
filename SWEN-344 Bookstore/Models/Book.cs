using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SWEN_344_Bookstore.Models {
    public class Book {
        private int b_id { get; set; }
        private string author { get; set; }
        private bool isEnabled { get; set; }
        private List<string> reviews { get; set; }

        public Book (int b_id,string author) {
            this.b_id = b_id;
            this.author = author;
            this.isEnabled = false;
        }

        public void setAuthor (string author) {
            this.author = author;
        }

        public void enable () {
            this.isEnabled = true;
        }

        public void disable () {
            this.isEnabled = false;
        }

        public List<string> getReviews () {
            return this.reviews;
        }

        public void addReview (string review) {
            this.reviews.Add(review);
        }
    }
}