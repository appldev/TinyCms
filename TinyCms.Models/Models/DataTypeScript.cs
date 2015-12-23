using System;
using System.Collections.Generic;
using TinySql.Attributes;

namespace TinyCms.Models
{	public partial class DataTypeScript
{
		[FK("DataTypeBase","Id","dbo","FK_DataTypeScript_DataTypeBase")]
		[PK]
		public Guid  DataTypeBaseId { get; set; }

		[FK("LibraryItemBase","Id","dbo","FK_DataTypeScript_LibraryItemBase")]
		[PK]
		public Guid  LibraryItemBaseId { get; set; }

	}
}
