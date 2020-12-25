using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using WebTest2.Models;

namespace WebTest2.Controllers
{
    public class LandingController : Controller
    {
        // GET: Landing
        public ActionResult Index()
        {
            using (var context = new DBModels())
            {
                var listMN = new listModuleName();
                listMN.list_Module_Name_ = new list_module_name_table();
                listMN.list_Module_Name_Tables = context.list_module_name_table.ToList();
                return View(listMN);
            }
        }
        /*public Boolean Check_Specialchar(string module_name)
        {
            string pattern = @"(\s+|@|&|!|\$|%|\^|#)";
            var regexItem = new Regex(pattern);
            if (regexItem.IsMatch(module_name)) {return true;}return false;
        }*/
        public Boolean Check_Specialchar(string module_name)
        {
            var withoutSpecial = new string(module_name.Where(c => Char.IsLetterOrDigit(c)
                                            || Char.IsWhiteSpace(c)).ToArray());

            if (module_name != withoutSpecial)
            {
                return true;
            }
            return false;
        }

        //check_duplicate_test_case_name()
        public Boolean Check_Duplicate_Module_Name(string case_name)
        {
            using (var context = new DBModels())
            {
                bool result = context.list_module_name_table.Any(u => u.name_of_module == case_name);
                return result;
            }

        }
        
        [HttpPost]
        public ActionResult Add_Module_name(listModuleName model)
        {
            String status = "false";
            String message = "";
            if (ModelState.IsValid)
            {
                using (var context = new DBModels())
                {
                    try
                    {

                        //string module_name = Regex.Replace(model.list_Module_Name_.name_of_module, @"(\s+|@|&|!|\$|%|\^|#)", "");
                        if (Check_Specialchar(model.list_Module_Name_.name_of_module) == true)
                        {
                            return Json(new { status = "false", message = "Special Char is invalid" });
                        }
                        //model.list_Module_Name_.name_of_module = module_name;
                        bool res = Check_Duplicate_Module_Name(model.list_Module_Name_.name_of_module);
                        if (res)
                        {
                            return Json(new { status = "false", message = "Already have modules" });
                        }
                        else
                        {
                            //add module to list_module
                            context.list_module_name_table.Add(model.list_Module_Name_);
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
            }
            else
            {
                return Json(new
                {
                    status = status,
                    message = string.Join("; ", ModelState.Values
                                        .SelectMany(x => x.Errors)
                                        .Select(x => x.ErrorMessage))
                });
            }
            if (status == "true")
            {
                //TempData["Message"] = message;
                //TempData["Status"] = status;
                return Json(new { status = status, message = message });
            }
            else
            {
                //ViewBag.Message = message;
                //ViewBag.Status = status;
                return Json(new { status = status, message = message });
            }
        }
        
        [HttpPost]
        public ActionResult Edit_Module_name(listModuleName model)
        {
            String status = "false";
            String message = "";
            if (ModelState.IsValid)
            {
                using (var context = new DBModels())
                {
                    try
                    {
                        //string module_name = Regex.Replace(model.list_Module_Name_.name_of_module, @"(\s+|@|&|!|\$|%|\^|#)", "");
                        if (Check_Specialchar(model.list_Module_Name_.name_of_module) == true) { 
                            return Json(new { status = "false", message = "Special Char is invalid" });
                        }
                        //model.list_Module_Name_.name_of_module = module_name;
                        bool res = Check_Duplicate_Module_Name(model.list_Module_Name_.name_of_module);
                        if (res)
                        {
                            //ViewBag.status = "false";
                            //ViewBag.Message = "Already have modules";
                            return Json(new { status = "false", message = "Already have modules" });
                        }
                        else
                        {
                            string module_id = Request["module_id"];
                            int module_id_int = Int32.Parse(module_id);
                            var result = context.list_module_name_table.First(a => a.module_id == module_id_int);
                            if (result != null)
                            {
                                result.name_of_module = model.list_Module_Name_.name_of_module;
                                var response = context.SaveChanges();
                                status = "true";
                                message = "Successfully";
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        status = "false";
                        message = ex.Message;
                    }
                }
            }
            else
            {
                return Json(new
                {
                    status = status,
                    message = string.Join("; ", ModelState.Values
                                                        .SelectMany(x => x.Errors)
                                                        .Select(x => x.ErrorMessage))
                });
            }
            if (status == "true")
            {
                //TempData["Message"] = message;
                //TempData["Status"] = status;
                return Json(new { status = status, message = message });
            }
            else
            {
                //ViewBag.Message = message;
                //ViewBag.Status = status;
                return Json(new { status = status, message = message });
            }
        }

        [HttpPost]
        public ActionResult Delete_Module_name(string id, string module_name)
        {
            String status = "false";
            String message = "";
            var id2 = Int32.Parse(id);
            using (var context = new DBModels())
            {
                try
                {
                    var itemToRemove = context.list_module_name_table.SingleOrDefault(x => x.module_id == id2 && x.name_of_module == module_name);

                    if (itemToRemove != null)
                    {
                        context.list_module_name_table.Remove(itemToRemove);
                        var res = context.SaveChanges();
                        status = "true";
                        message = "Successfully";
                    }
                    else
                    {
                        //TempData["Message"] = "module_id && module_name not found";
                        //TempData["Status"] = status;
                        //ยังไม่ส่งอะไรไปแสดง
                        return Json(new { status = status, message = "module_id && module_name not found" });
                    }


                }
                catch (Exception ex)
                {
                    status = "false";
                    message = ex.Message;
                }
            }
            //TempData["Message"] = message;
            //TempData["Status"] = status;
            return Json(new { status = status, message = message });          
        }


    }

    
}