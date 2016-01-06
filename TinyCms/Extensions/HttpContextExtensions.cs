using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using TinyCms.Models;

namespace TinyCms
{
    public static class HttpContextExtensions
    {
        public static Page GetCmsPage(this HttpContextBase context, string Culture = null)
        {
            PageHost ph = Caching.Hosts != null ? Caching.Hosts.ByName(context.Request.UserHostName) : PageHost.ByName(context.Request.UserHostName);
            if (string.IsNullOrEmpty(Culture) || Culture.Equals("xx-yy", StringComparison.OrdinalIgnoreCase ))
            {
                Culture = ph.Culture;
            }
            string fullpath = PageHost.BuildPath(ph, Culture, context.Request.Path);
            return Caching.PublishedPages.ByFullPath(fullpath);
        }
    }
}