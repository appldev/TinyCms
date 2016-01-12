using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyCms.Interfaces
{
    public interface IListProvider
    {
        IEnumerable<ListItem> GetListItems(Models.Field field, string selectedValue);
    }

    public class ListItem
    {
        string Text { get; set; }
        string Value { get; set; }
        bool Selected { get; set; }
        bool Disabled { get; set; }
        
    }
}
