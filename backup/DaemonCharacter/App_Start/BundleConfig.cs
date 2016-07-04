using System.Web;
using System.Web.Optimization;

namespace DaemonCharacter
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));
            
            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/javascripts").Include(
                        "~/Scripts/Campaign.js",
                        "~/Scripts/CharacterForms.js",
                        "~/Scripts/ItemForm.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/site.css"
                , "~/Content/DynamicForm.css"
                , "~/Content/1-col-portfolio.css"));

            bundles.Add(new StyleBundle("~/Content/fonts").Include("~/fonts/font-awesome.min.css"));


        }
    }
}