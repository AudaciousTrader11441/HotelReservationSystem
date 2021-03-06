﻿using System.Web;
using System.Web.Optimization;

namespace HotelReservationSystem
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            //bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
            //         "~/Scripts/material/js/bootstrap.min.js", "~/Scripts/material/js/jquery-3.2.1.min.js", "~/Scripts/material/js/mdb.js", "~/Scripts/material/js/popper.min.js",
            //          "~/Scripts/respond.js"));


            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css", "~/Content/Signup.css",
                      "~/Content/site.css"));

            //bundles.Add(new StyleBundle("~/Content/css").Include(
            //          "~/Scripts/material/css/bootstrap.min.css", "~/Scripts/material/css/mdb.min.css",
            //          "~/Scripts/material/css/style.css", 
            //          "~/Content/Signup.css",
            //          "~/Content/site.css"));

        }
    }
}
