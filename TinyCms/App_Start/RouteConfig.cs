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

            dynamic HomeDefaults = new Object();
            HomeDefaults.controller = "Home";
            HomeDefaults.action = "Index";
            HomeDefaults.id = UrlParameter.Optional;
            object o = new
            {
                controller = HomeDefaults.controller,
                action = HomeDefaults.action,
                id = HomeDefaults.id
            };

            o = ContentObject.ToObject(HomeDefaults);
            // routes.Clear();
            //routes.MapRoute(
            //        name: "Default",
            //        url: "{controller}/{action}/{id}"
            //        // ,defaults: (object)HomeDefaults
            //    );

            //routes.MapRoute(
            //        name: "Default",
            //        url: "{controller}/{action}/{id}",
            //        defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            //    );

            routes.MapRoute(
                    name: "Default",
                    url: "{controller}/{action}/{id}",
                    defaults: (object)HomeDefaults
                );

            //routes.MapRoute(
            //    name: "CmsCultureRoute",
            //    url: "{culture}/{*path}",
            //    defaults: new {
            //        culture = "en-US",
            //        controller = "VirtualPage",
            //        action = "Index" },
            //    constraints: new
            //    {
            //        culture = "[a-z]{2}-[a-z]{2}"
            //    }
            //);

            //routes.MapRoute(
            //    name: "CmsNonCultureRoute",
            //    url: "{*path}",
            //    defaults: new
            //    {
            //        culture = "en-US",
            //        controller = "VirtualPage",
            //        action = "Index"
            //    }
            // );

            //routes.MapRoute(
            //    name: "Default",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            //);

            routes.LowercaseUrls = true;
            routes.AppendTrailingSlash = true;
        }
    }
}
