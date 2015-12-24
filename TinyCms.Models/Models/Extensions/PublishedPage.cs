using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinySql;

namespace TinyCms.Models
{
    public partial class PublishedPage
    {
        public static List<PublishedPage> ByHost(Guid Id)
        {
            SqlBuilder builder = SqlBuilder.Select()
               .From("PublishedPage")
               .AllColumns()
               .Where<Guid>("PublishedPage", "PageHostId", SqlOperators.Equal, Id)
               .Builder();

            return builder.List<PublishedPage>(EnforceTypesafety: false);
        }
    }
}
