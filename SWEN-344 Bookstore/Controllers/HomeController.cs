using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SWEN_344_Bookstore.Database;
using SWEN_344_Bookstore.Models;
using Database_Test;

namespace SWEN_344_Bookstore.Controllers {
    public class HomeController : Controller {
        private User getCurrentUser()
        {
            if (Request.Cookies["LoginEmail"] != null)
            {
                String value = Request.Cookies["LoginEmail"].Value;
                return RestAccess.GetInstance().GetUserByEmail(value);
            }
            return new User(-99,"dummy","dummy","dummy", "dummy");
        }

        private void CommonData()
        {
            User user = getCurrentUser();
            ViewData["lgnusr"] = user;
            if(getCurrentUser().GetUid() == -99)
            {
                ViewData["usertype"] = "unknown";
            }
            else
            {
                ViewData["usertype"] = user.getUserType();
            }
        }
        
        public ActionResult Index() {
            CommonData();
            return View();
        }

        public ActionResult About() {
            CommonData();
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact() {
            CommonData();
            ViewBag.Message = "Your contact page.";

            return View();
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
            CommonData();
            return View();
        }
        public ActionResult ShoppingCart()
        {
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


        public ActionResult Reciepts()
        {
            {
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