using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SWEN_344_Bookstore;
using SWEN_344_Bookstore.Controllers;
using SWEN_344_Bookstore.Models;
using SWEN_344_Bookstore.Database;
using System.Threading.Tasks;


namespace SWEN_344_Bookstore.Tests.Database
{
    [TestClass]
    public class RestAccessTest
    {
        [TestMethod]
        public void CreateBook()
        {
            RestAccess db = RestAccess.GetInstance();
            
            db.CreateBook("name", 999, "name","desc");
            var list = db.GetBooks();
            var book = list[list.Count - 1];
            db.DeleteLastBook();

            Assert.AreEqual(book.Author,"name");
        }

        [TestMethod]
        public void UpdateBook()
        {
            RestAccess db = RestAccess.GetInstance();
            var list = db.GetBooks();
            var id = list[0].BookId;
            var auth = list[0].Author;
            var price = list[0].Price;
            var name = list[0].Name;
            var desc = list[0].desc;

            db.UpdateBook(id, "rr", 11037, "44","desc");
            
            var book = db.GetBook(id);
            db.UpdateBook(id, auth, price, name, desc);

            Assert.AreEqual(book.Author, "rr");
        }
    }
}
