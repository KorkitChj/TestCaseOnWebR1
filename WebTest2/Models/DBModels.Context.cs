﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using WebTest2.Controllers;

    public class DBModels : DbContext, WebTest2Context
    {
        public DBModels()
            : base("name=DBModels")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<defect_log> defect_log { get; set; }
        public virtual DbSet<factors_name_table> factors_name_table { get; set; }
        public virtual DbSet<items_in_factor_table> items_in_factor_table { get; set; }
        public virtual DbSet<list_module_name_table> list_module_name_table { get; set; }
        public virtual DbSet<rtm_table> rtm_table { get; set; }
        public virtual DbSet<test_case_table> test_case_table { get; set; }
        public virtual DbSet<test_scenario_table> test_scenario_table { get; set; }
        //Test
        public DbSet<list_module_name_table> list_Module_Name_Tables { get; set; }
        public void MarkAsModified(list_module_name_table item)
        {
            Entry(item).State = EntityState.Modified;
        }
    }
}
