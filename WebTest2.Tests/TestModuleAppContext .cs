using System;
using System.Data.Entity;
using WebTest2.Models;

namespace WebTest2.Tests
{
    public class TestModuleAppContext : WebTest2Context
    {
        public TestModuleAppContext()
        {
            this.list_Module_Name_Tables = new TestModuleDbSet();
        }

        public DbSet<list_module_name_table> list_Module_Name_Tables { get; set; }

        public int SaveChanges()
        {
            return 0;
        }

        public void MarkAsModified(list_module_name_table item) { }
        public void Dispose() { }
    }
}