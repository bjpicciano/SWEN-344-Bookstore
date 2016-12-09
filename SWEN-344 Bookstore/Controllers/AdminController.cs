using SWEN_344_Bookstore.Database;
using SWEN_344_Bookstore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SWEN_344_Bookstore.Controllers
{
    public class AdminController : Controller
    {
        private Boolean isAdmin()
        {
            HttpCookie cookie = Request.Cookies["UserType"];
            return cookie != null && cookie.Value != null && cookie.Value.Equals("admin");
        }

        private User getCurrentUser()
        {
            if (Request.Cookies["LoginEmail"] != null)
            {
                String value = Request.Cookies["LoginEmail"].Value;
                return RestAccess.GetInstance().GetUserByEmail(value);
            }
            return new User(-99, "dummy", "dummy", "dummy", "dummy");
        }

        // GET: Admin
        public ActionResult Index()
        {
            ViewData["lgnusr"] = getCurrentUser();
            if (isAdmin())
            {
                return View();
            }
            return RedirectToAction("NotAdmin", "Admin");
        }

        public ActionResult CreateBook()
        {
            ViewData["lgnusr"] = getCurrentUser();
            if (isAdmin())
            {
                return View();
            }
            return RedirectToAction("NotAdmin", "Admin");
        }

        public ActionResult EditBook(string id = null) {
            ViewData["lgnusr"] = getCurrentUser();
            if (isAdmin())
            {
                if (id != null)
                {
                    ViewData["bookID"] = int.Parse(id);
                    return View();
                }
            }
            return RedirectToAction("NotAdmin", "Admin");
        }

        public ActionResult MakeMeAdmin()
        {
            ViewData["lgnusr"] = getCurrentUser();
            HttpCookie MyCookie = new HttpCookie("UserType");
            MyCookie.Value = "admin";
            Response.Cookies.Add(MyCookie);
            return View();
        }

        public ActionResult NotAdmin()
        {
            ViewData["lgnusr"] = getCurrentUser();
            return View();
        }
    }
}