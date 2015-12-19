using TinySql.Attributes;
using System;

namespace TinyCms.Models
{	public partial class LibraryType
{
		[PK]
		public Int32  Id { get; set; }

		public String  FileTypes { get; set; }

		public String  Name { get; set; }

	}
}
