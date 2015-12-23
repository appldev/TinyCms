using System;
using System.Collections.Generic;
using TinySql.Attributes;

namespace TinyCms.Models
{	public partial class LibraryFolder
{
		public String  Description { get; set; }

		public Nullable<Int32>  folderlevel { get; set; }

		public String  folderpath { get; set; }

		public Nullable<Guid>  Id { get; set; }

		public Nullable<Guid>  LibraryId { get; set; }

		public String  Name { get; set; }

		public Nullable<Guid>  ParentId { get; set; }

		public String  Title { get; set; }

	}
}
