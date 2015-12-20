using System;
using System.Collections.Generic;
using TinySql.Attributes;

namespace TinyCms.Models
{	public partial class EditorType
{
		[PK]
		public Int32  Id { get; set; }

		public String  Name { get; set; }

	}
}
