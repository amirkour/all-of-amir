using System;
using System.Text;
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
        /// <summary>
        /// Helper to get custom/pretty exception strings for error reporting
        /// </summary>
        protected string BuildPrettyExceptionString(Exception e)
        {
            StringBuilder bldr = new StringBuilder();
            if (e == null)
                bldr.Append("No exception provided");
            else
            {
                if (e.Message.IsNullOrEmpty())
                    bldr.Append("No exception message provided");
                else
                    bldr.Append(e.Message);

                if (!e.StackTrace.IsNullOrEmpty())
                    bldr.Append(" - " + e.StackTrace);
            }

            return bldr.ToString();
        }

        protected void Info(string msg) { System.Diagnostics.Trace.TraceInformation(msg); }
        protected void Info(string msg, params object[] args) { System.Diagnostics.Trace.TraceInformation(msg, args); }
        protected void Warn(string msg) { System.Diagnostics.Trace.TraceWarning(msg); }
        protected void Warn(string msg, params object[] args) { System.Diagnostics.Trace.TraceWarning(msg, args); }
        protected void Warn(string msg, Exception e)
        {
            StringBuilder bldr = new StringBuilder();
            if (msg.IsNullOrEmpty())
                bldr.Append("No warning message provided");
            else
                bldr.Append(msg);

            string prettyException = BuildPrettyExceptionString(e);
            if (prettyException.IsNullOrEmpty())
                bldr.Append(" - no exception provided");
            else
                bldr.Append(" - " + prettyException);

            System.Diagnostics.Trace.TraceWarning(bldr.ToString());
        }

        protected void Error(string msg) { System.Diagnostics.Trace.TraceError(msg); }
        protected void Error(string msg, object[] args) { System.Diagnostics.Trace.TraceError(msg, args); }
        protected void Error(string msg, Exception e)
        {
            StringBuilder bldr = new StringBuilder();
            if (msg.IsNullOrEmpty())
                bldr.Append("No error message provided");
            else
                bldr.Append(msg);

            string prettyException = BuildPrettyExceptionString(e);
            if (prettyException.IsNullOrEmpty())
                bldr.Append(" - no exception provided");
            else
                bldr.Append(" - " + prettyException);

            System.Diagnostics.Trace.TraceError(bldr.ToString());
        }

        /// <summary>
        /// Helper that will stringify and return the given JsonResult object (which you 
        /// can generate via this.Json), or an empty json object if the given json or it's
        /// data are null.
        /// </summary>
        protected string Stringify(JsonResult json)
        {
            return json == null || json.Data == null ? "{}" : Newtonsoft.Json.JsonConvert.SerializeObject(json.Data);
        }

        /// <summary>
        /// Our custom exception handler will attempt to handle/standardize errors that
        /// are generated for ajax requests that accept JSON.  Otherwise, it'll kick handling
        /// back to .NET.
        /// 
        /// For requests that accept JSON, we'll kick back something like this:
        /// { Error: "some useful error message" }
        /// </summary>
        protected override void OnException(ExceptionContext filterContext)
        {
            //if (filterContext.ExceptionHandled)
            //{
            //    Warn("We almost handled an exception, but it looks like someone else already got to it!?  Passing it by", filterContext.Exception);
            //    return;
            //}

            // this method had better not throw ANOTHER exception ... 
            // ... if it does, we gotta handle it
            try
            {
                if (Request.IsAjaxRequest() && Request.AcceptsJSON())
                {
                    string msg = filterContext.Exception != null && !filterContext.Exception.Message.IsNullOrEmpty() ?
                        filterContext.Exception.Message :
                        "An unspecified error was encountered";

                    Response.Clear();
                    Response.StatusCode = 500;
                    Response.TrySkipIisCustomErrors = true;
                    filterContext.Result = this.Json(new { Error = msg });
                    filterContext.ExceptionHandled = true;

                    // DO I NEED THESE? I DON'T THINK SO ...
                    //filterContext.HttpContext.Response.StatusCode = 500;
                    //filterContext.HttpContext.Response.Clear();
                    //filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;

                    Error("We handled the following exception", filterContext.Exception);
                    return;
                }
            }
            catch (Exception e)
            {
                Error("Encountered the following exception in BaseException.OnException", e);
            }
            
            Error("We tried and failed to handle the following exception, so we're gonna let .NET handle it for us", filterContext.Exception);
            // TODO - maybe show a nice error page in this case?

            base.OnException(filterContext);
        }
    }
}