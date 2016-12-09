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

        private void CommonData()
        {
            User user = getCurrentUser();
            ViewData["lgnusr"] = user;
            if (getCurrentUser().GetUid() == -99)
            {
                ViewData["usertype"] = "unknown";
            }
            else
            {
                ViewData["usertype"] = user.getUserType();
            }
        }

        // GET: Admin
        public ActionResult Index()
        {
            CommonData();
            if (isAdmin())
            {
                return View();
            }
            return RedirectToAction("NotAdmin", "Admin");
        }

        public ActionResult CreateBook()
        {
            CommonData();
            if (isAdmin())
            {
                return View();
            }
            return RedirectToAction("NotAdmin", "Admin");
        }

        public ActionResult EditBook(string id = null) {
            CommonData();
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
            CommonData();
            HttpCookie MyCookie = new HttpCookie("UserType");
            MyCookie.Value = "admin";
            Response.Cookies.Add(MyCookie);
            return View();
        }

        public ActionResult NotAdmin()
        {
            CommonData();
            return View();
        }
    }
}