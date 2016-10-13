using System.Web.Optimization;

namespace DaemonCharacter.UI.MVC
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-2.2.3.min.js"
                        , "~/Scripts/jquery.validate.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui.min.js"));


            bundles.Add(new ScriptBundle("~/bundles/dataTables").Include(
                        "~/Scripts/DataTable/jquery.dataTables.min.js"
                        , "~/Scripts/DataTable/dataTables.tableTools.min.js"
                        , "~/Scripts/DataTable/dataTables.colReorder.min.js"
                        , "~/Scripts/DataTable/dataTables.scroller.min.js"
                        , "~/Scripts/DataTable/dataTables.bootstrap.min.js"
                        , "~/Scripts/DataTable/select2.full.min.js"
                        ));

            //bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
            //            "~/Scripts/jquery.unobtrusive*",
            //            "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/javascripts").Include(
                         "~/Scripts/Util.js"
                         , "~/Scripts/scripts.js"
                         , "~/Scripts/smoothscroll.js"
                         , "~/Scripts/bootstrap.min.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/css").Include(
                         "~/Content/bootstrap.min.css"
                         , "~/Content/Site.css"
                         , "~/Content/essentials.css"
                         , "~/Content/layout.css"
                         , "~/Content/ItemLayouts/layout-datatables.css"
                         , "~/Content/ColorScheme/darkblue.css"
                        ));


            bundles.Add(new ScriptBundle("~/bundles/homeCss").Include(
                         "~/Content/header-1.css"

                        ));

            bundles.Add(new ScriptBundle("~/bundles/Sprite/Attributes").Include(
                "~/Content/Sprites/Atributos.css"
                , "~/Images/Sprites/Atributos.png"
                ));

            bundles.Add(new ScriptBundle("~/bundles/Sprite/Acoes").Include(
               "~/Content/Sprites/Acoes.css"
               , "~/Images/Sprites/Acoes.png"
               ));


        }
    }
}