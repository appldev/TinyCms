using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TinyCms.API;
using TinyCms.API.Security;
using TinyCms.Models;

namespace TinyCms.Filters
{
    public class CmsPageAuthorizeAttribute : AuthorizeAttribute
    {
        private string culture = null;
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            object oCulture = filterContext.RouteData.Values["culture"];
            if (oCulture != null)
            {
                if (!oCulture.ToString().Equals("xx-YY", StringComparison.OrdinalIgnoreCase))
                {
                    culture = oCulture.ToString();
                }
            }
            base.OnAuthorization(filterContext);
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            Page page = httpContext.GetCmsPage(culture);
            if (page == null || page.PageSecurityId == 0)
            {
                return true;
            }

            int i = SecuritySettings.Default.GetUserSecurityLevelForPage(httpContext.User, page, httpContext.Request, httpContext.Session);
            if (i < page.PageSecurityId)
            {
                return false;
            }

            if (page.PageAudienceId.HasValue)
            {
                if (!SecuritySettings.Default.HasUserClaim(httpContext.User, TinyCmsClaimTypes.UserAudienceClaim))
                {
                    return false;
                }
                List<int> list = SecuritySettings.Default.GetUserClaim<int>(httpContext.User, TinyCmsClaimTypes.UserAudienceClaim, page,httpContext.Request, httpContext.Session);
                return list.Count > 0 && list.TrueForAll(id => ((id & page.PageAudienceId.Value) == page.PageAudienceId.Value));
            }
            
            return base.AuthorizeCore(httpContext);
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.RouteData.Values.Add("message", "you are not authorized");
            filterContext.Result = new HttpUnauthorizedResult("You are not allowed to see the page");
            base.HandleUnauthorizedRequest(filterContext);
        }
    }
}