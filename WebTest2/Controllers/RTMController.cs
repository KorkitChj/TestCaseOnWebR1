using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebTest2.Models;

namespace WebTest2.Controllers
{
    public class RTMController : Controller
    {
        public ActionResult RTM(int id, string module_name)
        {

            using (var context = new DBModels())
            {
                //var rtm_data = context.rtm_table.Where(s => s.module_id == id).ToList();
                var listRTM = new listRTM();
                listRTM.rtm_Table = new rtm_table();
                listRTM.rtm_Tables = context.rtm_table.Where(s => s.module_id == id).ToList();
                ViewBag.Module_id = id;
                ViewBag.MName = module_name;
                return View(listRTM);
            }

        }
        [HttpPost]
        public ActionResult Add_New_RTM(listRTM model)
        {
            String status = "false";
            String message = "";
            using (var context = new DBModels())
            {
                if (ModelState.IsValid)
                {                   
                    try
                    {
                        
                        string module_id = Request["module_id"];
                        int module_id_2 = Int32.Parse(module_id);
                        string module_name1 = "";
                        var module_name = context.list_module_name_table.Where(s => s.module_id == module_id_2).ToList();
                        module_name1 = module_name[0].name_of_module;
                        if (model.rtm_Table.comments == "" || model.rtm_Table.comments == null)
                        {
                            model.rtm_Table.comments = "Null";
                        }
                        model.rtm_Table.module_id = module_id_2;
                        context.rtm_table.Add(model.rtm_Table);
                        var response = context.SaveChanges();

                        status = "true";
                        message = "Successfully";
                    }
                    catch (Exception ex)
                    {
                        /*DbEntityValidationException e
                        foreach (var eve in e.EntityValidationErrors)
                        {
                            Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                                eve.Entry.Entity.GetType().Name, eve.Entry.State);
                            foreach (var ve in eve.ValidationErrors)
                            {
                                Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                    ve.PropertyName, ve.ErrorMessage);
                            }
                        }
                        throw;
                        */
                        return Json(new { status = status, message = ex.Message });
                    }                    
                    return Json(new { status = status, message = message });
                }
                else
                {
                    return Json(new {
                        status = status,
                        message = string.Join("; ", ModelState.Values
                                        .SelectMany(x => x.Errors)
                                        .Select(x => x.ErrorMessage))
                    });
                }
            }            
        }

        public ActionResult Edit_RTM()
        {
            String status = "false";
            String message = "";                               
                using (var context = new DBModels())
                {
                    try
                    {
                        int module_id = Int32.Parse(Request["module_id"]);                       
                        int req_id = Int32.Parse(Request["req_id"]);
                        string des = Request["item.value.req_description"];
                        int desing_spec = Int32.Parse(Request["item.value.design_specification_reference_number"]);
                        string pro_module = Request["item.value.program_module"];
                        int test_refer = Int32.Parse(Request["item.value.test_reference_number"]);
                        string statusr = Request["item.value.status"];
                        DateTime status_date = DateTime.Parse(Request["item.value.status_date"]);
                        string comment = Request["item.value.comments"];
                        if (comment == "" || comment == null)
                        {
                            comment = "Null";
                        }
                    var result = context.rtm_table.First(a => a.module_id == module_id && a.req_id == req_id);
                        if (result != null)
                        {
                            result.req_description = des;
                            result.program_module = pro_module;
                            result.status = statusr;
                            result.status_date = status_date;
                            result.test_reference_number = test_refer;
                            result.design_specification_reference_number = desing_spec;
                            result.comments = comment;
                            var response = context.SaveChanges();
                            status = "true";
                            message = "Successfully";
                        }                 
                    }
                    catch (Exception ex)
                    {
                        status = "false";
                        message = ex.Message;
                    }
                }         
            if (status == "true")
            {
                return Json(new { status = status, message = message });
            }
            else
            {
                return Json(new { status = status, message = message });
            }
        }
        public ActionResult Delete_RTM(string id ,string rtm_data, string mid)
        {
            String status = "false";
            String message = "";
            using (var context = new DBModels())
            {
                try
                {
                    int id2 = Int32.Parse(id);
                    int module_id = Int32.Parse(mid);
                    var itemToRemove = context.rtm_table.SingleOrDefault(x => x.req_id == id2 && x.module_id == module_id);

                    if (itemToRemove != null)
                    {
                        context.rtm_table.Remove(itemToRemove);
                        var response = context.SaveChanges();
                        status = "true";
                        message = "Successfully";
                    }
                    else
                    {
                        return Json(new { status = status, message = "req_id && module_id not found" });
                        //TempData["Message"] = "req_id && module_id not found";
                        //TempData["Status"] = status;
                    }


                }
                catch (Exception ex)
                {
                    status = "false";
                    message = ex.Message;
                }
            }
            //TempData["MessageRTM"] = message;
            //TempData["StatusRTM"] = status;
            return Json(new { status = status, message = message });
        }
    }
}