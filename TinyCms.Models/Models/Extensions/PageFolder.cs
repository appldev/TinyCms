using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinySql;

namespace TinyCms.Models
{
    public partial class PageFolder
    {
        public static List<PageFolder> ByHost(Guid Id)
        {
            SqlBuilder builder = SqlBuilder.Select()
                .From("PageFolder")
                .AllColumns()
                .Where<Guid>("PageFolder", "PageHostId", SqlOperators.Equal, Id)
                .Builder();

            return builder.List<PageFolder>(EnforceTypesafety: false);
        }

        public static PageFolder ById(Guid Id)
        {
            SqlBuilder builder = SqlBuilder.Select()
                .From("PageFolder")
                .AllColumns()
                .Where<Guid>("PageFolder", "Id", SqlOperators.Equal, Id)
                .Builder();

            return builder.FirstOrDefault<PageFolder>(EnforceTypesafety: false);
        }

        public static PageFolder Create(PageHost Host, PageFolder Parent, int FolderIndex, string FolderName)
        {
            Guid Id = Guid.NewGuid();

            SqlBuilder builder = SqlBuilder.Insert()
                .Into("PageFolderBase")
                .Value<Guid>("Id", Id, System.Data.SqlDbType.UniqueIdentifier)
                .Value<Guid>("PageHostId", Host.Id, System.Data.SqlDbType.UniqueIdentifier)
                .Value<Guid>("ParentId", Parent.Id.Value, System.Data.SqlDbType.UniqueIdentifier)
                .Value<string>("Name", FolderName, System.Data.SqlDbType.VarChar)
                .Value<int>("FolderIndex", FolderIndex, System.Data.SqlDbType.Int)
                .Output().PrimaryKey()
                .Builder();

            ResultTable result = builder.Execute();
            if (result.Count == 1)
            {
                return ById(Id);
            }
            return null;
                
        }
    }
}
