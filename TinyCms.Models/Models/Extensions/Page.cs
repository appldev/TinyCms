using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinySql;

namespace TinyCms.Models
{
    public partial class Page
    {
        public static List<Page> ByHost(Guid Id)
        {
            SqlBuilder builder = SqlBuilder.Select()
                .From("Page")
                .AllColumns()
                .Where<Guid>("Page", "PageHostId", SqlOperators.Equal, Id)
                .Builder();

            return builder.List<Page>(EnforceTypesafety: false);
        }
    }
}
