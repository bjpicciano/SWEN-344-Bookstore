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
    public class BookTest
    {
        [TestMethod]
        public void AddToStock()
        {
            Book book = new Book();
            InventoryBook ib = new InventoryBook();

            ib.AddToStock(5);
            Assert.AreEqual(5, ib.GetStock());
        }

        [TestMethod]
        public void RemoveFromStockStockToStock()
        {
            Book book = new Book();
            InventoryBook ib = new InventoryBook();

            ib.AddToStock(5);
            ib.RemoveFromStockStock(2);
            Assert.AreEqual(3, ib.GetStock());
        }
    }
}
