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
                        "~/Scripts/jquery-ui.min.js",
                        "~/Scripts/jquery.unobtrusive*"
                        ));


            bundles.Add(new ScriptBundle("~/bundles/dataTables").Include(
                        "~/Scripts/DataTable/jquery.dataTables.min.js"
                        , "~/Scripts/DataTable/dataTables.tableTools.min.js"
                        , "~/Scripts/DataTable/dataTables.colReorder.min.js"
                        , "~/Scripts/DataTable/dataTables.scroller.min.js"
                        , "~/Scripts/DataTable/dataTables.bootstrap.min.js"
                        , "~/Scripts/DataTable/select2.full.min.js"
                        ));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/javascripts").Include(
                         "~/Scripts/Util.js"
                         , "~/Scripts/scripts.js"
                         , "~/Scripts/smoothscroll.js"
                         , "~/Scripts/bootstrap.min.js"
                         , "~/Scripts/SmartNotification.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/css").Include(
                         "~/Content/bootstrap.min.css"
                         , "~/Content/Site.css"
                         , "~/Content/essentials.css"
                         , "~/Content/layout.css"
                         , "~/Content/ItemLayouts/layout-datatables.css"
                         , "~/Content/ColorScheme/darkblue.css"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/playerScripts").Include(
                "~/Scripts/bootstrap-markdown.min.js"
                , "~/Scripts/custom.fle_upload.js"
                , "~/Scripts/MultipleStepFormScript.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/playerCss").Include(
                "~/Content/bootstrap-markdown.min.css"
                ));


            bundles.Add(new ScriptBundle("~/bundles/homeCss").Include(
                         "~/Content/header-1.css"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/Sprite/Attributes").Include(
                "~/Content/Sprites/Atributos.css"
                , "~/Images/Sprites/Atributos.png"
                ));

            bundles.Add(new ScriptBundle("~/bundles/Sprite/Users").Include(
               "~/Content/Sprites/Users.css"
               , "~/Images/Sprites/Users.png"
               ));

            bundles.Add(new ScriptBundle("~/bundles/Sprite/Acoes").Include(
               "~/Content/Sprites/Acoes.css"
               , "~/Images/Sprites/Acoes.png"
               ));

            bundles.Add(new ScriptBundle("~/bundles/Sprite/playersheet").Include(
               "~/Content/Sprites/sprite_playersheet.css"
               , "~/Images/Sprites/sprite_playersheet.png"
               ));


        }
    }
}