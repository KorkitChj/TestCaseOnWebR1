using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebTest2.Models;

namespace WebTest2.Controllers
{
    public class AddFactorsController : Controller
    {
        // GET: AddFactors
        public ActionResult Display_Factors(int id, string module_name)
        {
            using (var context = new DBModels())
            {
                /*Get data from another controller.
                 * var controller = DependencyResolver.Current.GetService<FactorsController>();
                 * controller.ControllerContext = new ControllerContext(this.Request.RequestContext, controller);
                 * int cal = controller.Calculate_Items(id);
                 * ViewBag.TotalFactor = cal;
                 */
                listFactors listF = new listFactors();
                listF.factors_Name_Table = new factors_name_table();
                listF.factors_Name_Tables = context.factors_name_table.Where(s => s.module_id == id).ToList();
                ViewBag.Module_id = id;
                ViewBag.MName = module_name;                                             
                return View(listF);
            }
        }
        //ยังไม่เพิ่มในเอกสาร
        public Boolean check_duplicate_factors_name(string factor_name, int module_id)
        {
            using (var context = new DBModels())
            {
                bool result = context.factors_name_table.Any(u => u.fn_name == factor_name && u.module_id == module_id);
                return result;
            }

        }
        public ActionResult Add_new_Factors(listFactors model)
        {
            String status = "false";
            String message = "form is not valid";       
            string module_name1 = "";
            if (ModelState.IsValid)
            {
                using (var context = new DBModels())
                {                       
                    try
                    {
                        if(model.factors_Name_Table.fn_name == "")
                        {
                            return Json(new { status = "false", message = "Factors name is Empty" });
                        }
                        int module_id = Int32.Parse(Request["module_id"]);
                        var module_name = context.list_module_name_table.Where(s => s.module_id == module_id).ToList();
                        module_name1 = module_name[0].name_of_module;
                        //case sensitive ยังไม่เขียนในเอกสาร
                        bool res = check_duplicate_factors_name(model.factors_Name_Table.fn_name, module_id);
                        if (res)
                        {
                            return Json(new { status = "false", message = "Already have Factor" });
                        }
                        else
                        {
                            model.factors_Name_Table.module_id = module_id;
                            context.factors_name_table.Add(model.factors_Name_Table);
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
                    status = "false",
                    message = string.Join("; ", ModelState.Values
                                                        .SelectMany(x => x.Errors)
                                                        .Select(x => x.ErrorMessage))
                });
            }
            if (status == "true")
            {
                //TempData["MessageTC"] = message;
                //TempData["StatusTC"] = status;
                return Json(new { status = status, message = message });
            }
            else
            {
                //ViewBag.Message = message;
                //ViewBag.Status = status;
                return Json(new { status = status, message = message });
            }
        }
        public ActionResult Delete_Factors(string id, string module_id, string module_name)
        {
            String status = "false";
            String message = "";            
            using (var context = new DBModels())
            {
                try
                {
                    int id2 = Int32.Parse(id);
                    int module_id2 = Int32.Parse(module_id);
                    var itemToRemove = context.factors_name_table.SingleOrDefault(x => x.fn_id == id2);

                    if (itemToRemove != null)
                    {
                        context.factors_name_table.Remove(itemToRemove);
                        var response = context.SaveChanges();
                        status = "true";
                        message = "Successfully";
                    }
                    else
                    {
                        status = "false";
                        message = "fn_id not found";
                    }
                }
                catch (Exception ex)
                {
                    status = "false";
                    message = ex.Message;
                }
            }
            //TempData["MessageTC"] = message;
            //TempData["StatusTC"] = status;
            return Json(new { status = status, message = message });
        }
    }
}