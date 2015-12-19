using TinySql.Attributes;
using System;

namespace TinyCms.Models
{	public partial class AllLibraryItems
{
		public String  Description { get; set; }

		public Nullable<Int32>  folderlevel { get; set; }

		public String  folderpath { get; set; }

		public Nullable<Guid>  Id { get; set; }

		public Nullable<Guid>  LibraryFolderId { get; set; }

		public Nullable<Guid>  LibraryId { get; set; }

		public String  Name { get; set; }

		public String  Title { get; set; }

	}
}
