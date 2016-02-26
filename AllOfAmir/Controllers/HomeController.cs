using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AllOfAmir.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Personal()
        {
            return View();
        }

        public ActionResult Professional()
        {
            return View();
        }
    }
}