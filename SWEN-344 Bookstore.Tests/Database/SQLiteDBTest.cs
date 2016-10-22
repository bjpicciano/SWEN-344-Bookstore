using System;
using Database_Test;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SWEN_344_Bookstore.Tests.Database {
    [TestClass]
    public class SQLiteDBTest {
        private SQLite_Database localDB = SQLite_Database.GetInstance();

        [TestMethod]
        public void CreateInventoryBook() {
            localDB.InsertInventoryBook(0, false, 1);

//            var books = localDB.GetInventoryBooks();

            Assert.Fail();
        }

        [TestMethod]
        public void CreateReview () {

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
