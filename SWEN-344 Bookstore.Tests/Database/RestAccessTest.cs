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
            List<Book> list = db.GetBooks();
            Book book = list.Last();
            db.DeleteLastBook();

            Assert.AreEqual(book.Author,"name");
        }

        [TestMethod]
        public void UpdateBook()
        {
            RestAccess db = RestAccess.GetInstance();
            List<Book> list = db.GetBooks();
            int id = list.First().BookId;
            String auth = list.First().Author;
            float price = list.First().Price;
            String name = list.First().Name;
            String desc = list.First().desc;

            db.UpdateBook(id, "rr", 11037, "44","desc");
            
            Book book = db.GetBook(id);
            Assert.AreEqual(book.Author, "rr");
            db.UpdateBook(id, auth, price, name, desc);

            
        }
    }
}
