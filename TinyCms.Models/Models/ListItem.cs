using System;
using System.Collections.Generic;
using TinySql.Attributes;

namespace TinyCms.Models
{	public partial class ListItem
{
		[FK("ListDefinition","Id","dbo","FK_ListItem_ListDefinition")]
		[PK]
		public Guid  ListDefinitionId { get; set; }

		[PK]
		public String  Value { get; set; }

		public Boolean  Disabled { get; set; }

		public Boolean  Selected { get; set; }

		public String  Text { get; set; }

	}
}
