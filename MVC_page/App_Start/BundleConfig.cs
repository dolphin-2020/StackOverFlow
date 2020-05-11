using System.Web;
using System.Web.Optimization;

namespace MVC_page
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/js/layui").Include(
                        "~/Content/layui/layui.js"));
 

            bundles.Add(new StyleBundle("~/css/layui").Include(
                      "~/Content/layui/css/layui.css"));
        }
    }
}
