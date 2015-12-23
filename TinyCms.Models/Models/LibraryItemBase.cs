using System;
using System.Collections.Generic;
using TinySql.Attributes;

namespace TinyCms.Models
{	public partial class LibraryItemBase
{
		[PK]
		public Guid  Id { get; set; }

		public DateTime  CreatedOn { get; set; }

		public String  Data { get; set; }

		public String  Description { get; set; }

		[FK("LibraryFolderBase","Id","dbo","FK_LibraryItem_LibraryFolder")]
		public Guid  LibraryFolderId { get; set; }

		public DateTime  ModifiedOn { get; set; }

		public String  Name { get; set; }

		public String  Title { get; set; }

	}
}
