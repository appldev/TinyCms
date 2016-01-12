using System;
using System.Collections.Generic;
using TinySql.Attributes;

namespace TinyCms.Models
{	public partial class DataTypeTable
{
		[PK]
		public Guid  Id { get; set; }

		public Guid  DataTypeId { get; set; }

		public String  Name { get; set; }

	}
}
