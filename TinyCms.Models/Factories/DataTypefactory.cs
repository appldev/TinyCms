using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinySql;

namespace TinyCms.Models.Factories
{
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
                .SubSelect("Field", "Id", "DataTypeId")
                .AllColumns()
                .Builder().BaseTable()
                .Where<Guid>("DataType", "Id", SqlOperators.Equal, Id)
                .Builder();

            DataSet ds = builder.DataSet();
            DataType dataType = builder.List<DataType>(ds.Tables[0], false, true).First();
            dataType.Fields = builder.List<Field>(ds.Tables[1], false, true);
            

            return Caching.DataTypes.Add(dataType.Id, dataType);


        }
    }
}
