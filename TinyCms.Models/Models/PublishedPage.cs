using System;
using System.Collections.Generic;
using TinySql.Attributes;

namespace TinyCms.Models
{	public partial class PublishedPage
{
		public String  Culture { get; set; }

		public Nullable<Guid>  DataTypeId { get; set; }

		public String  Description { get; set; }

		public String  FilePath { get; set; }

		public Nullable<Int32>  FolderLevel { get; set; }

		public String  FullPath { get; set; }

		public String  Html { get; set; }

		public Guid  Id { get; set; }

		public String  Model { get; set; }

		public String  Name { get; set; }

		public Nullable<Int32>  PageAudienceId { get; set; }

		public Guid  PageFolderId { get; set; }

		public Guid  PageHostId { get; set; }

		public Int32  PageSecurityId { get; set; }

		public Nullable<Guid>  PageTypeId { get; set; }

		public Boolean  RequireSsl { get; set; }

		public String  Title { get; set; }

	}
}
