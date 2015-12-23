using System;
using System.Collections.Generic;
using TinySql.Attributes;

namespace TinyCms.Models
{	public partial class FieldBase
{
		[PK]
		public Int32  Id { get; set; }

		[FK("DataTypeBase","Id","dbo","FK_FieldBase_DataTypeBase")]
		public Guid  DataTypeId { get; set; }

		public String  DefaultValue { get; set; }

		public String  DisplayName { get; set; }

		[FK("EditorType","Id","dbo","FK_FieldBase_EditorType")]
		public Int32  EditorTypeId { get; set; }

		[FK("FieldType","Id","dbo","FK_FieldBase_FieldType")]
		public Int32  FieldTypeId { get; set; }

		public String  HelpText { get; set; }

		public String  MissingValue { get; set; }

		public String  Name { get; set; }

		public String  PlaceHolder { get; set; }

		public Boolean  Required { get; set; }

		public Boolean  UseValidation { get; set; }

		public String  ValidationAttributes { get; set; }

		public Int32  ValidationMax { get; set; }

		public String  ValidationMessage { get; set; }

		public Int32  ValidationMin { get; set; }

	}
}
