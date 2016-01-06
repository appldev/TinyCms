using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using TinyCms.Models;

namespace TinyCms.API.Security
{
    public static class TinyCmsClaimTypes
    {
        public static readonly string UserAudienceClaim = "uri:TinyCms:AudienceId";
    }
    public interface IUserSecurityResolver
    {
        int GetUserSecurityLevelForPage(IPrincipal principal, Page page, HttpRequestBase Request, HttpSessionStateBase Session);
    }

    public interface IUserClaimResolver
    {
        T GetUserClaim<T>(IPrincipal principal, string ClaimType, Page page, HttpRequestBase Request, HttpSessionStateBase Session);
        bool HasUserClaim(IPrincipal principal, string ClaimType);
    }

    public class DefaultSecurityResolver : IUserSecurityResolver, IUserClaimResolver
    {
        public T GetUserClaim<T>(IPrincipal principal, string ClaimType, Page page, HttpRequestBase Request, HttpSessionStateBase Session)
        {
            Claim c = null;
            if (principal is System.Web.Security.RolePrincipal)
            {
                c = ((System.Web.Security.RolePrincipal)principal).Claims.FirstOrDefault(x => x.Type.Equals(ClaimType, StringComparison.OrdinalIgnoreCase));
            }
            else
            {
                c = ((ClaimsPrincipal)principal).Claims.FirstOrDefault(x => x.Type.Equals(ClaimType, StringComparison.OrdinalIgnoreCase));
            }
            if (c != null && !string.IsNullOrEmpty(c.Value))
            {
                object o = Convert.ChangeType(c.Value, typeof(T));
                if (o != null)
                {
                    return (T)o;
                }
            }
            return default(T);
        }

        private Claim GetClaimInternal(IEnumerable<Claim> Claims, string ClaimType)
        {
            foreach (Claim c in Claims)
            {
                if (c.Type.Equals(ClaimType))
                {
                    return c;
                }
            }
            return null;
        }


        public bool HasUserClaim(IPrincipal principal, string ClaimType)
        {
            if (principal is System.Web.Security.RolePrincipal)
            {
                return ((System.Web.Security.RolePrincipal)principal).Claims.Any(x => x.Type.Equals(ClaimType, StringComparison.OrdinalIgnoreCase));
            }
            else
            {
                return ((ClaimsPrincipal)principal).Claims.Any(x => x.Type.Equals(ClaimType, StringComparison.OrdinalIgnoreCase));
            }
        }

        public int GetUserSecurityLevelForPage(IPrincipal principal, Page page, HttpRequestBase Request, HttpSessionStateBase Session)
        {
            if (!Request.IsAuthenticated)
            {
                if (!string.IsNullOrEmpty(Request.AnonymousID))
                {
                    return (int)PageSecurity.PageSecurityLevels.Unknown;
                }
                else
                {
                    return (int)PageSecurity.PageSecurityLevels.Anonymous;
                }
            }

            object oUser = Session["_LOGGEDIN"];
            if (oUser != null && Convert.ToString(oUser).Equals(principal.Identity.Name))
            {
                return (int)PageSecurity.PageSecurityLevels.LoggedIn;
            }
            return (int)PageSecurity.PageSecurityLevels.Known;
        }


    }
}