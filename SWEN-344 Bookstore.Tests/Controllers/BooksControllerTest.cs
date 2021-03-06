﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SWEN_344_Bookstore;
using SWEN_344_Bookstore.Controllers;
using SWEN_344_Bookstore.Models;

namespace SWEN_344_Bookstore.Tests.Controllers
{
    [TestClass]
    public class BooksControllerTest
    {
        /*
         * Test for getting books using controller
         */ 
        [TestMethod]
        public void GetBooks()
        {
            BooksController controller = new BooksController();
            IQueryable result = controller.GetBooks();
            Assert.IsNotNull(result);
        }
        
        //[TestMethod]
        //public void CreateBook()
        //{
        //    BooksController controller = new BooksController();
        //    Book book = new Book();
        //    book.Author = "hi";
        //    controller.PutBook(1, book);

        //    Book b = controller.GetBook(1) as Book;
        //    Assert.AreEqual(b.Author, "hi");
        //}
    }
}
