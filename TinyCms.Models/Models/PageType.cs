using TinySql.Attributes;
using System;

namespace TinyCms.Models
{	public partial class PageType
{
		[PK]
		public Guid  Id { get; set; }

		[FK("PageLayout","Id","dbo","FK_PageType_PageLayout")]
		[PK]
		public Int32  LCID { get; set; }

		public String  Html { get; set; }

		public String  Name { get; set; }

		[FK("PageLayout","Id","dbo","FK_PageType_PageLayout")]
		public Guid  PageHostId { get; set; }

		[FK("PageLayout","Id","dbo","FK_PageType_PageLayout")]
		public Nullable<Guid>  PageLayoutId { get; set; }

		public String  Title { get; set; }

	}
}
