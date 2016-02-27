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
            logger.Debug("Hi world debug");
            logger.Info("Hi world info");
            logger.Warn("Hi world warn");
            logger.Error("Hi world error");
            logger.Fatal("Hi world fatal");
            System.Diagnostics.Trace.TraceError("If you're seeing this, something bad happened");

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