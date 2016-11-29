using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SWEN_344_Bookstore.Database;
using SWEN_344_Bookstore.Models;

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
            IList<Book> books = ra.GetBooks();
            List<List<Book>> bookTable = new List<List<Book>>();
            for(int r = 0; r < 5; r++)
            {
                bookTable.Add(new List<Book>());
                if (books.Count > r * 5)
                {
                    for (int c = 0; c < 5; c++)
                    {
                        if(books.Count > (r * 5) + c)
                        {
                            bookTable[r].Add(books.ElementAt((r * 5) + c));
                        }
                    }
                }
            }
            ViewData["books"] = bookTable;
            return View();
        }

        public ActionResult Messages()
        {
            return View();
        }
        public ActionResult ShoppingCart()
        {
            return View();
        }
    }
}