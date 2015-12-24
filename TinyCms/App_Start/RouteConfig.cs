using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using TinyCms.Models;

namespace TinyCms
{
    public class RouteConfig
    {
        private static RouteValueDictionary GetDictionary(string source)
        {
            RouteValueDictionary d = new RouteValueDictionary();
            if (!string.IsNullOrEmpty(source) && source.IndexOf('=') > 0)
            {
                foreach (string s in source.Split(';'))
                {
                    string[] kv = s.Split('=');
                    {
                        int i;
                        if (Int32.TryParse(kv[1], out i))
                        {
                            d.Add(kv[0], i);
                        }
                        else
                        {
                            if (kv[1].Equals("Optional", StringComparison.OrdinalIgnoreCase))
                            {
                                d.Add(kv[0], UrlParameter.Optional);
                            }
                            else
                            {
                                d.Add(kv[0], kv[1]);
                            }
                        }
                    }
                }
            }
            return d;
        }
        public static void RegisterRoutes(RouteCollection routes)
        {
            List<ReservedRoutes> reserved = ReservedRoutes.Get();
            foreach (ReservedRoutes ignore in reserved.Where(x => x.IsSystem == false && x.IsIgnore == true).OrderBy(x => x.RouteOrder))
            {
                RouteValueDictionary d = new RouteValueDictionary();
                routes.Ignore(ignore.RoutePath, GetDictionary(ignore.Constraints));
            }
            //routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            foreach (ReservedRoutes optin in reserved.Where(x => x.IsSystem == false && x.IsIgnore == false).OrderBy(x => x.RouteOrder))
            {
                string[] ns = string.IsNullOrEmpty(optin.Namespaces) ? new string[0] : optin.Namespaces.Split(';');
                routes.MapRoute(
                    name: optin.Name,
                    url: optin.RoutePath,
                    defaults: GetDictionary(optin.Defaults),
                    constraints: GetDictionary(optin.Constraints),
                    namespaces: ns
                );
            }

            foreach (ReservedRoutes cms in reserved.Where(x => x.IsSystem == true).OrderBy(x => x.RouteOrder))
            {
                string[] ns = string.IsNullOrEmpty(cms.Namespaces) ? new string[0] : cms.Namespaces.Split(';');
                routes.MapRoute(
                    name: cms.Name,
                    url: cms.RoutePath,
                    defaults: GetDictionary(cms.Defaults),
                    constraints: GetDictionary(cms.Constraints),
                    namespaces: ns
                );
            }

            //RouteValueDictionary rvd = new RouteValueDictionary();
            //// rvd.Add("controller", "Home");
            //rvd.Add("action", "Index");
            //rvd.Add("id", UrlParameter.Optional);
            //RouteValueDictionary ct = new RouteValueDictionary();
            //ct.Add("action", "^((?!Contact).)*$");
            //ct.Add("controller", "Home|Account|Manage");
            //dynamic HomeDefaults = new Object();
            //HomeDefaults.controller = "Home";
            //HomeDefaults.action = "Index";
            //HomeDefaults.id = UrlParameter.Optional;
            //object o = new
            //{
            //    controller = HomeDefaults.controller,
            //    action = HomeDefaults.action,
            //    id = HomeDefaults.id
            //};

            //o = ContentObject.ToObject(HomeDefaults);
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

            //routes.MapRoute(
            //        name: "Default",
            //        url: "{controller}/{action}/{id}",
            //        defaults: rvd,
            //        constraints: ct
            //    );

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
