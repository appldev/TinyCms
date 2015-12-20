using System;
using System.Collections.Generic;
using TinySql.Attributes;

namespace TinyCms.Models
{	public partial class LibraryItem
{
		[PK]
		public Guid  Id { get; set; }

		public String  Description { get; set; }

		[FK("LibraryFolder","Id","dbo","FK_LibraryItem_LibraryFolder")]
		public Guid  LibraryFolderId { get; set; }

		public String  Name { get; set; }

		public String  Title { get; set; }

	}
}
