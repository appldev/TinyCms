using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinySql;

namespace TinyCms.Models
{
    public partial class LibraryFolder
    {
        public static List<LibraryFolder> ByLibrary(Guid Id)
        {
            SqlBuilder builder = SqlBuilder.Select()
                .From("LibraryFolder")
                .AllColumns()
                .Where<Guid>("LibraryFolder", "LibraryId", SqlOperators.Equal, Id)
                .Builder();

            return builder.List<LibraryFolder>(EnforceTypesafety: false);
        }

        public static bool Create(LibraryFolderBase Folder)
        {
            SqlBuilder builder = TypeBuilder.Insert<LibraryFolderBase>(Folder);
            return builder.ExecuteNonQuery() == 1;
        }

    }
}
