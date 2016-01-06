using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyCms.Models
{
    public partial class PageSecurity
    {
        public enum PageSecurityLevels: int
        {
            Anonymous = 0,
            Unknown = 1,
            Known = 2,
            LoggedIn = 3
        }
    }
}
