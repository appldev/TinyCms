using System;
using System.Collections.Generic;
using TinySql.Attributes;

namespace TinyCms.Models
{	public partial class ReservedRoutes
{
		[PK]
		public String  Name { get; set; }

		public String  Constraints { get; set; }

		public String  Defaults { get; set; }

		public Boolean  IsActive { get; set; }

		public Boolean  IsIgnore { get; set; }

		public Boolean  IsSystem { get; set; }

		public String  Namespaces { get; set; }

		public Int32  RouteOrder { get; set; }

		public String  RoutePath { get; set; }

	}
}
