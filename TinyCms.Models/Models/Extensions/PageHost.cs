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
        public static PageHost ByName(string Name)
        {
            SqlBuilder builder = SqlBuilder.Select()
                .From("PageHost")
                .AllColumns()
                .Where<string>("PageHost", "Name", SqlOperators.Equal, Name)
                .Builder();

            return builder.List<PageHost>(EnforceTypesafety: false).FirstOrDefault();
        }
    }
}
