using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TinyCms.Models;

namespace TinyCms.Filters
{
    public class RequireSslActionFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Request.IsSecureConnection)
            {
                return;
            }

            Page page = filterContext.HttpContext.GetCmsPage();
            if (page != null)
            {
                if (page.RequireSsl)
                {
                    UriBuilder builder = new UriBuilder(filterContext.HttpContext.Request.Url);
                    builder.Scheme = "https";
                    builder.Port = 443;
#if DEBUG
                    builder.Port = 44300;
#endif
                    filterContext.Result = new RedirectResult(builder.ToString());
                }
            }
        }
    }
}