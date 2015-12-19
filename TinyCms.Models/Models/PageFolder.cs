using TinySql.Attributes;
using System;

namespace TinyCms.Models
{	public partial class PageFolder
{
		[PK]
		public Guid  Id { get; set; }

		public Int32  FolderIndex { get; set; }

		public String  Name { get; set; }

		public Guid  PageHostId { get; set; }

		[FK("PageFolder","Id","dbo","FK_PageFolder_PageFolder")]
		public Nullable<Guid>  ParentId { get; set; }

	}
}
