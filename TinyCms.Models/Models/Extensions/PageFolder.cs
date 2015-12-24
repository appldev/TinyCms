using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinySql;

namespace TinyCms.Models
{
    public partial class PageFolder
    {
        public static List<PageFolder> ByHost(Guid Id)
        {
            SqlBuilder builder = SqlBuilder.Select()
                .From("PageFolder")
                .Where<Guid>("PageFolder", "PageHostId", SqlOperators.Equal, Id)
                .Builder();

            return builder.List<PageFolder>(EnforceTypesafety: false);
        }
    }
}
