using System;
using System.Collections.Generic;
using TinySql.Attributes;

namespace TinyCms.Models
{	public partial class PageType
{
		[FK("PageLayout","Id","dbo","FK_PageType_PageLayout")]
		[PK]
		public String  Culture { get; set; }

		[PK]
		public Guid  Id { get; set; }

		public String  Html { get; set; }

		public String  Name { get; set; }

		[FK("PageLayout","Id","dbo","FK_PageType_PageLayout")]
		public Guid  PageHostId { get; set; }

		[FK("PageLayout","Id","dbo","FK_PageType_PageLayout")]
		public Nullable<Guid>  PageLayoutId { get; set; }

		public String  Title { get; set; }

	}
}
