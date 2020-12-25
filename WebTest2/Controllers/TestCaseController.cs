using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebTest2.Models;

namespace WebTest2.Controllers
{
    public class TestCaseController : Controller
    {
        // GET: TestCase
        public ActionResult Display_TestCase(int id, string module_name)
        {
            using (var context = new DBModels())
            {
                ViewBag.Module_id = id;
                ViewBag.MName = module_name;
                var rtm_data = context.test_case_table.Where(s => s.module_id == id).ToList();
                return View(rtm_data);
            }
        }
        public ActionResult PrintPDF()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add_TestCase(test_case_table test_Case_Table)
        {
            string status = "false";
            string message = "";
            string module_name = "";
            int module_id = 0;
            try
            {               
                using (var context = new DBModels())
                {
                    module_id = test_Case_Table.module_id;
                    var module_name2 = context.list_module_name_table.Where(s => s.module_id == test_Case_Table.module_id).ToList();
                    module_name = module_name2[0].name_of_module;
                    List<string> imageNameWithDate = new List<string>();
                    List<string> upload = new List<string>();
                    if (Request.Files.Count > 0)
                    {
                        int i = 0;
                        HttpPostedFileBase file;
                        List<string> imageName = new List<string>();
                        List<string> imageNameNoExtention = new List<string>();
                        List<string> Extention = new List<string>();                      
                        //string UploadPath = ConfigurationManager.AppSettings["UserImagePath"].ToString();
                        
                        //string upload = "";
                        while (i < Request.Files.Count)
                        {
                            file = Request.Files[i];
                            if (file.ContentLength > 0)
                            {
                                imageName.Add(Path.GetFileName(file.FileName));                                
                                imageNameNoExtention.Add(Path.GetFileNameWithoutExtension(imageName[i]));
                                Extention.Add(Path.GetExtension(imageName[i]));
                                imageNameWithDate.Add(DateTime.Now.ToString("yyyyMMdd") + "-" + imageNameNoExtention[i].Trim() + Extention[i]);
                                string filePath = "~/UserImages/" + imageNameWithDate[i];
                                file.SaveAs(Server.MapPath(filePath));
                                upload.Add(filePath);
                                //upload.Add(UploadPath + imageNameWithDate[i]);
                                //file.SaveAs(upload[i]);
                            }
                            i++;
                        }
                    }
                    var resultA = context.test_case_table.First(a => a.test_case_id == test_Case_Table.test_case_id && a.module_id == test_Case_Table.module_id);
                    if (resultA != null)
                    {
                        string testcase = Request["item.test_case_data"];
                        string expected_result = Request["item.expected_result"];
                        DateTime date = DateTime.Parse(Request["item.date"]);
                        string testround = Request["item.test_round"];
                        string result = Request["item.result"];
                        string test_by = Request["item.test_by"];
                        string factors2 = Request["factors_data"];
                        resultA.test_case_data = testcase;
                        resultA.expected_result = expected_result;
                        resultA.date = date;
                        resultA.result = result;
                        resultA.test_by = test_by;
                        resultA.test_round = testround;
                        resultA.module_id = test_Case_Table.module_id;
                        resultA.test_case_id = test_Case_Table.test_case_id;
                        resultA.factors2 = factors2;                        
                        if(Request.Files.Count == 2)
                        {
                            if (!upload.Any())
                            {
                                //resultA.path_test_case = upload[0];
                                //resultA.path_expected_result = upload[1];
                            }
                            else
                            {
                                resultA.path_test_case = upload[0];
                                resultA.path_expected_result = upload[1];
                            }                       
                        }
                                             
                        var response = context.SaveChanges();
                        status = "true";
                        message = "Successfully";
                    }
                }
                
            }catch(Exception ex)
            {
                //DbEntityValidationException e
                /*foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;*/
                status = "false";
                message = ex.Message;
            }
            if (status == "true")
            {
                ViewBag.DataExists = message;
                //return Json(new { status = status, message = message });
            }
            else
            {
                ViewBag.DataExists = message;
                //return Json(new { status = status, message = message });
            }
            TempData["msg"] = "<script>alert('"+message+"');</script>";
            return RedirectToAction("Display_TestCase", new { id = module_id, module_name = module_name });
        }
        /*public ActionResult Edit_TestCase()
        {
            return View();
        }*/
    }
}