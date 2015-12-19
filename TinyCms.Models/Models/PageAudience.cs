using TinySql.Attributes;
using System;

namespace TinyCms.Models
{	public partial class PageAudience
{
		[PK]
		public Int32  Id { get; set; }

		public String  Name { get; set; }

	}
}
