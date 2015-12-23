using System;
using System.Collections.Generic;
using TinySql.Attributes;

namespace TinyCms.Models
{	public partial class LibraryFolderBase
{
		[PK]
		public Guid  Id { get; set; }

		public String  Description { get; set; }

		[FK("LibraryBase","Id","dbo","FK_LibraryFolder_LibraryBase")]
		public Guid  LibraryId { get; set; }

		public String  Name { get; set; }

		[FK("LibraryFolderBase","Id","dbo","FK_LibraryFolder_LibraryFolder")]
		public Nullable<Guid>  ParentId { get; set; }

		public String  Title { get; set; }

	}
}
