using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebTest2.Models
{
    public class listItems
    {
        public IEnumerable<items_in_factor_table> items_In_Factor_Tables { get; set; }
        public items_in_factor_table items_In_Factor_Table { get; set; }
    }
}