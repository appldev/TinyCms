using System;
using System.Collections.Generic;
using TinySql.Attributes;

namespace TinyCms.Models
{	public partial class DataType
{
		public String  DisplayName { get; set; }

		public Guid  Id { get; set; }

		public String  Model { get; set; }

		public String  Name { get; set; }

		public Int32  RenderTypeId { get; set; }

		public String  RenderTypeName { get; set; }

	}
}
