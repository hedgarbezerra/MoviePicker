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
                      "~/Scripts/vue.js",
                      "~/Scripts/vee-validate-locale-pt_Br.js",
                      "~/Scripts/vee-validate.full.min.js",
                      "~/Scripts/axios.min.js",
                      "~/Scripts/vue-spinners.umd.min.js",
                      "~/Scripts/vue-infinite-loading.js",
                      "~/Scripts/vue-toasted.min.js"
                      ));

            bundles.Add(new ScriptBundle("~/bundles/moment").Include(
                      "~/Scripts/moment-with-locales.js"));

            bundles.Add(new ScriptBundle("~/bundles/cadastro").Include(
                      "~/Content/js/cadastro.js"));

            bundles.Add(new ScriptBundle("~/bundles/login").Include(
                    "~/Content/js/login.js"));

            bundles.Add(new ScriptBundle("~/bundles/main").Include(
                      "~/Content/js/main.js"));

            bundles.Add(new ScriptBundle("~/bundles/nowuikit").Include(
                      "~/Scripts/BootstrapUiKit/popper.min.js",
                      "~/Scripts/BootstrapUiKit/bootstrap.min.js",
                      "~/Scripts/BootstrapUiKit/now-ui-kit.min.js"
                      ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/nowuikit").Include(
                     "~/Content/css/bootstrap.min.css",
                     "~/Content/css/now-ui-kit.min.css"));
        }
    }
}
