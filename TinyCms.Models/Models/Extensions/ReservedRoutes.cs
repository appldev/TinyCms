using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyCms.Models
{
    public partial class ReservedRoutes
    {
        public static List<ReservedRoutes> Get()
        {
            return TinySql.Data.All<ReservedRoutes>(EnforceTypesafety: false);
        }
    }
}
