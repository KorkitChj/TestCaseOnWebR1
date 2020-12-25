using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebTest2.Models;

namespace WebTest2.Controllers
{
    public class AddItemsController : Controller
    {
        // GET: AddItems
        public ActionResult Display_Items_Factors(int id, int module_id, string module_name)
        {
            using (var context = new Models.DBModels())
            {
                ViewBag.Module_id = module_id;
                ViewBag.MName = module_name;
                ViewBag.Fnid = id;
                listItems listItems = new listItems();
                listItems.items_In_Factor_Table = new items_in_factor_table();
                listItems.items_In_Factor_Tables = context.items_in_factor_table.Where(u => u.fn_id == id).ToList();
                //var result = context.items_in_factor_table.Where(u => u.fn_id == id).ToList();
                return View(listItems);
            }
        }
        public Boolean check_duplicate_items_factors(string item, int fn_id)
        {
            using (var context = new DBModels())
            {
                bool result = context.items_in_factor_table.Any(u => u.item == item && u.fn_id == fn_id);
                return result;
            }
        }
        public ActionResult Add_New_Items_Factors(listItems model)
        {
            String status = "false";
            String message = "";        
            string module_name = "";
            int module_id = 0;
            if (ModelState.IsValid)
            {
                using (var context = new DBModels())
                {                   
                    try
                    {
                        if (model.items_In_Factor_Table.item == "")
                        {
                            return Json(new { status = "false", message = "Items is Empty" });
                        }
                        int fn_id = Int32.Parse(Request["fn_id"]);
                        var factor_data = context.factors_name_table.Where(s => s.fn_id == fn_id).ToList();
                        module_id = factor_data[0].module_id;
                        var module_name2 = context.list_module_name_table.Where(s => s.module_id == module_id).ToList();
                        module_name = module_name2[0].name_of_module;
                        bool res = check_duplicate_items_factors(model.items_In_Factor_Table.item,fn_id);
                        if (res)
                        {                     
                            return Json(new { status = "false", message = "Already have Item Factor" });
                        }
                        else
                        {
                            //add factor name to factor table
                            model.items_In_Factor_Table.fn_id = fn_id;
                            context.items_in_factor_table.Add(model.items_In_Factor_Table);
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
                //TempData["MessageIF"] = message;
                //TempData["StatusIF"] = status;
                return Json(new { status = status, message = message });
            }
            else
            {
                //ViewBag.Message = message;
                //ViewBag.Status = status;
                return Json(new { status = status, message = message });
            }
        }
        public ActionResult Delete_Items_Factors(string id, string fn_id, string module_id, string module_name)
        {
            String status = "false";
            String message = "";
            using (var context = new DBModels())
            {
                try
                {
                    int id2 = Int32.Parse(id);
                    int fn_id2 = Int32.Parse(fn_id);
                    var itemToRemove = context.items_in_factor_table.SingleOrDefault(x => x.iif_id == id2 && x.fn_id == fn_id2);

                    if (itemToRemove != null)
                    {
                        context.items_in_factor_table.Remove(itemToRemove);
                        var response = context.SaveChanges();
                        status = "true";
                        message = "Successfully";
                    }
                    else
                    {
                        status = "false";
                        message = "iff_id not found";
                    }
                }
                catch (Exception ex)
                {
                    status = "false";
                    message = ex.Message;
                }
            }
            return Json(new { status = status, message = message });
        }
    }
}