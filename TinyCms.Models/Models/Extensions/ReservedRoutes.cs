using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinySql;

namespace TinyCms.Models
{
    public partial class ReservedRoutes
    {
        public static List<ReservedRoutes> Get()
        {
            return Data.All<ReservedRoutes>(EnforceTypesafety: false);
        }

        public static ReservedRoutes ByName(string Name)
        {
            SqlBuilder builder = SqlBuilder.Select()
                .From("ReservedRoutes")
                .AllColumns()
                .Where<string>("reservedRoutes", "Name", SqlOperators.Equal, Name)
                .Builder();

            return builder.FirstOrDefault<ReservedRoutes>(EnforceTypesafety: false);
        }

        public static bool Update(ReservedRoutes route)
        {
            SqlBuilder builder = TypeBuilder.Update<ReservedRoutes>(route);
            return builder.ExecuteNonQuery() == 1;
        }
    }
}
