using System;
using System.Collections.Generic;
using TinySql.Attributes;

namespace TinyCms.Models
{	public partial class PageHost
{
		[PK]
		public Guid  Id { get; set; }

		public Int32  LCID { get; set; }

		public String  Name { get; set; }

	}
}
