using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMSThongKe.Controllers
{
    public class UserController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string username, string password)
        {
            if(username.Equals("admin") && password.Equals("admin@123#"))
            {
                Session["User"] = "admin";
            }

            return RedirectToAction("Login");
        }

    }
}