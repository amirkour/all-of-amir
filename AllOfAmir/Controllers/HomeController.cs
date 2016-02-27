using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using log4net;
using log4net.Config;

namespace AllOfAmir.Controllers
{
    public class HomeController : Controller
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(HomeController));

        public ActionResult Index()
        {
            // prints to console by default w/ a newline, but no timestamps included
            System.Diagnostics.Debug.Print("Hi world debug via Print");

            // same as "Debug" api, but w/ timestamps
            System.Diagnostics.Trace.TraceInformation("Hi world info");
            System.Diagnostics.Trace.TraceWarning("Hi world warn");
            System.Diagnostics.Trace.TraceError("Hi world error");

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