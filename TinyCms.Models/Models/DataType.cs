using TinySql.Attributes;
using System;

namespace TinyCms.Models
{	public partial class DataType
{
		[PK]
		public Guid  Id { get; set; }

		public String  DisplayName { get; set; }

		public String  Model { get; set; }

		public String  Name { get; set; }

		[FK("RenderType","Id","dbo","FK_DataType_RenderType")]
		public Int32  RenderTypeId { get; set; }

	}
}
