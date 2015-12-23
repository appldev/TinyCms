using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinySql;

namespace TinyCms.Models
{
    public partial class Library
    {
        private List<LibraryFolder> _Folders = null;
        private List<LibraryItem> _Items = null;

        public List<LibraryFolder> Folders
        {
            get
            {
                if (_Folders == null && !this.Id.Equals(Guid.Empty))
                {
                    _Folders = LibraryFolder.ByLibrary(this.Id);
                }
                return _Folders;
            }

            set
            {
                _Folders = value;
            }
        }

        public List<LibraryItem> Items
        {
            get
            {
                if (_Items == null && !this.Id.Equals(Guid.Empty))
                {
                    _Items = LibraryItem.ByLibrary(this.Id);
                }
                return _Items;
            }

            set
            {
                _Items = value;
            }
        }

        public static Library Load(Guid Id)
        {
            SqlBuilder builder = TinySql.TypeBuilder.Select<Library>(ExcludeProperties:new string[] { "Items","Folders"})
                .BaseTable()
                .Where<Guid>("Library","Id", SqlOperators.Equal,Id)
                .Builder();

            return builder.List<Library>().First();
            


        }


    }
}
