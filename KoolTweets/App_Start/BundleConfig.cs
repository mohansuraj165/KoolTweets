using System.Web;
using System.Web.Optimization;

namespace KoolTweets
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery-{version}.min.js",
                        "~/Scripts/jquery-{version}.ui.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/parsley.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqwidgets").Include(
                        "~/Scripts/JQWidgets/jqxcore.js",
                        "~/Scripts/JQWidgets/jqxdatetimeinput.js",
                        "~/Scripts/JQWidgets/jqxcalendar.js",
                        "~/Scripts/JQWidgets/jqxtooltip.js",
                        "~/Scripts/JQWidgets/globalize.js"));

            bundles.Add(new StyleBundle("~/jqwidgets/css").Include(
                        "~/Content/jqx.base.css",
                         "~/Content/jqx.energyblue.css"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/bundles/tweet").Include(
                      "~/Scripts/tweets.js"));
        }
    }
}
