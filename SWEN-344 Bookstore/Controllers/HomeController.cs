﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SWEN_344_Bookstore.Database;
using SWEN_344_Bookstore.Models;
using Database_Test;

namespace SWEN_344_Bookstore.Controllers {
    public class HomeController : Controller {
        public ActionResult Index() {
            return View();
        }

        public ActionResult About() {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact() {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Catalog()
        {
            RestAccess ra = RestAccess.GetInstance();
            SQLite_Database sd = SQLite_Database.GetInstance();
            List<InventoryBook> IBooks = sd.GetInventoryBooks();
            List<Book> books = new List<Book>();
            for (int i = 0; i < IBooks.Count; i++)
            {
            }

            List<List<String>> bookInfo = new List<List<String>>();
            for (int i = 0; i < books.Count; i++)
            {
                bookInfo.Add(new List<String>());
                bookInfo[i].Add(books[i].Name);
                bookInfo[i].Add(books[i].Author);
                bookInfo[i].Add(books[i].desc);
                bookInfo[i].Add("$" + books[i].Price.ToString());
                bookInfo[i].Add(books[i].BookId.ToString());
            }
            ViewData["bookInfo"] = bookInfo;
            return View();
        }
            

        public ActionResult Messages()
        {
            return View();
        }
        public ActionResult ShoppingCart()
        {
            RestAccess ra = RestAccess.GetInstance();
            IList<Book> books = ra.GetBooks();
            List<List<String>> bookInfo = new List<List<String>>();
            for (int i = 0; i < books.Count; i++)
            {
                bookInfo.Add(new List<String>());
                bookInfo[i].Add(books[i].Name);
                bookInfo[i].Add(books[i].Author);
                bookInfo[i].Add(books[i].desc);
                bookInfo[i].Add("$" + books[i].Price.ToString());
            }
            ViewData["bookInfo"] = bookInfo;
            return View();
        }
    }
}