using TinySql.Attributes;
using System;

namespace TinyCms.Models
{	public partial class PublishedPage
{
		[FK("PageType","Id","dbo","FK_PublishedPage_PageType")]
		[PK]
		public Int32  LCID { get; set; }

		[PK]
		public String  Name { get; set; }

		[PK]
		public Guid  PageFolderId { get; set; }

		public String  Description { get; set; }

		public Guid  Id { get; set; }

		public String  Model { get; set; }

		[FK("PageAudience","Id","dbo","FK_PublishedPage_PageAudience")]
		public Nullable<Int32>  PageAudienceId { get; set; }

		[FK("PageSecurity","Id","dbo","FK_PublishedPage_PageSecurity")]
		public Int32  PageSecurityId { get; set; }

		[FK("PageType","Id","dbo","FK_PublishedPage_PageType")]
		public Nullable<Guid>  PageTypeId { get; set; }

		public Boolean  RequireSsl { get; set; }

		public String  Title { get; set; }

	}
}
