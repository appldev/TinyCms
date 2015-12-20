using System;
using System.Collections.Generic;
using TinySql.Attributes;

namespace TinyCms.Models
{	public partial class ReservedRoutes
{
		[PK]
		public String  RoutePath { get; set; }

		public String  Action { get; set; }

		public String  Controller { get; set; }

	}
}
