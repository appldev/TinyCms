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
            string metadataFile = HttpContext.Current.Server.MapPath(System.Configuration.ConfigurationManager.ConnectionStrings["cmsmetadata"].ConnectionString);
            SqlBuilder.DefaultMetadata = TinySql.Serialization.SerializationExtensions.FromFile(metadataFile);
            TinySql.Cache.CacheProvider.UseResultCache = false;

        }
	}
}