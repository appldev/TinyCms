using TinySql.Attributes;
using System;

namespace TinyCms.Models
{	public partial class LibraryFolder
{
		[PK]
		public Guid  Id { get; set; }

		public String  Description { get; set; }

		[FK("Library","Id","dbo","FK_LibraryFolder_Library")]
		public Guid  LibraryId { get; set; }

		public String  Name { get; set; }

		[FK("LibraryFolder","Id","dbo","FK_LibraryFolder_LibraryFolder")]
		public Nullable<Guid>  ParentId { get; set; }

		public String  Title { get; set; }

	}
}
