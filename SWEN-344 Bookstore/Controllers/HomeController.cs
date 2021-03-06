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
        public ActionResult CreateUserInDB() {
            CommonData();

            return View();
        }

        private User getCurrentUser()
        {
            if (Request.Cookies["LoginEmail"] != null)
            {
                String value = Request.Cookies["LoginEmail"].Value;
                ViewData["LoginEmail"] = value;
                return RestAccess.GetInstance().GetUserByEmail(value);
            }
            return new User(-99,"dummy","Please","Login", "dummy");
        }

        private Boolean CommonData()
        {
            User user = getCurrentUser();
            ViewData["lgnusr"] = user;
            String usertype = "unknown";
            if (Request.Cookies["UserType"] != null)
            {
                usertype = Request.Cookies["UserType"].Value;
            }
            ViewData["usertype"] = usertype;
            
            return (usertype.Equals("unknown"));
        }
        
        public ActionResult Index() {
            if (CommonData())
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }

        public ActionResult About() {
            CommonData();
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact() {
            if (CommonData())
            {
                return RedirectToAction("Login", "Account");
            }
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult AddToCart(int addid = -1)
        {
            if (CommonData())
            {
                return RedirectToAction("Login","Account");
            }
            if (addid != -1)
            {
                SQLite_Database.GetInstance().AddToShoppingCart(addid, Request.Cookies["LoginEmail"].Value);
            }
            return RedirectToAction("Catalog", "Home");
        }

        public ActionResult Catalog(String showid = null)
        {
            CommonData();
            RestAccess ra = RestAccess.GetInstance();
            SQLite_Database sd = SQLite_Database.GetInstance();
            List<InventoryBook> IBooks = sd.GetInventoryBooks();
            List<String> showline = null;
            int sbid = -99;
            ViewData["showmodal"] = false;
            if (showid != null)
            {
                sbid = Convert.ToInt32(showid);
                ViewData["showmodal"] = true;
            }

            List<List<String>> bookInfo = new List<List<String>>();
            for (int i = 0; i < IBooks.Count; i++)
            {
                //System.Diagnostics.Debug.Print(IBooks[i].GetBook().ToString());
                Book b = ra.GetBook(IBooks[i].GetBook());
                bookInfo.Add(new List<String>());
                bookInfo[i].Add(b.Name);
                bookInfo[i].Add(b.Author);
                bookInfo[i].Add(b.desc);
                bookInfo[i].Add("$" + b.Price.ToString());
                bookInfo[i].Add(b.BookId.ToString());
                bookInfo[i].Add(IBooks[i].GetStock().ToString());
                bookInfo[i].Add(IBooks[i].IsEnabled.ToString());
                if(b.BookId == sbid)
                {
                    showline = bookInfo[i];
                    ViewData["reviews"] = IBooks[i].reviews;
                }
            }
            ViewData["bookInfo"] = bookInfo;
            ViewData["showline"] = null;
            if(showid != null)
            {   
                if(showline == null)
                {
                    ViewData["showline"] = new List<String>() { "dummy", "dummy", "dummy", "dummy", "dummy", "dummy" };
                    ViewData["reviews"] = new List<Review>();
                }
                else
                {
                    ViewData["showline"] = showline;
                }
                
            }

            ViewData["convert"] = RestAccess.GetInstance().CurrRates;

            return View();
        }
            

        public ActionResult Messages()
        {
            if (CommonData())
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }

        public ActionResult ShoppingCart()
        {
            if (CommonData())
            {
                return RedirectToAction("Login", "Account");
            }
            ViewData["lgnusr"] = getCurrentUser();
            RestAccess ra = RestAccess.GetInstance();
            SQLite_Database sd = SQLite_Database.GetInstance();
            List<InventoryBook> IBooks = sd.GetInventoryBooks();
            List<Book> books = new List<Book>();
            for (int i = 0; i < IBooks.Count; i++)
            {
                books.Add(ra.GetBook(IBooks[i].GetBook()));
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
                bookInfo[i].Add(IBooks[i].GetStock().ToString());
            }
            ViewData["bookInfo"] = bookInfo;
            for (int i = 0; i < IBooks.Count; i++)
            {
                ViewData[IBooks[i].GetBook() + "reviews"] = IBooks[i].reviews;
            }
            return View();
        }

        public ActionResult Logout()
        {
            if (Request.Cookies["LoginEmail"] != null)
            {
                Response.Cookies["LoginEmail"].Expires = DateTime.Now.AddDays(-1);
            }
            if (Request.Cookies["UserType"] != null)
            {
                Response.Cookies["UserType"].Expires = DateTime.Now.AddDays(-1);
            }
            return RedirectToAction("Login", "Account");
        }


        public ActionResult Reciepts()
        {
            {
                if (CommonData())
                {
                    return RedirectToAction("Login", "Account");
                }
                ViewData["lgnusr"] = getCurrentUser();
                RestAccess ra = RestAccess.GetInstance();
                SQLite_Database sd = SQLite_Database.GetInstance();
                List<InventoryBook> IBooks = sd.GetInventoryBooks();
                List<Book> books = new List<Book>();
                for (int i = 0; i < IBooks.Count; i++)
                {
                    //                books.Add(ra.GetBook(IBooks[i].GetBook()));
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
        }

    }
}