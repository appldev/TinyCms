using System;
using System.Collections.Generic;
using TinySql.Attributes;

namespace TinyCms.Models
{	public partial class DataTypeTableRow
{
		[PK]
		public Guid  Id { get; set; }

		[FK("DataTypeTable","Id","dbo","FK_DataTypeTableRow_DataTypeTable")]
		public Guid  DataTypeTableId { get; set; }

		public String  Model { get; set; }

	}
}
