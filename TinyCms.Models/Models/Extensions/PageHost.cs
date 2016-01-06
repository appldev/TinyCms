using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinySql;

namespace TinyCms.Models
{
    public partial class PageHost
    {
        public static string BuildPath(PageHost Host, string Culture = null, string Path = null)
        {
            Culture = Culture ?? Host.Culture;
            Path = Path ?? "/";
            if (!Path.StartsWith("/"))
            {
                Path = "/" + Path;
            }
            return string.Format("/{0}/{1}{2}", Host.Name, Culture, Path).ToLower();
        }

        public static string BuildPath(string Host, string Culture = null, string Path = null)
        {
            PageHost ph = Caching.Hosts != null ? Caching.Hosts.ByName(Host) : PageHost.ByName(Host);
            if (ph == null)
            {
                return null;
            }
            return BuildPath(ph, Culture, Path);
        }
        public static PageHost ByName(string Name)
        {
            SqlBuilder builder = SqlBuilder.Select()
                .From("PageHost")
                .AllColumns()
                .Where<string>("PageHost", "Name", SqlOperators.Equal, Name)
                .Builder();

            return builder.FirstOrDefault<PageHost>(EnforceTypesafety: false);
        }

        public static List<PageHost> List()
        {
            SqlBuilder builder = SqlBuilder.Select()
                .From("PageHost")
                .AllColumns()
                .Builder();

            return builder.List<PageHost>(EnforceTypesafety: false);
        }
    }
}
