using System.Web.Optimization;

namespace Web
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/datatable").Include(
                "~/Scripts/DataTables/jquery.dataTables.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/knockout").Include(
                "~/Scripts/knockout-{version}.js",
                "~/Scripts/knockout.mapping-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqwidgets").Include(
                "~/Scripts/jqwidgets/jqxcore.js",
                "~/Scripts/jqwidgets/jqxtabs.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/globalization").Include(
                "~/Scripts/globalization/jquery.global.js",
                "~/Scripts/globalization/jquery.glob.es-PE.js"));

            bundles.Add(new ScriptBundle("~/Content/datatable/css").Include(
               "~/Content/DataTables/css/jquery.dataTables.min.css"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/jqwidgets/styles/jqx").Include(
                "~/Content/jqwidgets/styles/jqx.base.css",
                "~/Content/jqwidgets/styles/jqx.darkblue.css"));
        }
    }
}