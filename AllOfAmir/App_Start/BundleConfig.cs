using System.Web;
using System.Web.Optimization;

namespace AllOfAmir
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/scripts")
                   .Include("~/Scripts/jquery-{version}.js")
                   .Include("~/Scripts/bootstrap.js"));
            
            bundles.Add(new StyleBundle("~/bundles/styles")
                   .Include("~/Content/bootstrap.css")
                   .Include("~/Content/site.css"));
        }
    }
}
