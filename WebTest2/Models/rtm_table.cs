//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebTest2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class rtm_table
    {
        public int req_id { get; set; }
        [Required]
        [StringLength(255)]
        public string req_description { get; set; }
        [Required]
        [Range(1, 1000, ErrorMessage = "Only integer numbers and maximum is 1000.")]
        public int design_specification_reference_number { get; set; }
        [Required]
        [StringLength(50)]
        public string program_module { get; set; }
        [Required]
        [Range(1, 1000, ErrorMessage = "Only integer numbers and maximum is 1000.")]
        public int test_reference_number { get; set; }
        [Required]
        [StringLength(50)]
        public string status { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public System.DateTime? status_date { get; set; }
        [StringLength(255)]
        public string comments { get; set; }
        public int module_id { get; set; }
    
        public virtual list_module_name_table list_module_name_table { get; set; }
    }
}
