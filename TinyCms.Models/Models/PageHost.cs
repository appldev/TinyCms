using TinySql.Attributes;
using System;

namespace TinyCms.Models
{	public partial class PageHost
{
		[PK]
		public String  Name { get; set; }

		public Int32  LCID { get; set; }

	}
}
