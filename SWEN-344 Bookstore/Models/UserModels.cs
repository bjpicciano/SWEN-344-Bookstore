using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/**
 * We may want to extract these to their own files,
 * but im unsure where the database comes in
 **/
namespace SWEN_344_Bookstore.Models {
    public class User {
        private int uid { get; set; }

        public int getUid () {
            return this.uid;
        }
    }

    public class ActiveUser {
        public List<Book> receipts { get; set; } //should probably be it's own objects with date, hasOwnership, etc
        public List<Book> shoppingCart { get; set; }

        public void buyBook () {
            //for each book in shoppingCart
                //spend some money
                //addReceipt(book);
        }

        public void returnBook(Book book) {
            //increment Bookstore stock + 1
            //set the receipt hasOwnership to false
            //remove book from User
        }

        public List<Book> getReceipts () {
            return this.receipts;
        }

        public void addReceipt (Book book) {
            this.receipts.Add(book);
        }

        public List<Book> getShoppingCart () {
            return this.shoppingCart;
        }

        public void addToShoppingCart (Book book) {
            this.shoppingCart.Add(book);
        }

        public void removeFromShoppingCart (Book book) {
            this.shoppingCart.Remove(book);
        }
    }

    public class Student : ActiveUser {
    }

    public class Instructer : ActiveUser {
        private List<Book> classBooks { get; set; }

        public void addClassBook (Book book) {
            this.classBooks.Add(book);
        }

        public void removeClassBook(Book book) {
            this.classBooks.Remove(book);
        }

        public List<Book> getClassBooks () {
            return this.classBooks;
        }
    }

    public class Admin : User {
    }
}