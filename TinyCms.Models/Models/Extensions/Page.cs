using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TinySql;

namespace TinyCms.Models
{
    public partial class Page
    {
        public static string GetFileHashByPath(string path)
        {
                byte[] bytes = Encoding.Unicode.GetBytes(path.ToCharArray());
            return GetMd5Hash(bytes);
        }

        public static string GetFileHashByPage(Page page)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(string.Concat(page.Id.ToString() + page.ModifiedOn.ToString()).ToCharArray());
            return GetMd5Hash(bytes);
        }

        private static string GetMd5Hash(byte[] bytes)
        {
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                byte[] hash = md5.ComputeHash(bytes);
                // Concatenate the hash bytes into one long String.
                return hash.Aggregate(
                    new StringBuilder(32),
                    (sb, b) => sb.Append(b.ToString("x2",
                                         CultureInfo.InvariantCulture)))
                    .ToString().ToLowerInvariant();
            }
        }

        public static SqlBuilder FilePathMapBuilderByHost(Guid? Id, bool Published = true)
        {
            return BuilderByHostInternal(Id, Published, new string[] { "Id", "FilePath" });
        }

        public static SqlBuilder FullPathMapBuilderByHost(Guid? Id, bool Published = true)
        {
            return BuilderByHostInternal(Id, Published, new string[] { "Id", "FullPath" });
        }

        private static SqlBuilder BuilderByHostInternal(Guid? Id = null, bool Published = true, string[] Columns = null)
        {
            string tableName = Published ? "PublishedPage" : "Page";
            Table t = SqlBuilder.Select().From(tableName);
            if (Columns == null)
            {
                t.AllColumns();
            }
            else
            {
                t.Columns(Columns);
            }
            if (Id.HasValue)
            {
                t.Where<Guid>(tableName, "PageHostId", SqlOperators.Equal, Id.Value);
            }
            return t.Builder();

        }

        public static SqlBuilder BuilderByHost(Guid? Id=null, bool Published = true)
        {
            return BuilderByHostInternal(Id, Published);
        }

        public static List<Page> ByHost(Guid Id, bool Published = true)
        {
            SqlBuilder builder = BuilderByHostInternal(Id, Published);
            return builder.List<Page>(EnforceTypesafety: false);
        }

        public static Page ById(Guid Id, bool Published = true)
        {
            string table = Published ? "PublishedPage" : "Page";
            SqlBuilder builder = SqlBuilder.Select()
                .From(table)
                .AllColumns()
                .Where<Guid>(table, "Id", SqlOperators.Equal, Id)
                .Builder();

            return builder.FirstOrDefault<Page>(EnforceTypesafety: false);
        }

       

       
        public static Page Create(PageFolder Folder, string Culture, string Name, string Title, string Description, string Model, Guid? PageTypeId, bool RequireSsl = false, int PageSecurityId = 0, int? PageAudienceId = null, bool IsExternal = false, DateTime? CreatedOn = null, DateTime? ModifiedOn = null)
        {
            Guid Id = Guid.NewGuid();
            CreatedOn = CreatedOn ?? DateTime.Now;
            ModifiedOn = ModifiedOn ?? CreatedOn;
            SqlBuilder builder = SqlBuilder.Insert()
                .Into("PageBase")
                .Value("Name", Name)
                .Value("Culture", Culture)
                .Value("PageFolderId", Folder.Id)
                .Value("Id", Id)
                .Value("Title", Title)
                .Value("Description", Description)
                .Value("Model", Model)
                .Value("PageTypeId", PageTypeId)
                .Value("RequireSsl", RequireSsl)
                .Value("PageSecurityId", PageSecurityId)
                .Value("PageAudienceId", PageAudienceId)
                .Value("IsExternal", IsExternal)
                .Value("CreatedOn", CreatedOn.Value)
                .Value("ModifiedOn", ModifiedOn.Value)
                .Output().PrimaryKey()
                .Builder();

            ResultTable result = builder.Execute();
            if (result.Count == 1)
            {
                return ById(Id);
            }

            return null;
        }

        public static Page Create(PageBase page)
        {
            SqlBuilder builder = TypeBuilder.Insert<PageBase>(page);
            var result = builder.Execute();
            if (result.Count == 1)
            {
                return ById(page.Id, false);
            }
            return null;
        }

        public static bool Update(Page page, string[] Properties = null)
        {
            SqlBuilder builder = TypeBuilder.Update<Page>(page, "PageBase", Properties: Properties);
            return builder.ExecuteNonQuery() == 1;
        }

        public static bool Publish(Page page)
        {
            Page pp = ById(page.Id);
            SqlBuilder builder;
            if (!page.PageTypeId.HasValue)
            {
                if (string.IsNullOrEmpty(page.Model) && !string.IsNullOrEmpty(page.Html))
                {
                    page.Model = page.Html;
                }
            }
            if (pp == null)
            {
                builder = TypeBuilder.Insert<Page>(page, "PublishedPageBase");
            }
            else
            {
                builder = TypeBuilder.Update<Page>(page, "PublishedPageBase");
            }

            return builder.ExecuteNonQuery() == 1;
        }
    }
}
