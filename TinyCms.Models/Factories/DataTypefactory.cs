using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinySql;
using TinyCms.Models;

namespace TinyCms.Factories
{
    public class ListProviderFactory
    {

    }


    public class DataTypefactory
    {
        public static DataType GetDataType(Guid Id)
        {
            if (Caching.DataTypes.Has(Id))
            {
                return Caching.DataTypes.Get(Id);
            }

            SqlBuilder builder = SqlBuilder.Select()
                .From("DataType")
                .AllColumns()
                .SubSelect("Field", "Id", "DataTypeId", BuilderName: "Fields")
                .AllColumns()
                .Builder().BaseTable()
                .Where<Guid>("DataType", "Id", SqlOperators.Equal, Id)
                .Builder();

            DataType dataType = builder.FirstOrDefault<DataType>(EnforceTypesafety: false);
            foreach (Models.Field field in dataType.Fields.Where(x => x.DataTypeId.HasValue))
            {
                field.DataType = GetDataType(field.DataTypeId.Value);
            }


            //DataSet ds = builder.DataSet();
            //DataType dataType = builder.List<DataType>(ds.Tables[0], false, true).First();
            //dataType.Fields = builder.List<FieldBase>(ds.Tables[1], false, true);
            

            return Caching.DataTypes.Add(dataType.Id, dataType);


        }
    }
}
