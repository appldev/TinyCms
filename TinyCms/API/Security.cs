using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using TinyCms.API.Security;
using TinyCms.Models;

namespace TinyCms.API
{
    public class SecuritySettings
    {
        private static SecuritySettings _default = null;
        public static SecuritySettings Default
        {
            get
            {
                if (_default == null)
                {
                    _default = new SecuritySettings();
                    _default.UserSecurityResolvers.Add(new DefaultSecurityResolver());
                    _default.UserClaimResolvers.Add(new DefaultSecurityResolver());
                }
                return _default;
            }
            set
            {
                _default = value;
            }
        }

        public List<IUserSecurityResolver> UserSecurityResolvers = new List<IUserSecurityResolver>();
        public List<IUserClaimResolver> UserClaimResolvers = new List<IUserClaimResolver>();

        public bool HasUserClaim(IPrincipal principal, string ClaimType)
        {
            foreach (IUserClaimResolver resolver in UserClaimResolvers)
            {
                if (resolver.HasUserClaim(principal, ClaimType))
                {
                    return true;
                }
            }
            return false;
        }

        public List<T> GetUserClaim<T>(IPrincipal principal, string ClaimType, Page page, HttpRequestBase Request, HttpSessionStateBase Session)
        {
            List<T> list = new List<T>();

            foreach (IUserClaimResolver resolver in UserSecurityResolvers)
            {
                T result = resolver.GetUserClaim<T>(principal, ClaimType, page, Request, Session);
                if (!result.Equals(default(T)))
                {
                    list.Add(result);
                }
            }
            return list;
        }

        public int GetUserSecurityLevelForPage(IPrincipal principal, Page page, HttpRequestBase Request, HttpSessionStateBase Session)
        {
            int i = 0;
            foreach (IUserSecurityResolver resolver in UserSecurityResolvers)
            {
                int result = resolver.GetUserSecurityLevelForPage(principal, page, Request, Session);
                if (result > i)
                {
                    i = result;
                }
            }
            return i;
        }


    }
}