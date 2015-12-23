using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinySql;

namespace TinyCms.Models
{
    public partial class LibraryItem
    {
        public static List<LibraryItem> ByLibrary(Guid Id)
        {
            SqlBuilder builder = SqlBuilder.Select()
                .From("LibraryItem")
                .AllColumns()
                .Where<Guid>("LibraryItem", "LibraryId", SqlOperators.Equal, Id)
                .Builder();

            return builder.List<LibraryItem>(EnforceTypesafety: false);
        }

        public static bool Create(LibraryItemBase item)
        {
            SqlBuilder builder = TypeBuilder.Insert<LibraryItemBase>(item);
            return builder.ExecuteNonQuery() == 1;
        }
    }
}
