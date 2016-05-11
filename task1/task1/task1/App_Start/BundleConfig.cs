using System.Web;
using System.Web.Optimization;

namespace task1 {
  public class BundleConfig {
    public static void RegisterBundles(BundleCollection bundles) {
      bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                  "~/Scripts/jquery-{version}.js"));

      bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                  "~/Scripts/jquery-ui-{version}.js"));

      bundles.Add(new ScriptBundle("~/bundles/highcharts").Include(
                  "~/Scripts/Highcharts-3.0.1/js/highcharts.js"));

      bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/Site.css"));

      bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
        "~/Content/themes/base/jquery.ui.core.css",
         "~/Content/themes/base/jquery.ui.datepicker.css",
         "~/Content/themes/base/jquery.ui.theme.css"));

      BundleTable.EnableOptimizations = true;
    }
  }
}