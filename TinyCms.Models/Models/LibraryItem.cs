using System;
using System.Collections.Generic;
using TinySql.Attributes;

namespace TinyCms.Models
{	public partial class LibraryItem
{
		public String  Description { get; set; }

		public String  FilePath { get; set; }

		public Nullable<Int32>  FolderLevel { get; set; }

		public String  FolderName { get; set; }

		public String  FolderPath { get; set; }

		public Guid  Id { get; set; }

		public Guid  LibraryFolderId { get; set; }

		public Nullable<Guid>  LibraryId { get; set; }

		public String  Name { get; set; }

		public String  Title { get; set; }

	}
}
