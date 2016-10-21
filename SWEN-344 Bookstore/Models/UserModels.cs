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
        private int Uid { get; set; }
        private String email;
        private String fname;
        private String lname;

        public int GetUid () {
            return this.Uid;
        }
        public User(int id, String el, String fn, String ln)
        {
            Uid = id; email = el; fname = fn; lname = ln;
        }
    }

    public class ActiveUser {
        public List<Book> Receipts { get; set; } //should probably be it's own objects with date, hasOwnership, etc
        public List<Book> ShoppingCart { get; set; }

        public void BuyBook () {
            //for each book in ShoppingCart
                //spend some money
                //addReceipt(book);
        }

        public void ReturnBook(Book book) {
            //increment Bookstore stock + 1
            //set the receipt hasOwnership to false
            //remove book from User
        }

        public List<Book> GetReceipts () {
            return this.Receipts;
        }

        public void AddReceipt (Book book) {
            this.Receipts.Add(book);
        }

        public List<Book> GetShoppingCart () {
            return this.ShoppingCart;
        }

        public void AddToShoppingCart (Book book) {
            this.ShoppingCart.Add(book);
        }

        public void RemoveFromShoppingCart (Book book) {
            this.ShoppingCart.Remove(book);
        }
    }

    public class Student : ActiveUser {
    }

    public class Instructer : ActiveUser {
        private List<Book> ClassBooks { get; set; }

        public void AddClassBook (Book book) {
            this.ClassBooks.Add(book);
        }

        public void RemoveClassBook(Book book) {
            this.ClassBooks.Remove(book);
        }

        public List<Book> GetClassBooks () {
            return this.ClassBooks;
        }
    }

    public class Admin : User {
    }
}