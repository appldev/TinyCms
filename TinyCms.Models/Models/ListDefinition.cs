using System;
using System.Collections.Generic;
using TinySql.Attributes;

namespace TinyCms.Models
{	public partial class ListDefinition
{
		[PK]
		public Guid  Id { get; set; }

		public String  Name { get; set; }

	}
}
