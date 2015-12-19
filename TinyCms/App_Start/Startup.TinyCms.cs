using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TinySql;

namespace TinyCms
{
	public static class TinyCmsStartup
	{
		public static void Startup()
        {
            SqlBuilder.DefaultConnection = System.Configuration.ConfigurationManager.ConnectionStrings["cms"].ConnectionString;
			



        }
	}
}