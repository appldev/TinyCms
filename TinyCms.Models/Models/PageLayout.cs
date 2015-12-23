using System;
using System.Collections.Generic;
using TinySql.Attributes;

namespace TinyCms.Models
{	public partial class PageLayout
{
		[PK]
		public String  Culture { get; set; }

		[PK]
		public Guid  Id { get; set; }

		[PK]
		public Guid  PageHostId { get; set; }

		public String  Html { get; set; }

		public String  Name { get; set; }

		public String  Title { get; set; }

	}
}
