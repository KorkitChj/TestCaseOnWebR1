using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebTest2.Models
{
    public class listRTM
    {       
        public IEnumerable<rtm_table> rtm_Tables { get; set; }
        public rtm_table rtm_Table { get; set; }   
    }
}