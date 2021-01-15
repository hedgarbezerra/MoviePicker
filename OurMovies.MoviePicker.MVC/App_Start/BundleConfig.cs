using System.Web;
using System.Web.Optimization;

namespace OurMovies.MoviePicker.MVC
{
    public class BundleConfig
    {        
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/vue").Include(
                      "~/Scripts/vue.min.js",
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
                    "~/Scripts/Vue/Components/recuperarSenha.js",
                    "~/Content/js/login.js"));

             bundles.Add(new ScriptBundle("~/bundles/alterarsenha").Include(
                    "~/Content/js/alterarSenha.js"));

             bundles.Add(new ScriptBundle("~/bundles/aleatorio").Include(
                    "~/Content/js/aleatorios.js"));

             bundles.Add(new ScriptBundle("~/bundles/adicionarfilmes").Include(
                    "~/Content/js/adicionarFilmes.js"));

             bundles.Add(new ScriptBundle("~/bundles/adicionarcategorias").Include(
                    "~/Content/js/adicionarCategorias.js"));

            bundles.Add(new ScriptBundle("~/bundles/main").Include(
                      "~/Content/js/main.js"));

            bundles.Add(new ScriptBundle("~/bundles/index").Include(
                      "~/Content/js/index.js"));

            bundles.Add(new ScriptBundle("~/bundles/vuecomponents")
                .IncludeDirectory("~/Scripts/Vue/Components", "*.js"));

            bundles.Add(new ScriptBundle("~/bundles/vuefilters")
               .IncludeDirectory("~/Scripts/Vue/Filters", "*.js"));

            bundles.Add(new ScriptBundle("~/bundles/materialdesign").Include(
                   "~/Scripts/popper.min.js",
                   "~/Scripts/bootstrap-material-design.min.js",
                   "~/Scripts/material-kit.min.js"
                   ));


            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/Site.css"));

            bundles.Add(new StyleBundle("~/Content/animatecss").Include(
                     "~/Content/css/animate.css"));

             bundles.Add(new StyleBundle("~/Content/fontawesome").Include(
                     "~/Content/css/all.css"));

            bundles.Add(new StyleBundle("~/Content/materialdesign").Include(
                     "~/Content/css/material-kit.min.css"));
        }
    }
}
