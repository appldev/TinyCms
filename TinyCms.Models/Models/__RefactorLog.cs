using System;
using System.Collections.Generic;
using TinySql.Attributes;

namespace TinyCms.Models
{	public partial class __RefactorLog
{
		[PK]
		public Guid  OperationKey { get; set; }

	}
}
