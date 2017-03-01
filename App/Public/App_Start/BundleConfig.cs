using System.Web;
using System.Web.Optimization;

namespace Public
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/modernizr.custom.26633.js",
                      "~/Scripts/jquery-1.11.2.min.js",
                      "~/Scripts/bootstrap.min.js",
                      "~/Scripts/jquery.placeholder.js",
                      "~/Scripts/stickUp.js",
                      "~/Scripts/jquery.superslides.js",
                      "~/Scripts/jquery.isotope.js",
                      "~/Scripts/jquery.ui.widget.min.js",
                      "~/Scripts/jquery.ui.rlightbox.js",
                      "~/Scripts/jquery.prettyPhoto.js",
                      "~/Scripts/jquery.contact.js",
                      "~/Scripts/jquery.subscribe.js",
                      "~/Scripts/jquery.classyloader.min.js",
                      "~/Scripts/jquery.flexslider.js",
                      "~/Scripts/jquery.easing.js",
                      "~/Scripts/jquery.mousewheel.js",
                      "~/Scripts/slideroption.js",
                      "~/Scripts/jquery.countTo.js",
                      "~/Scripts/jquery.cookie.js",
                      "~/Scripts/styleswitch.js",
                      "~/Scripts/custom.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.min.css",
                      "~/Content/main.css",
                      "~/Content/default.css",
                      "~/Content/switcher.css",
                      "~/Content/font-awesome.min.css",
                      "~/Content/jquery-ui-1.8.16.custom.css",
                      "~/Content/lightbox.min.css",
                      "~/Content/prettyPhoto.css"
                      //,"~/Content/site.css"
                      ));
        }
    }
}
