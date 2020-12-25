using System;
using System.Linq;
using WebTest2.Models;
using WebTest2.Tests;

namespace WebTest2.Tests
{
    class TestModuleDbSet : TestDbSet<list_module_name_table>
    {
        public override list_module_name_table Find(params object[] keyValues)
        {
            return this.SingleOrDefault(module => module.module_id == (int)keyValues.Single());
        }
    }
}