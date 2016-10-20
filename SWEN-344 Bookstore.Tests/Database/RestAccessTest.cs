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
           
            //db.CreateBook("name", 999, "name");
            var list = db.GetBooks();

            //Assert.AreEqual(list[list.Count-1].Author,"name");
        }

        [TestMethod]
        public void UpdateBook()
        {
            RestAccess db = RestAccess.GetInstance();

            db.UpdateBook(42, "rr", 11037, "44");
            var list = db.GetBooks();
            Assert.AreEqual(list[41].Author, "rr");
        }
    }
}
