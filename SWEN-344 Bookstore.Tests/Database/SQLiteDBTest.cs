using System;
using System.Collections.Generic;
using Database_Test;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SWEN_344_Bookstore.Tests.Database {
    [TestClass]
    public class SQLiteDBTest {
        private SQLite_Database localDB = SQLite_Database.GetInstance();

        [TestMethod]
        public void CreateInventoryBook() {
            //            var amountOfBooksBefore = localDB.GetInventoryBooks().Count;
            var quantity = 0;
            var enabled = false;
            var reviewid = 1;

            //            localDB.InsertInventoryBook(quantity, enabled, reviewid); //int quantity, Boolean enabled, int reviewid
            //            var books = localDB.GetInventoryBooks();
            //            var amountOfBooksAfter = books.Count;
            //            var insertedBook = books[books.Count - 1];
            //
            //            Assert.AreEqual(amountOfBooksBefore, amountOfBooksAfter - 1);
            //            Assert.AreEqual(quantity, insertedBook.quantity);
            //            Assert.AreEqual(enabled, insertedBook.enabled);
            //            Assert.AreEqual(reviewid, insertedBook.reviewid);
            Assert.Fail();
        }

        [TestMethod]
        public void CreateReview () {
            //            var amountOfReviewsBefore = localDB.GetReviews().Count;
            var InvBookID = 1;
            var userid = 1;
            var review = "this is my test review";

            //            localDB.InsertReview(InvBookID, userid, review); //int InvBookID, int userid, String review
            //            var reviews = localDB.GetReviews();
            //            var amountOfReviewsAfter = reviews.Count;
            //            var insertedReview = reviews[reviews.Count - 1];
            //
            //            Assert.AreEqual(amountOfReviewsBefore, amountOfReviewsAfter - 1);
            //            Assert.AreEqual(InvBookID, insertedReview.InvBookID);
            //            Assert.AreEqual(userid, insertedReview.userid);
            //            Assert.AreEqual(review, insertedReview.review);
            Assert.Fail();
        }

        [TestMethod]
        public void GetInventoryBook () {
            
            Assert.Fail();
        }

        [TestMethod]
        public void GetReviews () {

            Assert.Fail();
        }
    }
}
