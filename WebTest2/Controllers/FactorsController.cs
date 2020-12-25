using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebTest2.Models;

namespace WebTest2.Controllers
{
    public class FactorsController : Controller
    {
        public static List<string> MyListCheck { get; set; }

        public int? module_id;
        // GET: Factors
        public ActionResult Display_Factors_Items(int id, string module_name)
        {
            using (var context = new DBModels())
            {
                try
                {
                    module_id = id;
                    if (module_id == null) {
                        TempData["msg"] = "<script>alert('Module id is not found');</script>";
                        return RedirectToAction("Index","Landing");
                    }
                    ViewBag.Module_id = id;
                    ViewBag.MName = module_name;
                    var factor_name = context.factors_name_table.Where(s => s.module_id == id).ToList();
                    if (module_id == 0)
                    {
                        TempData["msg"] = "<script>alert('Module id is invalid');</script>";
                        return RedirectToAction("Index", "Landing");
                    }
                    List<string> factors_name = new List<string>();
                    List<string> factors_item = new List<string>();
                    List<string> factors_item2 = new List<string>();

                    foreach (var fn_name in factor_name)
                    {
                        factors_name.Add(fn_name.fn_name);
                        foreach (var item in fn_name.items_in_factor_table)
                        {
                            factors_item.Add(fn_name.fn_name + "_" + item.item);
                        }
                    }
                    factors_name.Sort();
                    factors_item.Sort();
                    FactorsController.MyListCheck = new List<string>();
                    foreach (var x in factors_name.Select((value, index) => new { value, index }))
                    {                        
                        var index = x.index;
                        MyListCheck.Add(index.ToString());
                        foreach (var item2 in factors_item)
                        {
                            if (item2.Contains(x.value))
                            {
                                factors_item2.Add(item2 + "_" + index);                               
                            }
                        }

                    }
                    
                    List<string> listaz = new List<string>()
                    {
                    "A","B","C","D","E","F","G","H","I","J","K","L","M","N","O","P","Q","R","S","T","U","V","W","X","Y","Z"
                    };
                    List<string> data_header = new List<string>();
                    for (int p = 0; p < MyListCheck.Distinct().ToList().Count; p++)
                    {
                        for (int t = 0; t < listaz.Count; t++)
                        {
                            if(p == t)
                            {
                                data_header.Add(listaz[t]);
                            }
                            
                        }
                    }
                    
                    ViewBag.listaz = data_header;
                    ViewBag.factors_name = factors_name;
                    ViewBag.factors_item = factors_item2;                   
                    int cal = Calculate_Items(id);
                    ViewBag.TotalFactor = cal;
                    
                }
                catch(Exception Ex)
                {
                    TempData["msg"] = "<script>alert('"+ Ex.Message +"');</script>";
                }
                return View();
            }
        }
        public int Calculate_Items(int id)
        {
            int cal = 1;
            using (var context = new DBModels())
            {
                var data = (from a in context.factors_name_table
                            join b in context.items_in_factor_table
                            on a.fn_id equals b.fn_id
                            where a.module_id == id
                            group a by new { a.fn_id } into g
                            orderby g.Count() descending
                            select new
                            {
                                count = g.Count()
                            }).ToList();

                if (data.Count > 0)
                {
                    foreach (var item in data)
                    {

                        cal *= item.count;                       
                    }
                }
                else
                {
                    cal = 0;
                }
            }

            return cal;
        }
        
    }
}