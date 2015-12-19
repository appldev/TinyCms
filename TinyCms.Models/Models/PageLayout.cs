using TinySql.Attributes;
using System;

namespace TinyCms.Models
{	public partial class PageLayout
{
		[PK]
		public Guid  Id { get; set; }

		[PK]
		public Int32  LCID { get; set; }

		[PK]
		public Guid  PageHostId { get; set; }

		public String  Html { get; set; }

		public String  Name { get; set; }

		public String  Title { get; set; }

	}
}
