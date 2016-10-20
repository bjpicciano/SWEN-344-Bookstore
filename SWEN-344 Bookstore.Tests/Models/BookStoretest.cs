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
using System.Threading.Tasks;

namespace SWEN_344_Bookstore.Tests.Models
{
    [TestClass]
    public class BookStoreTest
    {
        [TestMethod]
        public void AddToInventory()
        {
            Book book = new Book();
            Bookstore store = new Bookstore();
            store.GetInventory();

            store.AddToInventory(book);
            Assert.AreEqual(1, store.GetInventory().Count);
        }
    }
}
