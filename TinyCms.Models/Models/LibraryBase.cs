using System;
using System.Collections.Generic;
using TinySql.Attributes;

namespace TinyCms.Models
{	public partial class LibraryBase
{
		[PK]
		public Guid  Id { get; set; }

		public String  Description { get; set; }

		[FK("LibraryType","Id","dbo","FK_LibraryBase_LibraryType")]
		public Int32  LibraryTypeId { get; set; }

		public String  Name { get; set; }

		public String  PathRoot { get; set; }

	}
}
