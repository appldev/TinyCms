using System;
using System.Collections.Generic;
using TinySql.Attributes;

namespace TinyCms.Models
{	public partial class PageFolder
{
		public Nullable<Int32>  FolderIndex { get; set; }

		public Nullable<Int32>  folderlevel { get; set; }

		public String  folderpath { get; set; }

		public Nullable<Guid>  Id { get; set; }

		public String  Name { get; set; }

		public Nullable<Guid>  PageHostId { get; set; }

		public Nullable<Guid>  ParentId { get; set; }

	}
}
