using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Http.Results;
using System.Net;
using WebTest2.Models;
using WebTest2.Controllers;

namespace WebTest2.Tests
{
    [TestClass]
    public class TestModuleControllerTest
    {
        [TestMethod]
        public void PostModule_ShouldReturnSameModule()
        {
            var controller = new list_module_name_tableController(new TestModuleAppContext());

            var item = GetDemoModule();

            var result =
                controller.Postlist_module_name_table(item) as CreatedAtRouteNegotiatedContentResult<list_module_name_table>;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.RouteName, "DefaultApi");
            Assert.AreEqual(result.RouteValues["id"], result.Content.module_id);
            Assert.AreEqual(result.Content.name_of_module, item.name_of_module);
        }

        [TestMethod]
        public void PutModule_ShouldReturnStatusCode()
        {
            var controller = new list_module_name_tableController(new TestModuleAppContext());

            var item = GetDemoModule();

            var result = controller.Putlist_module_name_table(item.module_id, item) as StatusCodeResult;
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(StatusCodeResult));
            Assert.AreEqual(HttpStatusCode.NoContent, result.StatusCode);
        }

        [TestMethod]
        public void PutModule_ShouldFail_WhenDifferentID()
        {
            var controller = new list_module_name_tableController(new TestModuleAppContext());

            var badresult = controller.Putlist_module_name_table(999, GetDemoModule());
            Assert.IsInstanceOfType(badresult, typeof(BadRequestResult));
        }

        [TestMethod]
        public void GetModule_ShouldReturnModuleWithSameID()
        {
            var context = new TestModuleAppContext();
            context.list_Module_Name_Tables.Add(GetDemoModule());

            var controller = new list_module_name_tableController(context);
            var result = controller.Getlist_module_name_table(3) as OkNegotiatedContentResult<list_module_name_table>;

            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Content.module_id);
        }

        [TestMethod]
        public void GetModule_ShouldReturnAllModule()
        {
            var context = new TestModuleAppContext();
            context.list_Module_Name_Tables.Add(new list_module_name_table { module_id = 1, name_of_module = "Module 1" });
            context.list_Module_Name_Tables.Add(new list_module_name_table { module_id = 2, name_of_module = "Module 2" });
            context.list_Module_Name_Tables.Add(new list_module_name_table { module_id = 3, name_of_module = "Module 3" });

            var controller = new list_module_name_tableController(context);
            var result = controller.Getlist_module_name_table() as TestModuleDbSet;

            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Local.Count);
        }

        [TestMethod]
        public void DeleteModule_ShouldReturnOK()
        {
            var context = new TestModuleAppContext();
            var item = GetDemoModule();
            context.list_Module_Name_Tables.Add(item);

            var controller = new list_module_name_tableController(context);
            var result = controller.Deletelist_module_name_table(3) as OkNegotiatedContentResult<list_module_name_table>;

            Assert.IsNotNull(result);
            Assert.AreEqual(item.module_id, result.Content.module_id);
        }

        list_module_name_table GetDemoModule()
        {
            return new list_module_name_table() { module_id = 3, name_of_module = "Web Appliction 1" };
        }
    }
}