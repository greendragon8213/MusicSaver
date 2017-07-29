using System.Web.Optimization;

namespace MusicDownloader
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/utils-js").Include(
                "~/Scripts/modernizr-2.6.2.js",
                "~/Scripts/jquery-1.10.2.min.js",
                "~/Scripts/jquery.localize.min.js",
                "~/Scripts/bootstrap.min.js",
                "~/Scripts/jquery.blockUI.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/app-js").Include(
                "~/Scripts/app/constants.js",
                "~/Scripts/app/main.js",
                "~/Scripts/app/contact-me-form.js",
                "~/Scripts/app/index.js"
                ));

            bundles.Add(new LessBundle("~/bundles/app-less").Include(
                "~/Content/app/shared.less",
                "~/Content/app/site.less",
                "~/Content/app/index.less",
                "~/Content/app/how-it-works.less",
                "~/Content/app/faq.less",
                "~/Content/app/about.less"
                ));

            //bundles.Add(new StyleBundle("~/bundles/app-css").Include(
            //    "~/Content/app/site.css",
            //    "~/Content/app/index.css",
            //    "~/Content/app/how-it-works.css",
            //    "~/Content/app/faq.css",
            //    "~/Content/app/about.css"
            //    ));

            BundleTable.EnableOptimizations = true;
        }
    }
}