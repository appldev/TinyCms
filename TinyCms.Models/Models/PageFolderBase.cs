using System;
using System.Collections.Generic;
using TinySql.Attributes;

namespace TinyCms.Models
{	public partial class PageFolderBase
{
		[PK]
		public Guid  Id { get; set; }

		public Int32  FolderIndex { get; set; }

		public String  Name { get; set; }

		public Guid  PageHostId { get; set; }

		[FK("PageFolderBase","Id","dbo","FK_PageFolderBase_PageFolderBase")]
		public Nullable<Guid>  ParentId { get; set; }

	}
}
