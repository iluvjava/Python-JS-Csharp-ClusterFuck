using System.Web;
using System.Web.Optimization;

/// <summary>
/// 
/// </summary>
namespace MyWebAppAttempt
{
    /// <summary>
    /// This is related to @scripts.Render("~/???") on the cshtml
    /// 
    /// This where all the scrips tags are bundled. 
    ///     - bundling: puts all the sctipts in one place, this works but this is the microsoft 
    ///     way of doing things. 
    /// 
    /// </summary>
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));
                                            //^
                                            //This is pretty sick. 

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));


            //Add you own bundles here, or read given for more stuff. 
        }
    }
}
