using System.Web.Optimization;
namespace CloudBasedRMS.View
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/assets/js/jquery-{version}.js"));
            bundles.Add(new ScriptBundle("~/js/required").Include(
                                     "~/assets/js/jquery-1.12.4.min.js"));
            // Code removed for clarity.
            BundleTable.EnableOptimizations = true;
            bundles.Add(new StyleBundle("~/Content/jqueryui")
                             .Include("~/Content/themes/base/all.css"));
            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/assets/js/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/assets/js/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/assets/js/bootstrap.js",
                      "~/assets/js/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
            bundles.Add(new ScriptBundle("~/bundles/jqueryui")
                                .Include("~/assets/js/jquery-ui-{version}.js"));
            bundles.Add(new StyleBundle("~/Content/jqueryui")
                                .Include("~/Content/themes/base/all.css"));
            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                       "~/Scripts/angular.js",
                       "~/Scripts/angular-route.js"));
        }
    }
}
