using System.Web;
using System.Web.Optimization;

namespace OurMovies.MoviePicker.MVC
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/vue").Include(
                      "~/Scripts/vue.js"));

            bundles.Add(new ScriptBundle("~/bundles/vuetify").Include(
                      "~/Scripts/vuetify.js"));

            bundles.Add(new ScriptBundle("~/bundles/veevalidate").Include(
                      "~/Scripts/vee-validate.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/vuetifycss").Include(
                     @"~/Scripts/vuetify.min.css"));
        }
    }
}
