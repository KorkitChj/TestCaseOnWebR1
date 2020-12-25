using System;
using System.Data.Entity;

namespace WebTest2.Models
{
    public interface WebTest2Context : IDisposable
    {
        DbSet<list_module_name_table> list_Module_Name_Tables { get; }
        int SaveChanges();
        void MarkAsModified(list_module_name_table item);
    }
}