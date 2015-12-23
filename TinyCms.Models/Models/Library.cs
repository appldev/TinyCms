using System;
using System.Collections.Generic;
using TinySql.Attributes;

namespace TinyCms.Models
{	public partial class Library
{
		public String  Description { get; set; }

		public Guid  Id { get; set; }

		public Int32  LibraryTypeId { get; set; }

		public String  LibraryTypeName { get; set; }

		public String  Name { get; set; }

		public String  PathRoot { get; set; }

	}
}
