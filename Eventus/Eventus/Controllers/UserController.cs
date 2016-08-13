using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Eventus.Controllers
{
    public class UserController : Controller
    {
        //
        // GET: /User/Login

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult SubmitLogin()
        {
            return View();
        }
         
        //
        // GET: /User/Login

        public ActionResult Register()
        {
            return View();
        }

        public ActionResult SubmitRegister()
        {
            return View();
        }
    }
}