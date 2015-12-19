using TinySql.Attributes;
using System;

namespace TinyCms.Models
{	public partial class AllUnpublishedPages
{
		public String  Description { get; set; }

		public String  fullpath { get; set; }

		public Guid  Id { get; set; }

		public Int32  LCID { get; set; }

		public String  Model { get; set; }

		public String  Name { get; set; }

		public Nullable<Int32>  PageAudienceId { get; set; }

		public Guid  PageFolderId { get; set; }

		public Int32  PageSecurityId { get; set; }

		public Nullable<Guid>  PageTypeId { get; set; }

		public Boolean  RequireSsl { get; set; }

		public String  Title { get; set; }

	}
}
