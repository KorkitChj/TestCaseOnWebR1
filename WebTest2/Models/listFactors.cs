using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebTest2.Models
{
    public class listFactors
    {
        public IEnumerable<factors_name_table> factors_Name_Tables { get; set; }
        public factors_name_table factors_Name_Table { get; set; }
    }
}