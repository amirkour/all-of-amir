using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DotNetUtils;

namespace AllOfAmir.Controllers
{
    /// <summary>
    /// Helper/base for all controllers - just houses helper functions.
    /// </summary>
    public abstract class BaseController : Controller
    {
        protected void Info(string msg) { System.Diagnostics.Trace.TraceInformation(msg); }
        protected void Info(string msg, params object[] args) { System.Diagnostics.Trace.TraceInformation(msg, args); }
        protected void Warn(string msg) { System.Diagnostics.Trace.TraceWarning(msg); }
        protected void Warn(string msg, params object[] args) { System.Diagnostics.Trace.TraceWarning(msg, args); }
        protected void Error(string msg) { System.Diagnostics.Trace.TraceError(msg); }
        protected void Error(string msg, object[] args = null) { System.Diagnostics.Trace.TraceError(msg, args); }
    }
}