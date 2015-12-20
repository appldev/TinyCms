using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace TinyCms
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                    name: "Default",
                    url: "Home/Index/{id}",
                    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
                );

            routes.MapRoute(
                name: "CmsCultureRoute",
                url: "{culture}/{*path}",
                defaults: new {
                    culture = "en-US",
                    controller = "VirtualPage",
                    action = "Index" },
                constraints: new
                {
                    culture = "[a-z]{2}-[a-z]{2}"
                }
            );

            routes.MapRoute(
                name: "CmsNonCultureRoute",
                url: "{*path}",
                defaults: new
                {
                    culture = "en-US",
                    controller = "VirtualPage",
                    action = "Index"
                }
            );

            //routes.MapRoute(
            //    name: "Default",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            //);

            routes.LowercaseUrls = true;
        }
    }
}
