using System.Web;
using System.Web.Optimization;

namespace DACN
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/js").Include(
                        "~/assets/js/jquery-min.js",
                        "~/assets/js/jquery-min2.js",
                        "~/assets/js/bootstrap.min.js",
                        "~/assets/js/material.min.js",
                        "~/assets/js/material-kit.js",
                        "~/assets/js/jquery.parallax.js",
                        "~/assets/js/owl.carousel.min.js",
                        "~/assets/js/jquery.slicknav.js",
                        "~/assets/js/main.js",
                        "~/assets/js/jquery.counterup.min.js",
                        "~/assets/js/waypoints.min.js",
                        "~/assets/js/jasny-bootstrap.min.js",
                        "~/assets/js/bootstrap-select.min.js",
                        "~/assets/js/form-validator.min.js",
                        "~/assets/js/contact-form-script.js",
                        "~/assets/js/jquery.themepunch.revolution.min.js",
                        "~/assets/js/jquery.themepunch.tools.min.js",
                        "~/assets/plusins/lightslider-master/lightslider-master/src/js/lightslider.js"

                       ));
            bundles.Add(new ScriptBundle("~/bundles/jsconection").Include(
                        "~/Scripts/jquery.signalR-2.4.0.min.js",
                        "~/signalr/hubs"
                      ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/bundles/core").Include(
                      "~/assets/css/bootstrap.min.css",
                      "~/assets/css/bootstrap-select.min.css",
                      "~/assets/css/material-kit.css",
                      "~/assets/extras/animate.css",
                      "~/assets/extras/owl.carousel.css",
                      "~/assets/extras/owl.theme.css",
                      "~/assets/extras/settings.css",
                      "~/assets/css/slicknav.css",
                      "~/assets/css/main.css",
                      "~/assets/css/responsive.css",
                      "~/assets/css/colors/red.css",
                      "~/assets/plusins/lightslider-master/lightslider-master/src/css/lightslider.css",
                      "~/assets/css/jasny-bootstrap.min.css"));
            BundleTable.EnableOptimizations = true;

        }
    }
}
