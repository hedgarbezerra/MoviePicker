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

            bundles.Add(new ScriptBundle("~/bundles/index").Include(
                      "~/Content/js/index.js"));

            bundles.Add(new ScriptBundle("~/bundles/nowuikit").Include(
                      "~/Scripts/BootstrapUiKit/popper.min.js",
                      "~/Scripts/BootstrapUiKit/bootstrap.min.js",
                      "~/Scripts/BootstrapUiKit/now-ui-kit.min.js"
                      ));

            bundles.Add(new ScriptBundle("~/bundles/vuecomponents")
                .IncludeDirectory("~/Scripts/Vue/Components", "*.js"));

            bundles.Add(new ScriptBundle("~/bundles/vuefilters")
               .IncludeDirectory("~/Scripts/Vue/Filters", "*.js"));

            bundles.Add(new ScriptBundle("~/bundles/materialdesign").Include(
                   "~/Scripts/popper.min.js",
                   "~/Scripts/bootstrap-material-design.min.js",
                   "~/Scripts/material-kit.min.js"
                   ));

            bundles.Add(new StyleBundle("~/Content/uikit").Include(
                     "~/Content/css/uikit.min.css",
                     "~/Content/css/uikit-rtl.min.css"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/materialdesign").Include(
                     "~/Content/css/material-kit.min.css"));
        }
    }
}
