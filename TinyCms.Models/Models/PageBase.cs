using System;
using System.Collections.Generic;
using TinySql.Attributes;

namespace TinyCms.Models
{	public partial class PageBase
{
		[FK("PageType","Id","dbo","FK_PageBase_PageType")]
		[PK]
		public String  Culture { get; set; }

		[PK]
		public String  Name { get; set; }

		[FK("PageFolderBase","Id","dbo","FK_PageBase_PageFolderBase")]
		[PK]
		public Guid  PageFolderId { get; set; }

		public DateTime  CreatedOn { get; set; }

		public String  Description { get; set; }

		public Guid  Id { get; set; }

		public Boolean  IsExternal { get; set; }

		public String  Model { get; set; }

		public DateTime  ModifiedOn { get; set; }

		[FK("PageAudience","Id","dbo","FK_PageBase_PageAudience")]
		public Nullable<Int32>  PageAudienceId { get; set; }

		[FK("PageSecurity","Id","dbo","FK_PageBase_PageSecurity")]
		public Int32  PageSecurityId { get; set; }

		[FK("PageType","Id","dbo","FK_PageBase_PageType")]
		public Nullable<Guid>  PageTypeId { get; set; }

		public Boolean  RequireSsl { get; set; }

		public String  Title { get; set; }

	}
}
