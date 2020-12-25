using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using WebTest2.Models;

namespace WebTest2.Controllers
{
    public class GenerateTestController : Controller
    {
        public static List<string> MyListCheck { get; set; }
        public static List<string> Factors_Description { get; set; }
        public static List<int> tid { get; set; }

        public int? module_id;
        // GET: GenerateTest
        public ActionResult Display_TestScenario(int id, string module_name)
        {
            using (var context = new DBModels())
            {
                //listFactors listF = new listFactors();
                //listF.factors_Name_Table = new factors_name_table();
                //listF.factors_Name_Tables = context.factors_name_table.Where(s => s.module_id == id).ToList();
                /*List<string> listaz = new List<string>()
                {
                    "A","B","C","D","E","F","G","H","I","J","K",
                    "L","M","N","O","P","Q","R","S","T","U","V","W","X","Y","Z"                   
                };*/
                module_id = id;               
                var data = context.test_scenario_table.Where(s => s.module_id == id).ToList();      
                GenerateTestController.Factors_Description = new List<string>();
                List<string> listEmpty = new List<string>();
                //List<string> factors_description = new List<string>();
                dynamic factors_data = createDataforMapItems(id);
                dynamic mapList = mapItems(0, factors_data, listEmpty);

                List<string> listModify = new List<string>();
                List<string> indexp = new List<string>();
                foreach (var data2 in SplitST(data).Select((value, i) => new { i, value }))
                {
                    //listModify.Add(data2.i.ToString());
                    foreach (var data3 in data2.value)
                    {
                        //Regex.Replace(data3, @"\s+", "");
                        if(data3 == "") { continue; }
                        if(data3 != "" && !data3.Contains("Mid") && !data3.Contains("Tid")) { 
                            listModify.Add(data3.Trim());
                            //if (data3 == "Null") { continue; }                           
                        }                       
                        if (data2.value.IndexOf(data3) == data2.value.Count - 1)
                        {
                            string resultString = "";
                            string resultString2 = "";
                            string[] values;
                            //string resultString3 = "";
                            if (data3.Contains("Mid") && data3.Contains("Tid"))
                            {
                                values = data3.Split(',');
                                for (int i = 0; i < values.Length; i++)
                                {
                                    values[i] = values[i].Trim();
                                }
                                resultString = Regex.Match(values[0], @"\d+").Value;
                                resultString2 = Regex.Match(values[1], @"\d+").Value;
                                listModify.Add("Edit," + "Mid=" + resultString + ",Tid=" + resultString2 +","+ values[2]);
                                listModify.Add("Link," + "Mid=" + resultString + ",Tid=" + resultString2);
                                //resultString3 = Regex.Match(values[2], @"\d+").Value;
                            }
                            
                            listModify.Add("|");
                        }
                    }
                }
                //GenerateTestController.Factors_Description = new List<string>();                
                ViewBag.listaz = MyListCheck.Distinct().ToList();
                ViewBag.Module_id = id;
                ViewBag.MName = module_name;
                ViewBag.FactorsData = listModify;
                ViewBag.ItemData = Factors_Description.ToList();
                return View();
            }               
        }
        private IEnumerable<List<string>> SplitST(List<test_scenario_table> list)
        {
            //String items_Name_ = "";
            List<string> factors_Name_ = new List<string>();
            //List<string> items_Name_ = new List<string>();
            GenerateTestController.MyListCheck = new List<string>();
            foreach (test_scenario_table data3 in list)
            {
                //int ind = 0;
                foreach (var factors_Name in data3.list_module_name_table.factors_name_table)
                {
                    factors_Name_.Add(factors_Name.fn_name);                   
                    /*foreach (var item_Name in factors_Name.items_in_factor_table.Reverse())
                    {
                        items_Name_.Add(item_Name.item);
                        //++ind;
                        break;
                    }*/
                }
                //if (items_Name_.Any()) { items_Name_.Distinct();  /*items_Name_.Sort();*/ }
                if (!MyListCheck.Any()) { factors_Name_.Sort(); }
                List<string> stringList = data3.factors.Split(',').ToList();
                for (int i = 0; i < stringList.Count; i++)
                {
                    if(stringList[i] == "") { continue; }
                    if (stringList[i].Contains("A")) { MyListCheck.Add($"A:{factors_Name_[0]}"); }
                    else if (stringList[i].Contains("B")) { MyListCheck.Add($"B:{factors_Name_[1]}"); }
                    else if (stringList[i].Contains("C")) { MyListCheck.Add($"C:{factors_Name_[2]}"); }
                    else if (stringList[i].Contains("D")) { MyListCheck.Add($"D:{factors_Name_[3]}"); }
                    else if (stringList[i].Contains("E")) { MyListCheck.Add($"E:{factors_Name_[4]}"); }
                    else if (stringList[i].Contains("F")) { MyListCheck.Add($"F:{factors_Name_[5]}"); }
                    else if (stringList[i].Contains("G")) { MyListCheck.Add($"G:{factors_Name_[6]}"); }
                    else if (stringList[i].Contains("H")) { MyListCheck.Add($"H:{factors_Name_[7]}"); }
                    else if (stringList[i].Contains("I")) { MyListCheck.Add($"I:{factors_Name_[8]}"); }
                    else if (stringList[i].Contains("J")) { MyListCheck.Add($"J:{factors_Name_[9]}"); }
                    else if (stringList[i].Contains("K")) { MyListCheck.Add($"K:{factors_Name_[10]}"); }
                    else if (stringList[i].Contains("L")) { MyListCheck.Add($"L:{factors_Name_[11]}"); }
                    else if (stringList[i].Contains("M")) { MyListCheck.Add($"M:{factors_Name_[12]}"); }
                    else if (stringList[i].Contains("N")) { MyListCheck.Add($"N:{factors_Name_[13]}"); }
                    else if (stringList[i].Contains("O")) { MyListCheck.Add($"O:{factors_Name_[14]}"); }
                    else if (stringList[i].Contains("P")) { MyListCheck.Add($"P:{factors_Name_[15]}"); }
                    else if (stringList[i].Contains("Q")) { MyListCheck.Add($"Q:{factors_Name_[16]}"); }
                    else if (stringList[i].Contains("R")) { MyListCheck.Add($"R:{factors_Name_[17]}"); }
                    else if (stringList[i].Contains("S")) { MyListCheck.Add($"S:{factors_Name_[18]}"); }
                    else if (stringList[i].Contains("T")) { MyListCheck.Add($"T:{factors_Name_[19]}"); }
                    else if (stringList[i].Contains("U")) { MyListCheck.Add($"U:{factors_Name_[20]}"); }
                    else if (stringList[i].Contains("V")) { MyListCheck.Add($"V:{factors_Name_[21]}"); }
                    else if (stringList[i].Contains("W")) { MyListCheck.Add($"W:{factors_Name_[22]}"); }
                    else if (stringList[i].Contains("X")) { MyListCheck.Add($"X:{factors_Name_[23]}"); }
                    else if (stringList[i].Contains("Y")) { MyListCheck.Add($"Y:{factors_Name_[24]}"); }
                    else if (stringList[i].Contains("Z")) { MyListCheck.Add($"Z:{factors_Name_[25]}"); }
                    //if (i != 0) { factors_Name_ = ""; }
                }
                                     
                stringList.Add(data3.scenario_message);
                stringList.Add("Mid="+data3.module_id.ToString()+",Tid=" + data3.test_scenario_id.ToString()+","+ data3.scenario_message);
                //stringList.Add("Tid="+data3.test_scenario_id.ToString());
                yield return stringList;
            }
        }       
        //public static List<string> MyListCheck { get; set; }
        public ActionResult GenerateTest(int id,string module_name)
        {
            using (var context = new DBModels())
            {
               

                List<string> A = new List<string>();
                List<string> B = new List<string>();
                List<string> C = new List<string>();
                List<string> D = new List<string>();
                List<string> E = new List<string>();
                List<string> F = new List<string>();
                List<string> G = new List<string>();
                List<string> H = new List<string>();
                List<string> I = new List<string>();
                List<string> J = new List<string>();
                List<string> K = new List<string>();
                List<string> L = new List<string>();
                List<string> M = new List<string>();
                List<string> N = new List<string>();
                List<string> O = new List<string>();
                List<string> P = new List<string>();
                List<string> Q = new List<string>();
                List<string> R = new List<string>();
                List<string> S = new List<string>();
                List<string> T = new List<string>();
                List<string> U = new List<string>();
                List<string> V = new List<string>();
                List<string> W = new List<string>();
                List<string> X = new List<string>();
                List<string> Y = new List<string>();
                List<string> Z = new List<string>();

                /*var listaz = new[]
                 {
                    new { i = 0, n = "A" },new { i = 1, n = "B" },new { i = 2, n = "C" },new { i = 3, n = "D" },new { i = 4, n = "E" },
                    new { i = 5, n = "F" },new { i = 6, n = "G" },new { i = 7, n = "H" },new { i = 8, n = "I" },new { i = 9, n = "J" },
                    new { i = 10, n = "K" },new { i = 11, n = "L" },new { i = 12, n = "M" },new { i = 13, n = "N" },new { i = 14, n = "O" },
                    new { i = 15, n = "P" },new { i = 16, n = "Q" },new { i = 17, n = "R" },new { i = 18, n = "S" },new { i = 19, n = "T" },
                    new { i = 20, n = "U" },new { i = 21, n = "V" },new { i = 22, n = "W" },new { i = 23, n = "X" },new { i = 24, n = "Y" },
                    new { i = 25, n = "Z" }
                 }.ToList();*/

                GenerateTestController.Factors_Description = new List<string>();
                List<string> listEmpty = new List<string>();
                //List<string> factors_description = new List<string>();
                dynamic factors_data = createDataforMapItems(id);
                dynamic mapList = mapItems(0, factors_data, listEmpty);

                List<string> dataFT = new List<string>();

                foreach (var indexx in mapList)
                {
                    if (indexx.Contains("A")) { A.Add(indexx); }
                    else if (indexx.Contains("B")) { B.Add(indexx);  }
                    else if (indexx.Contains("C")) { C.Add(indexx);  }
                    else if (indexx.Contains("D")) { D.Add(indexx);  }
                    else if (indexx.Contains("E")) { E.Add(indexx);  }
                    else if (indexx.Contains("F")) { F.Add(indexx);  }
                    else if (indexx.Contains("G")) { G.Add(indexx);  }
                    else if (indexx.Contains("H")) { H.Add(indexx);  }
                    else if (indexx.Contains("I")) { I.Add(indexx);  }
                    else if (indexx.Contains("J")) { J.Add(indexx);  }
                    else if (indexx.Contains("K")) { K.Add(indexx);  }
                    else if (indexx.Contains("L")) { L.Add(indexx);  }
                    else if (indexx.Contains("M")) { M.Add(indexx);  }
                    else if (indexx.Contains("N")) { N.Add(indexx);  }
                    else if (indexx.Contains("O")) { O.Add(indexx);  }
                    else if (indexx.Contains("P")) { P.Add(indexx);  }
                    else if (indexx.Contains("Q")) { Q.Add(indexx);  }
                    else if (indexx.Contains("R")) { R.Add(indexx);  }
                    else if (indexx.Contains("S")) { S.Add(indexx);  }
                    else if (indexx.Contains("T")) { T.Add(indexx);  }
                    else if (indexx.Contains("U")) { U.Add(indexx);  }
                    else if (indexx.Contains("V")) { V.Add(indexx);  }
                    else if (indexx.Contains("W")) { W.Add(indexx);  }
                    else if (indexx.Contains("X")) { X.Add(indexx);  }
                    else if (indexx.Contains("Y")) { Y.Add(indexx);  }
                    else if (indexx.Contains("Z")) { Z.Add(indexx);  }
                }

                //Get data from another controller.
                 var controller = DependencyResolver.Current.GetService<FactorsController>();
                 controller.ControllerContext = new ControllerContext(this.Request.RequestContext, controller);
                 int total = controller.Calculate_Items(id);

                
                 /*dynamic mapItem(int rowCount, List<string> item, List<string> dataaz)
                    {
                        int j = 0;
                        List<string> test2 = new List<string>();
                        List<string> test3 = new List<string>();
                        GenerateTestController.Factors_Description = new List<string>();
                        bool isEmpty = !dataaz.Any();
                        bool isEmpty2 = !item.Any();
                        if (isEmpty) { } else { test3 = dataaz; }
                        if (isEmpty2) { } else { factors_item2 = item; }
                        foreach (var factors_item3 in factors_item2)
                        {
                            int status = 0;
                            var temp = factors_item3.Split('_');
                            for (int i = j; i <= Int32.Parse(temp[2]); i++)
                            {
                                foreach (var item2 in listaz)
                                {
                                    if (i == Int32.Parse(temp[2]) && i == item2.i)
                                    {
                                        int c = 0;                                       
                                        if (rowCount != 0) {
                                            c = rowCount + 1;                          
                                            test3.Add(item2.n + c);
                                            Factors_Description.Add(item2.n + c + ":"+ temp[1]);
                                        } else {
                                            rowCount = 0;
                                            test3.Add(item2.n + "1");
                                            Factors_Description.Add(item2.n + c + ":" + temp[1]);
                                        }
                                        j = i + 1;
                                        status = 1;
                                    }
                                    if (i != item2.i && status != 1)
                                    {
                                        continue;
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                            }
                            if (status == 0)
                            {
                                test2.Add(temp[0] + "_" + temp[1] + "_" + temp[2]);
                            }
                            int t = factors_item2.IndexOf(factors_item3);
                            if (factors_item2.IndexOf(factors_item3) == factors_item2.Count - 1)
                            {
                                if (test2.Count == 0)
                                {                                                             
                                    return test3;
                                }
                                if (rowCount >= 0) { ++rowCount; }
                                mapItem(rowCount, test2, test3);                                                   
                            }
                            if (status == 1)
                            {
                                continue;
                            }
                        }
                        return test3;
                    }*/
                
                    int indexA = 0,jx = 1;
                    string ex = "", ex2 = "",ex3 = "",ex4 = "",ex5 = "",ex6 = "",ex7 = "",ex8 = "",ex9 = "",ex10 = "",
                     ex11 = "", ex12 = "", ex13 = "", ex14 = "", ex15 = "", ex16 = "", ex17 = "", ex18 = "", ex19 = "", ex20 = "",
                     ex21 = "", ex22 = "", ex23 = "", ex24 = "", ex25 = "", ex26 = "", ex27 = "", ex28 = "", ex29 = "", ex30 = "",
                     ex31 = "", ex32 = "", ex33 = "", ex34 = "", ex35 = "", ex36 = "", ex37 = "", ex38 = "", ex39 = "", ex40 = "",
                     ex41 = "", ex42 = "", ex43 = "", ex44 = "", ex45 = "", ex46 = "", ex47 = "", ex48 = "", ex49 = "", ex50 = "",
                     ex51 = "", ex52 = "";

                    int py = 0;
                    while (py <= total)
                    {
                        int a = 0;
                        while (!A.Any() || A.Any())
                        {
                            try
                            {
                                ex9 = (A.Any() ? A[a] : "");
                                //1 value or empty
                                if (ex9 != "" && ex10 != "")
                                {
                                    if (ex9 == ex10) { break; }
                                }

                                if (indexA == total) { break; }
                                ex10 = (A.Any() ? A[a] : "");

                            }
                            catch (Exception)
                            {
                                break;
                            }
                            int b = 0;
                            while (!B.Any() || B.Any())
                            {
                                try
                                {
                                    ex7 = (A.Any() ? A[a] : "") + (B.Any() ? B[b] : "");
                                    //1 value or empty
                                    if (ex7 != "" && ex8 != "")
                                    {
                                        if (ex7 == ex8) { break; }
                                    }

                                    if (indexA == total) { break; }
                                    ex8 = (A.Any() ? A[a] : "") + (B.Any() ? B[b] : "");

                                }
                                catch (Exception)
                                {
                                    break;
                                }
                                int c = 0;
                                while (!C.Any() || C.Any())
                                {
                                    try
                                    {
                                        ex5 = (A.Any() ? A[a] : "") + (B.Any() ? B[b] : "") + (C.Any() ? C[c] : "");
                                        //1 value or empty
                                        if (ex5 != "" && ex6 != "")
                                        {
                                            if (ex5 == ex6) { break; }
                                        }

                                        if (indexA == total) { break; }
                                        ex6 = (A.Any() ? A[a] : "") + (B.Any() ? B[b] : "") + (C.Any() ? C[c] : "");

                                    }
                                    catch (Exception)
                                    {
                                        break;
                                    }
                                    int d = 0;
                                    while (!D.Any() || D.Any())
                                    {
                                        try
                                        {
                                            ex3 = (A.Any() ? A[a] : "") + (B.Any() ? B[b] : "") + (C.Any() ? C[c] : "") + (D.Any() ? D[d] : "");
                                            //1 value or empty
                                            if (ex3 != "" && ex4 != "")
                                            {
                                                if (ex3 == ex4) { break; }
                                            }

                                            if (indexA == total) { break; }
                                            ex4 = (A.Any() ? A[a] : "") + (B.Any() ? B[b] : "") + (C.Any() ? C[c] : "") + (D.Any() ? D[d] : "");

                                        }
                                        catch (Exception)
                                        {
                                            break;
                                        }
                                        int e = 0;
                                        while (!E.Any() || E.Any())
                                        {
                                            try
                                            {
                                                ex11 = (A.Any() ? A[a] : "") + (B.Any() ? B[b] : "") + (C.Any() ? C[c] : "") + (D.Any() ? D[d] : "") + (E.Any() ? E[e] : "");
                                                //1 value or empty
                                                if (ex11 != "" && ex12 != "")
                                                {
                                                    if (ex11 == ex12) { break; }
                                                }

                                                if (indexA == total) { break; }
                                                ex12 = (A.Any() ? A[a] : "") + (B.Any() ? B[b] : "") + (C.Any() ? C[c] : "") + (D.Any() ? D[d] : "") + (E.Any() ? E[e] : "");

                                            }
                                            catch (Exception)
                                            {
                                                break;
                                            }
                                            int f = 0;
                                            while (!F.Any() || F.Any())
                                            {
                                                try
                                                {
                                                    ex13 = (A.Any() ? A[a] : "") + (B.Any() ? B[b] : "") + 
                                                    (C.Any() ? C[c] : "") + (D.Any() ? D[d] : "") + (E.Any() ? E[e] : "") + (F.Any() ? F[f] : "");
                                                    //1 value or empty
                                                    if (ex13 != "" && ex14 != "")
                                                    {
                                                        if (ex13 == ex14) { break; }
                                                    }

                                                    if (indexA == total) { break; }
                                                    ex14 = (A.Any() ? A[a] : "") + (B.Any() ? B[b] : "") + 
                                                    (C.Any() ? C[c] : "") + (D.Any() ? D[d] : "") + (E.Any() ? E[e] : "") + (F.Any() ? F[f] : "");

                                                }
                                                catch (Exception)
                                                {
                                                    break;
                                                }
                                                int g = 0;
                                                while (!G.Any() || G.Any())
                                                {
                                                    try
                                                    {
                                                        ex15 = (A.Any() ? A[a] : "") + (B.Any() ? B[b] : "") +
                                                        (C.Any() ? C[c] : "") + (D.Any() ? D[d] : "") + (E.Any() ? E[e] : "")
                                                        + (F.Any() ? F[f] : "") + (G.Any() ? G[g] : "");
                                                        //1 value or empty
                                                        if (ex15 != "" && ex16 != "")
                                                        {
                                                            if (ex15 == ex16) { break; }
                                                        }

                                                        if (indexA == total) { break; }
                                                        ex16 = (A.Any() ? A[a] : "") + (B.Any() ? B[b] : "") +
                                                        (C.Any() ? C[c] : "") + (D.Any() ? D[d] : "") + (E.Any() ? E[e] : "")
                                                        + (F.Any() ? F[f] : "") + (G.Any() ? G[g] : "");

                                                    }
                                                    catch (Exception)
                                                    {
                                                        break;
                                                    }
                                                    int h = 0;
                                                    while (!H.Any() || H.Any())
                                                    {
                                                        try
                                                        {
                                                            ex17 = (A.Any() ? A[a] : "") + (B.Any() ? B[b] : "") +
                                                            (C.Any() ? C[c] : "") + (D.Any() ? D[d] : "") + (E.Any() ? E[e] : "")
                                                            + (F.Any() ? F[f] : "") + (G.Any() ? G[g] : "") + (H.Any() ? H[h] : "");
                                                            //1 value or empty
                                                            if (ex17 != "" && ex18 != "")
                                                            {
                                                                if (ex17 == ex18) { break; }
                                                            }

                                                            if (indexA == total) { break; }
                                                            ex18 = (A.Any() ? A[a] : "") + (B.Any() ? B[b] : "") +
                                                            (C.Any() ? C[c] : "") + (D.Any() ? D[d] : "") + (E.Any() ? E[e] : "")
                                                            + (F.Any() ? F[f] : "") + (G.Any() ? G[g] : "") + (H.Any() ? H[h] : "");

                                                        }
                                                        catch (Exception)
                                                        {
                                                            break;
                                                        }
                                                        int ix = 0;
                                                        while (!I.Any() || I.Any())
                                                        {
                                                            try
                                                            {
                                                                ex19 = (A.Any() ? A[a] : "") + (B.Any() ? B[b] : "") +
                                                                (C.Any() ? C[c] : "") + (D.Any() ? D[d] : "") + (E.Any() ? E[e] : "")
                                                                + (F.Any() ? F[f] : "") + (G.Any() ? G[g] : "") + (H.Any() ? H[h] : "")
                                                                + (I.Any() ? I[ix] : "");
                                                                //1 value or empty
                                                                if (ex19 != "" && ex20 != "")
                                                                {
                                                                    if (ex19 == ex20) { break; }
                                                                }

                                                                if (indexA == total) { break; }
                                                                ex20 = (A.Any() ? A[a] : "") + (B.Any() ? B[b] : "") +
                                                                (C.Any() ? C[c] : "") + (D.Any() ? D[d] : "") + (E.Any() ? E[e] : "")
                                                                + (F.Any() ? F[f] : "") + (G.Any() ? G[g] : "") + (H.Any() ? H[h] : "")
                                                                + (I.Any() ? I[ix] : "");

                                                            }
                                                            catch (Exception)
                                                            {
                                                                break;
                                                            }
                                                            int j = 0;
                                                            while (!J.Any() || J.Any())
                                                            {
                                                                try
                                                                {
                                                                    ex21 = (A.Any() ? A[a] : "") + (B.Any() ? B[b] : "") +
                                                                    (C.Any() ? C[c] : "") + (D.Any() ? D[d] : "") + (E.Any() ? E[e] : "")
                                                                    + (F.Any() ? F[f] : "") + (G.Any() ? G[g] : "") + (H.Any() ? H[h] : "")
                                                                    + (I.Any() ? I[ix] : "") + (J.Any() ? J[j] : "");
                                                                    //1 value or empty
                                                                    if (ex21 != "" && ex22 != "")
                                                                    {
                                                                        if (ex21 == ex22) { break; }
                                                                    }

                                                                    if (indexA == total) { break; }
                                                                    ex22 = (A.Any() ? A[a] : "") + (B.Any() ? B[b] : "") +
                                                                    (C.Any() ? C[c] : "") + (D.Any() ? D[d] : "") + (E.Any() ? E[e] : "")
                                                                    + (F.Any() ? F[f] : "") + (G.Any() ? G[g] : "") + (H.Any() ? H[h] : "")
                                                                    + (I.Any() ? I[ix] : "") + (J.Any() ? J[j] : "");

                                                                }
                                                                catch (Exception)
                                                                {
                                                                    break;
                                                                }
                                                                int k = 0;
                                                                while (!K.Any() || K.Any())
                                                                {
                                                                    try
                                                                    {
                                                                        ex23 = (A.Any() ? A[a] : "") + (B.Any() ? B[b] : "") +
                                                                        (C.Any() ? C[c] : "") + (D.Any() ? D[d] : "") + (E.Any() ? E[e] : "")
                                                                        + (F.Any() ? F[f] : "") + (G.Any() ? G[g] : "") + (H.Any() ? H[h] : "")
                                                                        + (I.Any() ? I[ix] : "") + (J.Any() ? J[j] : "") + (K.Any() ? K[k] : "");
                                                                        //1 value or empty
                                                                        if (ex23 != "" && ex24 != "")
                                                                        {
                                                                            if (ex23 == ex24) { break; }
                                                                        }

                                                                        if (indexA == total) { break; }
                                                                        ex24 = (A.Any() ? A[a] : "") + (B.Any() ? B[b] : "") +
                                                                        (C.Any() ? C[c] : "") + (D.Any() ? D[d] : "") + (E.Any() ? E[e] : "")
                                                                        + (F.Any() ? F[f] : "") + (G.Any() ? G[g] : "") + (H.Any() ? H[h] : "")
                                                                        + (I.Any() ? I[ix] : "") + (J.Any() ? J[j] : "") + (K.Any() ? K[k] : "");

                                                                    }
                                                                    catch (Exception)
                                                                    {
                                                                        break;
                                                                    }
                                                                    int l = 0;
                                                                    while (!L.Any() || L.Any())
                                                                    {
                                                                        try
                                                                        {
                                                                            ex25 = (A.Any() ? A[a] : "") + (B.Any() ? B[b] : "") +
                                                                            (C.Any() ? C[c] : "") + (D.Any() ? D[d] : "") + (E.Any() ? E[e] : "")
                                                                            + (F.Any() ? F[f] : "") + (G.Any() ? G[g] : "") + (H.Any() ? H[h] : "")
                                                                            + (I.Any() ? I[ix] : "") + (J.Any() ? J[j] : "") + (K.Any() ? K[k] : "")
                                                                            + (L.Any() ? L[l] : "");
                                                                            //1 value or empty
                                                                            if (ex25 != "" && ex26 != "")
                                                                            {
                                                                                if (ex25 == ex26) { break; }
                                                                            }

                                                                            if (indexA == total) { break; }
                                                                            ex26 = (A.Any() ? A[a] : "") + (B.Any() ? B[b] : "") +
                                                                            (C.Any() ? C[c] : "") + (D.Any() ? D[d] : "") + (E.Any() ? E[e] : "")
                                                                            + (F.Any() ? F[f] : "") + (G.Any() ? G[g] : "") + (H.Any() ? H[h] : "")
                                                                            + (I.Any() ? I[ix] : "") + (J.Any() ? J[j] : "") + (K.Any() ? K[k] : "")
                                                                            + (L.Any() ? L[l] : "");

                                                                        }
                                                                        catch (Exception)
                                                                        {
                                                                            break;
                                                                        }
                                                                        int m = 0;
                                                                        while (!M.Any() || M.Any())
                                                                        {
                                                                            try
                                                                            {
                                                                                ex27 = (A.Any() ? A[a] : "") + (B.Any() ? B[b] : "") +
                                                                                (C.Any() ? C[c] : "") + (D.Any() ? D[d] : "") + (E.Any() ? E[e] : "")
                                                                                + (F.Any() ? F[f] : "") + (G.Any() ? G[g] : "") + (H.Any() ? H[h] : "")
                                                                                + (I.Any() ? I[ix] : "") + (J.Any() ? J[j] : "") + (K.Any() ? K[k] : "")
                                                                                + (L.Any() ? L[l] : "") + (M.Any() ? M[m] : "");
                                                                                //1 value or empty
                                                                                if (ex27 != "" && ex28 != "")
                                                                                {
                                                                                    if (ex27 == ex28) { break; }
                                                                                }

                                                                                if (indexA == total) { break; }
                                                                                ex28 = (A.Any() ? A[a] : "") + (B.Any() ? B[b] : "") +
                                                                                (C.Any() ? C[c] : "") + (D.Any() ? D[d] : "") + (E.Any() ? E[e] : "")
                                                                                + (F.Any() ? F[f] : "") + (G.Any() ? G[g] : "") + (H.Any() ? H[h] : "")
                                                                                + (I.Any() ? I[ix] : "") + (J.Any() ? J[j] : "") + (K.Any() ? K[k] : "")
                                                                                + (L.Any() ? L[l] : "") + (M.Any() ? M[m] : "");

                                                                            }
                                                                            catch (Exception)
                                                                            {
                                                                                break;
                                                                            }
                                                                            int n = 0;
                                                                            while (!N.Any() || N.Any())
                                                                            {
                                                                                try
                                                                                {
                                                                                    ex29 = (A.Any() ? A[a] : "") + (B.Any() ? B[b] : "") +
                                                                                    (C.Any() ? C[c] : "") + (D.Any() ? D[d] : "") + (E.Any() ? E[e] : "")
                                                                                    + (F.Any() ? F[f] : "") + (G.Any() ? G[g] : "") + (H.Any() ? H[h] : "")
                                                                                    + (I.Any() ? I[ix] : "") + (J.Any() ? J[j] : "") + (K.Any() ? K[k] : "")
                                                                                    + (L.Any() ? L[l] : "") + (M.Any() ? M[m] : "") + (N.Any() ? N[n] : "");
                                                                                    //1 value or empty
                                                                                    if (ex29 != "" && ex30 != "")
                                                                                    {
                                                                                        if (ex29 == ex30) { break; }
                                                                                    }

                                                                                    if (indexA == total) { break; }
                                                                                    ex30 = (A.Any() ? A[a] : "") + (B.Any() ? B[b] : "") +
                                                                                    (C.Any() ? C[c] : "") + (D.Any() ? D[d] : "") + (E.Any() ? E[e] : "")
                                                                                    + (F.Any() ? F[f] : "") + (G.Any() ? G[g] : "") + (H.Any() ? H[h] : "")
                                                                                    + (I.Any() ? I[ix] : "") + (J.Any() ? J[j] : "") + (K.Any() ? K[k] : "")
                                                                                    + (L.Any() ? L[l] : "") + (M.Any() ? M[m] : "") + (N.Any() ? N[n] : "");

                                                                                }
                                                                                catch (Exception)
                                                                                {
                                                                                    break;
                                                                                }
                                                                                int o = 0;
                                                                                while (!O.Any() || O.Any())
                                                                                {
                                                                                    try
                                                                                    {
                                                                                        ex31 = (A.Any() ? A[a] : "") + (B.Any() ? B[b] : "") +
                                                                                        (C.Any() ? C[c] : "") + (D.Any() ? D[d] : "") + (E.Any() ? E[e] : "")
                                                                                        + (F.Any() ? F[f] : "") + (G.Any() ? G[g] : "") + (H.Any() ? H[h] : "")
                                                                                        + (I.Any() ? I[ix] : "") + (J.Any() ? J[j] : "") + (K.Any() ? K[k] : "")
                                                                                        + (L.Any() ? L[l] : "") + (M.Any() ? M[m] : "") + (N.Any() ? N[n] : "")
                                                                                        + (O.Any() ? O[o] : "");
                                                                                        //1 value or empty
                                                                                        if (ex31 != "" && ex32 != "")
                                                                                        {
                                                                                            if (ex31 == ex32) { break; }
                                                                                        }

                                                                                        if (indexA == total) { break; }
                                                                                        ex32 = (A.Any() ? A[a] : "") + (B.Any() ? B[b] : "") +
                                                                                        (C.Any() ? C[c] : "") + (D.Any() ? D[d] : "") + (E.Any() ? E[e] : "")
                                                                                        + (F.Any() ? F[f] : "") + (G.Any() ? G[g] : "") + (H.Any() ? H[h] : "")
                                                                                        + (I.Any() ? I[ix] : "") + (J.Any() ? J[j] : "") + (K.Any() ? K[k] : "")
                                                                                        + (L.Any() ? L[l] : "") + (M.Any() ? M[m] : "") + (N.Any() ? N[n] : "")
                                                                                        + (O.Any() ? O[o] : "");

                                                                                    }
                                                                                    catch (Exception)
                                                                                    {
                                                                                        break;
                                                                                    }
                                                                                    int p = 0;
                                                                                    while (!P.Any() || P.Any())
                                                                                    {
                                                                                        try
                                                                                        {
                                                                                            ex33 = (A.Any() ? A[a] : "") + (B.Any() ? B[b] : "") +
                                                                                            (C.Any() ? C[c] : "") + (D.Any() ? D[d] : "") + (E.Any() ? E[e] : "")
                                                                                            + (F.Any() ? F[f] : "") + (G.Any() ? G[g] : "") + (H.Any() ? H[h] : "")
                                                                                            + (I.Any() ? I[ix] : "") + (J.Any() ? J[j] : "") + (K.Any() ? K[k] : "")
                                                                                            + (L.Any() ? L[l] : "") + (M.Any() ? M[m] : "") + (N.Any() ? N[n] : "")
                                                                                            + (O.Any() ? O[o] : "") + (P.Any() ? P[p] : "");
                                                                                            //1 value or empty
                                                                                            if (ex33 != "" && ex34 != "")
                                                                                            {
                                                                                                if (ex33 == ex34) { break; }
                                                                                            }

                                                                                            if (indexA == total) { break; }
                                                                                            ex34 = (A.Any() ? A[a] : "") + (B.Any() ? B[b] : "") +
                                                                                            (C.Any() ? C[c] : "") + (D.Any() ? D[d] : "") + (E.Any() ? E[e] : "")
                                                                                            + (F.Any() ? F[f] : "") + (G.Any() ? G[g] : "") + (H.Any() ? H[h] : "")
                                                                                            + (I.Any() ? I[ix] : "") + (J.Any() ? J[j] : "") + (K.Any() ? K[k] : "")
                                                                                            + (L.Any() ? L[l] : "") + (M.Any() ? M[m] : "") + (N.Any() ? N[n] : "")
                                                                                            + (O.Any() ? O[o] : "") + (P.Any() ? P[p] : "");

                                                                                        }
                                                                                        catch (Exception)
                                                                                        {
                                                                                            break;
                                                                                        }
                                                                                        int q = 0;
                                                                                        while (!Q.Any() || Q.Any())
                                                                                        {
                                                                                            try
                                                                                            {
                                                                                                ex35 = (A.Any() ? A[a] : "") + (B.Any() ? B[b] : "") +
                                                                                                (C.Any() ? C[c] : "") + (D.Any() ? D[d] : "") + (E.Any() ? E[e] : "")
                                                                                                + (F.Any() ? F[f] : "") + (G.Any() ? G[g] : "") + (H.Any() ? H[h] : "")
                                                                                                + (I.Any() ? I[ix] : "") + (J.Any() ? J[j] : "") + (K.Any() ? K[k] : "")
                                                                                                + (L.Any() ? L[l] : "") + (M.Any() ? M[m] : "") + (N.Any() ? N[n] : "")
                                                                                                + (O.Any() ? O[o] : "") + (P.Any() ? P[p] : "") + (Q.Any() ? Q[q] : "");
                                                                                                //1 value or empty
                                                                                                if (ex35 != "" && ex36 != "")
                                                                                                {
                                                                                                    if (ex35 == ex36) { break; }
                                                                                                }

                                                                                                if (indexA == total) { break; }
                                                                                                ex36 = (A.Any() ? A[a] : "") + (B.Any() ? B[b] : "") +
                                                                                                (C.Any() ? C[c] : "") + (D.Any() ? D[d] : "") + (E.Any() ? E[e] : "")
                                                                                                + (F.Any() ? F[f] : "") + (G.Any() ? G[g] : "") + (H.Any() ? H[h] : "")
                                                                                                + (I.Any() ? I[ix] : "") + (J.Any() ? J[j] : "") + (K.Any() ? K[k] : "")
                                                                                                + (L.Any() ? L[l] : "") + (M.Any() ? M[m] : "") + (N.Any() ? N[n] : "")
                                                                                                + (O.Any() ? O[o] : "") + (P.Any() ? P[p] : "") + (Q.Any() ? Q[q] : "");

                                                                                            }
                                                                                            catch (Exception)
                                                                                            {
                                                                                                break;
                                                                                            }
                                                                                            int r = 0;
                                                                                            while (!R.Any() || R.Any())
                                                                                            {
                                                                                                try
                                                                                                {
                                                                                                    ex37 = (A.Any() ? A[a] : "") + (B.Any() ? B[b] : "") +
                                                                                                    (C.Any() ? C[c] : "") + (D.Any() ? D[d] : "") + (E.Any() ? E[e] : "")
                                                                                                    + (F.Any() ? F[f] : "") + (G.Any() ? G[g] : "") + (H.Any() ? H[h] : "")
                                                                                                    + (I.Any() ? I[ix] : "") + (J.Any() ? J[j] : "") + (K.Any() ? K[k] : "")
                                                                                                    + (L.Any() ? L[l] : "") + (M.Any() ? M[m] : "") + (N.Any() ? N[n] : "")
                                                                                                    + (O.Any() ? O[o] : "") + (P.Any() ? P[p] : "") + (Q.Any() ? Q[q] : "")
                                                                                                    + (R.Any() ? R[r] : "");
                                                                                                    //1 value or empty
                                                                                                    if (ex37 != "" && ex38 != "")
                                                                                                    {
                                                                                                        if (ex37 == ex38) { break; }
                                                                                                    }

                                                                                                    if (indexA == total) { break; }
                                                                                                    ex38 = (A.Any() ? A[a] : "") + (B.Any() ? B[b] : "") +
                                                                                                    (C.Any() ? C[c] : "") + (D.Any() ? D[d] : "") + (E.Any() ? E[e] : "")
                                                                                                    + (F.Any() ? F[f] : "") + (G.Any() ? G[g] : "") + (H.Any() ? H[h] : "")
                                                                                                    + (I.Any() ? I[ix] : "") + (J.Any() ? J[j] : "") + (K.Any() ? K[k] : "")
                                                                                                    + (L.Any() ? L[l] : "") + (M.Any() ? M[m] : "") + (N.Any() ? N[n] : "")
                                                                                                    + (O.Any() ? O[o] : "") + (P.Any() ? P[p] : "") + (Q.Any() ? Q[q] : "")
                                                                                                    + (R.Any() ? R[r] : "");

                                                                                                }
                                                                                                catch (Exception)
                                                                                                {
                                                                                                    break;
                                                                                                }
                                                                                                int s = 0;
                                                                                                while (!S.Any() || S.Any())
                                                                                                {
                                                                                                    try
                                                                                                    {
                                                                                                        ex39 = (A.Any() ? A[a] : "") + (B.Any() ? B[b] : "") +
                                                                                                        (C.Any() ? C[c] : "") + (D.Any() ? D[d] : "") + (E.Any() ? E[e] : "")
                                                                                                        + (F.Any() ? F[f] : "") + (G.Any() ? G[g] : "") + (H.Any() ? H[h] : "")
                                                                                                        + (I.Any() ? I[ix] : "") + (J.Any() ? J[j] : "") + (K.Any() ? K[k] : "")
                                                                                                        + (L.Any() ? L[l] : "") + (M.Any() ? M[m] : "") + (N.Any() ? N[n] : "")
                                                                                                        + (O.Any() ? O[o] : "") + (P.Any() ? P[p] : "") + (Q.Any() ? Q[q] : "")
                                                                                                        + (R.Any() ? R[r] : "") + (S.Any() ? S[s] : "");
                                                                                                        //1 value or empty
                                                                                                        if (ex39 != "" && ex40 != "")
                                                                                                        {
                                                                                                            if (ex39 == ex40) { break; }
                                                                                                        }

                                                                                                        if (indexA == total) { break; }
                                                                                                        ex40 = (A.Any() ? A[a] : "") + (B.Any() ? B[b] : "") +
                                                                                                        (C.Any() ? C[c] : "") + (D.Any() ? D[d] : "") + (E.Any() ? E[e] : "")
                                                                                                        + (F.Any() ? F[f] : "") + (G.Any() ? G[g] : "") + (H.Any() ? H[h] : "")
                                                                                                        + (I.Any() ? I[ix] : "") + (J.Any() ? J[j] : "") + (K.Any() ? K[k] : "")
                                                                                                        + (L.Any() ? L[l] : "") + (M.Any() ? M[m] : "") + (N.Any() ? N[n] : "")
                                                                                                        + (O.Any() ? O[o] : "") + (P.Any() ? P[p] : "") + (Q.Any() ? Q[q] : "")
                                                                                                        + (R.Any() ? R[r] : "") + (S.Any() ? S[s] : "");

                                                                                                    }
                                                                                                    catch (Exception)
                                                                                                    {
                                                                                                        break;
                                                                                                    }
                                                                                                    int t = 0;
                                                                                                    while (!T.Any() || T.Any())
                                                                                                    {
                                                                                                        try
                                                                                                        {
                                                                                                            ex41 = (A.Any() ? A[a] : "") + (B.Any() ? B[b] : "") +
                                                                                                            (C.Any() ? C[c] : "") + (D.Any() ? D[d] : "") + (E.Any() ? E[e] : "")
                                                                                                            + (F.Any() ? F[f] : "") + (G.Any() ? G[g] : "") + (H.Any() ? H[h] : "")
                                                                                                            + (I.Any() ? I[ix] : "") + (J.Any() ? J[j] : "") + (K.Any() ? K[k] : "")
                                                                                                            + (L.Any() ? L[l] : "") + (M.Any() ? M[m] : "") + (N.Any() ? N[n] : "")
                                                                                                            + (O.Any() ? O[o] : "") + (P.Any() ? P[p] : "") + (Q.Any() ? Q[q] : "")
                                                                                                            + (R.Any() ? R[r] : "") + (S.Any() ? S[s] : "") + (T.Any() ? T[t] : "");
                                                                                                            //1 value or empty
                                                                                                            if (ex41 != "" && ex42 != "")
                                                                                                            {
                                                                                                                if (ex41 == ex42) { break; }
                                                                                                            }

                                                                                                            if (indexA == total) { break; }
                                                                                                            ex42 = (A.Any() ? A[a] : "") + (B.Any() ? B[b] : "") +
                                                                                                            (C.Any() ? C[c] : "") + (D.Any() ? D[d] : "") + (E.Any() ? E[e] : "")
                                                                                                            + (F.Any() ? F[f] : "") + (G.Any() ? G[g] : "") + (H.Any() ? H[h] : "")
                                                                                                            + (I.Any() ? I[ix] : "") + (J.Any() ? J[j] : "") + (K.Any() ? K[k] : "")
                                                                                                            + (L.Any() ? L[l] : "") + (M.Any() ? M[m] : "") + (N.Any() ? N[n] : "")
                                                                                                            + (O.Any() ? O[o] : "") + (P.Any() ? P[p] : "") + (Q.Any() ? Q[q] : "")
                                                                                                            + (R.Any() ? R[r] : "") + (S.Any() ? S[s] : "") + (T.Any() ? T[t] : "");

                                                                                                        }
                                                                                                        catch (Exception)
                                                                                                        {
                                                                                                            break;
                                                                                                        }
                                                                                                        int u = 0;
                                                                                                        while (!U.Any() || U.Any())
                                                                                                        {
                                                                                                            try
                                                                                                            {
                                                                                                                ex43 = (A.Any() ? A[a] : "") + (B.Any() ? B[b] : "") +
                                                                                                                (C.Any() ? C[c] : "") + (D.Any() ? D[d] : "") + (E.Any() ? E[e] : "")
                                                                                                                + (F.Any() ? F[f] : "") + (G.Any() ? G[g] : "") + (H.Any() ? H[h] : "")
                                                                                                                + (I.Any() ? I[ix] : "") + (J.Any() ? J[j] : "") + (K.Any() ? K[k] : "")
                                                                                                                + (L.Any() ? L[l] : "") + (M.Any() ? M[m] : "") + (N.Any() ? N[n] : "")
                                                                                                                + (O.Any() ? O[o] : "") + (P.Any() ? P[p] : "") + (Q.Any() ? Q[q] : "")
                                                                                                                + (R.Any() ? R[r] : "") + (S.Any() ? S[s] : "") + (T.Any() ? T[t] : "")
                                                                                                                + (U.Any() ? U[u] : "");
                                                                                                                //1 value or empty
                                                                                                                if (ex43 != "" && ex44 != "")
                                                                                                                {
                                                                                                                    if (ex43 == ex44) { break; }
                                                                                                                }

                                                                                                                if (indexA == total) { break; }
                                                                                                                ex44 = (A.Any() ? A[a] : "") + (B.Any() ? B[b] : "") +
                                                                                                                (C.Any() ? C[c] : "") + (D.Any() ? D[d] : "") + (E.Any() ? E[e] : "")
                                                                                                                + (F.Any() ? F[f] : "") + (G.Any() ? G[g] : "") + (H.Any() ? H[h] : "")
                                                                                                                + (I.Any() ? I[ix] : "") + (J.Any() ? J[j] : "") + (K.Any() ? K[k] : "")
                                                                                                                + (L.Any() ? L[l] : "") + (M.Any() ? M[m] : "") + (N.Any() ? N[n] : "")
                                                                                                                + (O.Any() ? O[o] : "") + (P.Any() ? P[p] : "") + (Q.Any() ? Q[q] : "")
                                                                                                                + (R.Any() ? R[r] : "") + (S.Any() ? S[s] : "") + (T.Any() ? T[t] : "")
                                                                                                                + (U.Any() ? U[u] : "");

                                                                                                            }
                                                                                                            catch (Exception)
                                                                                                            {
                                                                                                                break;
                                                                                                            }
                                                                                                            int v = 0;
                                                                                                            while (!V.Any() || V.Any())
                                                                                                            {
                                                                                                                try
                                                                                                                {
                                                                                                                    ex45 = (A.Any() ? A[a] : "") + (B.Any() ? B[b] : "") +
                                                                                                                    (C.Any() ? C[c] : "") + (D.Any() ? D[d] : "") + (E.Any() ? E[e] : "")
                                                                                                                    + (F.Any() ? F[f] : "") + (G.Any() ? G[g] : "") + (H.Any() ? H[h] : "")
                                                                                                                    + (I.Any() ? I[ix] : "") + (J.Any() ? J[j] : "") + (K.Any() ? K[k] : "")
                                                                                                                    + (L.Any() ? L[l] : "") + (M.Any() ? M[m] : "") + (N.Any() ? N[n] : "")
                                                                                                                    + (O.Any() ? O[o] : "") + (P.Any() ? P[p] : "") + (Q.Any() ? Q[q] : "")
                                                                                                                    + (R.Any() ? R[r] : "") + (S.Any() ? S[s] : "") + (T.Any() ? T[t] : "")
                                                                                                                    + (U.Any() ? U[u] : "") + (V.Any() ? V[v] : "");
                                                                                                                    //1 value or empty
                                                                                                                    if (ex45 != "" && ex46 != "")
                                                                                                                    {
                                                                                                                        if (ex45 == ex46) { break; }
                                                                                                                    }

                                                                                                                    if (indexA == total) { break; }
                                                                                                                    ex46 = (A.Any() ? A[a] : "") + (B.Any() ? B[b] : "") +
                                                                                                                    (C.Any() ? C[c] : "") + (D.Any() ? D[d] : "") + (E.Any() ? E[e] : "")
                                                                                                                    + (F.Any() ? F[f] : "") + (G.Any() ? G[g] : "") + (H.Any() ? H[h] : "")
                                                                                                                    + (I.Any() ? I[ix] : "") + (J.Any() ? J[j] : "") + (K.Any() ? K[k] : "")
                                                                                                                    + (L.Any() ? L[l] : "") + (M.Any() ? M[m] : "") + (N.Any() ? N[n] : "")
                                                                                                                    + (O.Any() ? O[o] : "") + (P.Any() ? P[p] : "") + (Q.Any() ? Q[q] : "")
                                                                                                                    + (R.Any() ? R[r] : "") + (S.Any() ? S[s] : "") + (T.Any() ? T[t] : "")
                                                                                                                    + (U.Any() ? U[u] : "") + (V.Any() ? V[v] : "");

                                                                                                                }
                                                                                                                catch (Exception)
                                                                                                                {
                                                                                                                    break;
                                                                                                                }
                                                                                                                int w = 0;
                                                                                                                while (!W.Any() || W.Any())
                                                                                                                {
                                                                                                                    try
                                                                                                                    {
                                                                                                                        ex47 = (A.Any() ? A[a] : "") + (B.Any() ? B[b] : "") +
                                                                                                                        (C.Any() ? C[c] : "") + (D.Any() ? D[d] : "") + (E.Any() ? E[e] : "")
                                                                                                                        + (F.Any() ? F[f] : "") + (G.Any() ? G[g] : "") + (H.Any() ? H[h] : "")
                                                                                                                        + (I.Any() ? I[ix] : "") + (J.Any() ? J[j] : "") + (K.Any() ? K[k] : "")
                                                                                                                        + (L.Any() ? L[l] : "") + (M.Any() ? M[m] : "") + (N.Any() ? N[n] : "")
                                                                                                                        + (O.Any() ? O[o] : "") + (P.Any() ? P[p] : "") + (Q.Any() ? Q[q] : "")
                                                                                                                        + (R.Any() ? R[r] : "") + (S.Any() ? S[s] : "") + (T.Any() ? T[t] : "")
                                                                                                                        + (U.Any() ? U[u] : "") + (V.Any() ? V[v] : "") + (W.Any() ? W[w] : "");
                                                                                                                        //1 value or empty
                                                                                                                        if (ex47 != "" && ex48 != "")
                                                                                                                        {
                                                                                                                            if (ex47 == ex48) { break; }
                                                                                                                        }

                                                                                                                        if (indexA == total) { break; }
                                                                                                                        ex48 = (A.Any() ? A[a] : "") + (B.Any() ? B[b] : "") +
                                                                                                                        (C.Any() ? C[c] : "") + (D.Any() ? D[d] : "") + (E.Any() ? E[e] : "")
                                                                                                                        + (F.Any() ? F[f] : "") + (G.Any() ? G[g] : "") + (H.Any() ? H[h] : "")
                                                                                                                        + (I.Any() ? I[ix] : "") + (J.Any() ? J[j] : "") + (K.Any() ? K[k] : "")
                                                                                                                        + (L.Any() ? L[l] : "") + (M.Any() ? M[m] : "") + (N.Any() ? N[n] : "")
                                                                                                                        + (O.Any() ? O[o] : "") + (P.Any() ? P[p] : "") + (Q.Any() ? Q[q] : "")
                                                                                                                        + (R.Any() ? R[r] : "") + (S.Any() ? S[s] : "") + (T.Any() ? T[t] : "")
                                                                                                                        + (U.Any() ? U[u] : "") + (V.Any() ? V[v] : "") + (W.Any() ? W[w] : "");

                                                                                                                    }
                                                                                                                    catch (Exception)
                                                                                                                    {
                                                                                                                        break;
                                                                                                                    }
                                                                                                                    int x = 0;
                                                                                                                    while (!X.Any() || X.Any())
                                                                                                                    {
                                                                                                                        try
                                                                                                                        {
                                                                                                                            ex49 = (A.Any() ? A[a] : "") + (B.Any() ? B[b] : "") +
                                                                                                                            (C.Any() ? C[c] : "") + (D.Any() ? D[d] : "") + (E.Any() ? E[e] : "")
                                                                                                                            + (F.Any() ? F[f] : "") + (G.Any() ? G[g] : "") + (H.Any() ? H[h] : "")
                                                                                                                            + (I.Any() ? I[ix] : "") + (J.Any() ? J[j] : "") + (K.Any() ? K[k] : "")
                                                                                                                            + (L.Any() ? L[l] : "") + (M.Any() ? M[m] : "") + (N.Any() ? N[n] : "")
                                                                                                                            + (O.Any() ? O[o] : "") + (P.Any() ? P[p] : "") + (Q.Any() ? Q[q] : "")
                                                                                                                            + (R.Any() ? R[r] : "") + (S.Any() ? S[s] : "") + (T.Any() ? T[t] : "")
                                                                                                                            + (U.Any() ? U[u] : "") + (V.Any() ? V[v] : "") + (W.Any() ? W[w] : "")
                                                                                                                            + (X.Any() ? X[x] : "");
                                                                                                                            //1 value or empty
                                                                                                                            if (ex49 != "" && ex50 != "")
                                                                                                                            {
                                                                                                                                if (ex49 == ex50) { break; }
                                                                                                                            }

                                                                                                                            if (indexA == total) { break; }
                                                                                                                            ex50 = (A.Any() ? A[a] : "") + (B.Any() ? B[b] : "") +
                                                                                                                            (C.Any() ? C[c] : "") + (D.Any() ? D[d] : "") + (E.Any() ? E[e] : "")
                                                                                                                            + (F.Any() ? F[f] : "") + (G.Any() ? G[g] : "") + (H.Any() ? H[h] : "")
                                                                                                                            + (I.Any() ? I[ix] : "") + (J.Any() ? J[j] : "") + (K.Any() ? K[k] : "")
                                                                                                                            + (L.Any() ? L[l] : "") + (M.Any() ? M[m] : "") + (N.Any() ? N[n] : "")
                                                                                                                            + (O.Any() ? O[o] : "") + (P.Any() ? P[p] : "") + (Q.Any() ? Q[q] : "")
                                                                                                                            + (R.Any() ? R[r] : "") + (S.Any() ? S[s] : "") + (T.Any() ? T[t] : "")
                                                                                                                            + (U.Any() ? U[u] : "") + (V.Any() ? V[v] : "") + (W.Any() ? W[w] : "")
                                                                                                                            + (X.Any() ? X[x] : "");

                                                                                                                        }
                                                                                                                        catch (Exception)
                                                                                                                        {
                                                                                                                            break;
                                                                                                                        }
                                                                                                                        int y = 0;
                                                                                                                        while (!Y.Any() || Y.Any())
                                                                                                                        {
                                                                                                                            try
                                                                                                                            {
                                                                                                                                ex51 = (A.Any() ? A[a] : "") + (B.Any() ? B[b] : "") +
                                                                                                                                (C.Any() ? C[c] : "") + (D.Any() ? D[d] : "") + (E.Any() ? E[e] : "")
                                                                                                                                + (F.Any() ? F[f] : "") + (G.Any() ? G[g] : "") + (H.Any() ? H[h] : "")
                                                                                                                                + (I.Any() ? I[ix] : "") + (J.Any() ? J[j] : "") + (K.Any() ? K[k] : "")
                                                                                                                                + (L.Any() ? L[l] : "") + (M.Any() ? M[m] : "") + (N.Any() ? N[n] : "")
                                                                                                                                + (O.Any() ? O[o] : "") + (P.Any() ? P[p] : "") + (Q.Any() ? Q[q] : "")
                                                                                                                                + (R.Any() ? R[r] : "") + (S.Any() ? S[s] : "") + (T.Any() ? T[t] : "")
                                                                                                                                + (U.Any() ? U[u] : "") + (V.Any() ? V[v] : "") + (W.Any() ? W[w] : "")
                                                                                                                                + (X.Any() ? X[x] : "") + (Y.Any() ? Y[y] : "");
                                                                                                                                //1 value or empty
                                                                                                                                if (ex51 != "" && ex52 != "")
                                                                                                                                {
                                                                                                                                    if (ex51 == ex52) { break; }
                                                                                                                                }

                                                                                                                                if (indexA == total) { break; }
                                                                                                                                ex52 = (A.Any() ? A[a] : "") + (B.Any() ? B[b] : "") +
                                                                                                                                (C.Any() ? C[c] : "") + (D.Any() ? D[d] : "") + (E.Any() ? E[e] : "")
                                                                                                                                + (F.Any() ? F[f] : "") + (G.Any() ? G[g] : "") + (H.Any() ? H[h] : "")
                                                                                                                                + (I.Any() ? I[ix] : "") + (J.Any() ? J[j] : "") + (K.Any() ? K[k] : "")
                                                                                                                                + (L.Any() ? L[l] : "") + (M.Any() ? M[m] : "") + (N.Any() ? N[n] : "")
                                                                                                                                + (O.Any() ? O[o] : "") + (P.Any() ? P[p] : "") + (Q.Any() ? Q[q] : "")
                                                                                                                                + (R.Any() ? R[r] : "") + (S.Any() ? S[s] : "") + (T.Any() ? T[t] : "")
                                                                                                                                + (U.Any() ? U[u] : "") + (V.Any() ? V[v] : "") + (W.Any() ? W[w] : "")
                                                                                                                                + (X.Any() ? X[x] : "") + (Y.Any() ? Y[y] : "");

                                                                                                                            }
                                                                                                                            catch (Exception)
                                                                                                                            {
                                                                                                                                break;
                                                                                                                            }
                                                                                                                            int z = 0;
                                                                                                                            while (!Z.Any() || Z.Any())
                                                                                                                            {
                                                                                                                                try
                                                                                                                                {
                                                                                                                                    ex2 = (A.Any() ? A[a] : "") + (B.Any() ? B[b] : "") +
                                                                                                                                    (C.Any() ? C[c] : "") + (D.Any() ? D[d] : "") + (E.Any() ? E[e] : "")
                                                                                                                                    + (F.Any() ? F[f] : "") + (G.Any() ? G[g] : "") + (H.Any() ? H[h] : "")
                                                                                                                                    + (I.Any() ? I[ix] : "") + (J.Any() ? J[j] : "") + (K.Any() ? K[k] : "")
                                                                                                                                    + (L.Any() ? L[l] : "") + (M.Any() ? M[m] : "") + (N.Any() ? N[n] : "")
                                                                                                                                    + (O.Any() ? O[o] : "") + (P.Any() ? P[p] : "") + (Q.Any() ? Q[q] : "")
                                                                                                                                    + (R.Any() ? R[r] : "") + (S.Any() ? S[s] : "") + (T.Any() ? T[t] : "")
                                                                                                                                    + (U.Any() ? U[u] : "") + (V.Any() ? V[v] : "") + (W.Any() ? W[w] : "")
                                                                                                                                    + (X.Any() ? X[x] : "") + (Y.Any() ? Y[y] : "") + (Z.Any() ? Z[z] : "");
                                                                                                                                    //1 value or empty
                                                                                                                                    if (ex2 != "" && ex != "")
                                                                                                                                    {
                                                                                                                                        if (ex2 == ex) { break; }
                                                                                                                                    }
                                                                                                                                    //Console.WriteLine($"{jx + ":" + (A.Any() ? A[a] : "") + (B.Any() ? B[b] : "") + (C.Any() ? C[c] : "") + (D.Any() ? D[d] : "") + (E.Any() ? E[e] : "")}");
                                                                                                                                    dataFT.Add(jx + ":," + (A.Any() ? A[a] + "," : "") + (B.Any() ? B[b] + "," : "") + 
                                                                                                                                    (C.Any() ? C[c] + "," : "") + (D.Any() ? D[d] + "," : "") + (E.Any() ? E[e] + "," : "") 
                                                                                                                                    + (F.Any() ? F[f] + "," : "") + (G.Any() ? G[g] + "," : "") + (H.Any() ? H[h] + "," : "") 
                                                                                                                                    + (I.Any() ? I[ix] + "," : "") + (J.Any() ? J[j] + "," : "") + (K.Any() ? K[k] + "," : "") 
                                                                                                                                    + (L.Any() ? L[l] + "," : "") + (M.Any() ? M[m] + "," : "") + (N.Any() ? N[n] + "," : "") 
                                                                                                                                    + (O.Any() ? O[o] + "," : "") + (P.Any() ? P[p] + "," : "") + (Q.Any() ? Q[q] + "," : "") 
                                                                                                                                    + (R.Any() ? R[r] + "," : "") + (S.Any() ? S[s] + "," : "") + (T.Any() ? T[t] + "," : "") 
                                                                                                                                    + (U.Any() ? U[u] + "," : "") + (V.Any() ? V[v] + "," : "") + (W.Any() ? W[w] + "," : "") 
                                                                                                                                    + (X.Any() ? X[x] + "," : "") + (Y.Any() ? Y[y] + "," : "") + (Z.Any() ? Z[z] : ""));
                                                                                                                                    if (indexA == total) { break; }
                                                                                                                                    ex = (A.Any() ? A[a] : "") + (B.Any() ? B[b] : "") +
                                                                                                                                    (C.Any() ? C[c] : "") + (D.Any() ? D[d] : "") + (E.Any() ? E[e] : "")
                                                                                                                                    + (F.Any() ? F[f] : "") + (G.Any() ? G[g] : "") + (H.Any() ? H[h] : "")
                                                                                                                                    + (I.Any() ? I[ix] : "") + (J.Any() ? J[j] : "") + (K.Any() ? K[k] : "")
                                                                                                                                    + (L.Any() ? L[l] : "") + (M.Any() ? M[m] : "") + (N.Any() ? N[n] : "")
                                                                                                                                    + (O.Any() ? O[o] : "") + (P.Any() ? P[p] : "") + (Q.Any() ? Q[q] : "")
                                                                                                                                    + (R.Any() ? R[r] : "") + (S.Any() ? S[s] : "") + (T.Any() ? T[t] : "")
                                                                                                                                    + (U.Any() ? U[u] : "") + (V.Any() ? V[v] : "") + (W.Any() ? W[w] : "")
                                                                                                                                    + (X.Any() ? X[x] : "") + (Y.Any() ? Y[y] : "") + (Z.Any() ? Z[z] : "");
                                                                                                                                    ++indexA;
                                                                                                                                    ++jx;
                                                                                                                                    ++z;
                                                                                                                                }
                                                                                                                                catch (Exception)
                                                                                                                                {
                                                                                                                                    break;
                                                                                                                                }
                                                                                                                            }
                                                                                                                            ++y;
                                                                                                                        }
                                                                                                                        ++x;
                                                                                                                    }
                                                                                                                    ++w;
                                                                                                                }
                                                                                                                ++v;
                                                                                                            }
                                                                                                            ++u;
                                                                                                        }
                                                                                                        ++t;
                                                                                                    }
                                                                                                    ++s;
                                                                                                }
                                                                                                ++r;
                                                                                            }
                                                                                            ++q;
                                                                                        }
                                                                                        ++p;
                                                                                    }
                                                                                    ++o;
                                                                                }
                                                                                ++n;
                                                                            }
                                                                            ++m;
                                                                        }
                                                                        ++l;
                                                                    }
                                                                    ++k;
                                                                }
                                                                ++j;
                                                            }
                                                            ++ix;
                                                        }
                                                        ++h;
                                                    }
                                                    ++g;
                                                }
                                                ++f;
                                            }
                                            ++e;
                                        }
                                        ++d;
                                    }
                                    ++c;
                                }
                                ++b;
                            }
                            ++a;
                        }
                        break;
                    }

                                  
                        try
                        {                  
                            var itemToRemove = context.test_scenario_table.Where(c => c.module_id == id);
                            var itemToRemove2 = context.test_case_table.Where(c => c.module_id == id);
                            if (itemToRemove != null && itemToRemove2 != null) {
                            context.test_scenario_table.RemoveRange(context.test_scenario_table.Where(x => x.module_id == id));
                            context.test_case_table.RemoveRange(context.test_case_table.Where(x => x.module_id == id));
                            context.SaveChanges();
                            }
                            foreach (var dataFt2 in dataFT)
                            {
                                test_scenario_table test_Scenario_Table = new test_scenario_table();
                                test_Scenario_Table.module_id = id;
                                test_Scenario_Table.scenario_message = "Null";
                                test_Scenario_Table.factors = dataFt2;

                                context.test_scenario_table.Add(test_Scenario_Table);
                                context.SaveChanges();

                                test_case_table test_Case_Table = new test_case_table();
                                DateTime today = DateTime.Today;
                                test_Case_Table.module_id = id;
                                test_Case_Table.test_case_data = "Null";
                                test_Case_Table.expected_result = "Null";
                                test_Case_Table.test_round = "Null";
                                test_Case_Table.result = "Null";
                                test_Case_Table.date = today;
                                test_Case_Table.test_by = "Null";
                                test_Case_Table.path_test_case = "Null";
                                test_Case_Table.path_expected_result = "Null";
                                test_Case_Table.factors2 = dataFt2;

                                context.test_case_table.Add(test_Case_Table);
                                context.SaveChanges();
                            }
                                                        
                        }
                        catch(DbEntityValidationException e)
                        {
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
                            //return Content("<script language='javascript' type='text/javascript'>alert('"+ Ex2.InnerException.Message +"');</script>");
                        }
            }
            return RedirectToAction("Display_TestScenario",new { id = id,module_name = module_name});
        }
        public dynamic createDataforMapItems(int id)
        {
            using (var context = new DBModels())
            {
                var factor_name = context.factors_name_table.Where(s => s.module_id == id).ToList();
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

                foreach (var x in factors_name.Select((value, index) => new { value, index }))
                {
                    //var aa = x.index + "_" + x.value;
                    var index = x.index;
                    foreach (var item2 in factors_item)
                    {
                        if (item2.Contains(x.value))
                        {
                            factors_item2.Add(item2 + "_" + index);
                        }
                    }

                }
                return factors_item2;
            }           
        }
        public dynamic mapItems(int rowCount, List<string> item, List<string> dataaz)
        {
            var listaz = new[]
                 {
                    new { i = 0, n = "A" },new { i = 1, n = "B" },new { i = 2, n = "C" },new { i = 3, n = "D" },new { i = 4, n = "E" },
                    new { i = 5, n = "F" },new { i = 6, n = "G" },new { i = 7, n = "H" },new { i = 8, n = "I" },new { i = 9, n = "J" },
                    new { i = 10, n = "K" },new { i = 11, n = "L" },new { i = 12, n = "M" },new { i = 13, n = "N" },new { i = 14, n = "O" },
                    new { i = 15, n = "P" },new { i = 16, n = "Q" },new { i = 17, n = "R" },new { i = 18, n = "S" },new { i = 19, n = "T" },
                    new { i = 20, n = "U" },new { i = 21, n = "V" },new { i = 22, n = "W" },new { i = 23, n = "X" },new { i = 24, n = "Y" },
                    new { i = 25, n = "Z" }
                 }.ToList();

            int j = 0;
            //int d = 1;
            List<string> test2 = new List<string>();
            List<string> test3 = new List<string>();               
            //List<string> item4 = new List<string>();           
            bool isEmpty = !dataaz.Any();
            //bool isEmpty2 = !item.Any();
            if (isEmpty) { } else { test3 = dataaz; }
            //if (isEmpty2) { } else { item4 = item; }
            foreach (var factors_item3 in item)
            {
                int status = 0;
                string []temp = factors_item3.Split('_');
                if(temp.Count() != 3) { return 0; }
                for (int i = j; i <= Int32.Parse(temp[2]); i++)
                {
                    foreach (var item2 in listaz)
                    {
                        if (i == Int32.Parse(temp[2]) && i == item2.i)
                        {
                            int c = 0;                            
                            if (rowCount != 0)
                            {
                                c = rowCount + 1;
                                test3.Add(item2.n + c);
                                Factors_Description.Add(item2.n + c + ":" + temp[1]);
                            }
                            else
                            {
                                rowCount = 0;
                                test3.Add(item2.n + "1");
                                Factors_Description.Add(item2.n + c + ":" + temp[1]);
                            }
                            j = i + 1;
                            status = 1;
                            //++d;
                        }
                        if (i != item2.i && status != 1)
                        {
                            continue;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                if (status == 0)
                {
                    test2.Add(temp[0] + "_" + temp[1] + "_" + temp[2]);
                }
                int t = item.IndexOf(factors_item3);
                if (item.IndexOf(factors_item3) == item.Count - 1)
                {
                    if (test2.Count == 0)
                    {
                        return test3;
                    }
                    if (rowCount >= 0) { ++rowCount; }
                    mapItems(rowCount, test2, test3);
                }
                if (status == 1)
                {
                    continue;
                }
            }
            return test3;
        }
        public ActionResult EditDescription()
        {       
            String status = "false";
            String message = "";
            using (var context = new DBModels())
            {
                try
                {
                    int module_id = Int32.Parse(Request["module_id"]);
                    int t_id = Int32.Parse(Request["t_id"]);
                    string des = Request["edit_Des"];                    
                    if (des == "" || des == null)
                    {
                        des = "Null";
                    }
                    var result = context.test_scenario_table.First(a => a.module_id == module_id && a.test_scenario_id == t_id);
                    if (result != null)
                    {
                        result.scenario_message = des;                       
                        context.SaveChanges();
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
        public Boolean Check_Specialchar(string item_data)
        {
            var withoutSpecial = new string(item_data.Where(c => Char.IsLetterOrDigit(c)
                                            || Char.IsWhiteSpace(c)).ToArray());

            if (item_data != withoutSpecial)
            {
                return true;
            }
            return false;
        }
        [HttpPost]
        public ActionResult EditFactors(string model)
        {
            //List<string>
            //GenerateTestController.tid = new List<int>();       
            String status = "false";
            try
            {
                if (model == "")
                {
                    return Json(new { status = "false", message = "Data is empty" });
                }
                var js = new JavaScriptSerializer();
                dynamic result = js.DeserializeObject(model);
                int tid = Int32.Parse(result["tid"]);         
            List<string> listModify = new List<string>();
            for (int i = 1; i < 51000; i++)
            {
                try
                {
                    listModify.Add(result["field_" + i]);
                }
                catch(Exception ex)
                {
                    break;
                }
                
            }
            int total = listModify.Count;
            int x = 0;
            string Message = "";
            string az = "";
            foreach (var data in listModify.ToList())
            {
                if(x == 0 && data == "")
                {
                        listModify.RemoveAt(0);
                        ++x;
                        continue;
                }

                if(x != 0 && data != "")
                {
                        if(listModify.IndexOf(data) == listModify.Count - 1)
                        {                           
                        }
                        else
                        {
                          
                            bool res = Check_Specialchar(data);
                            if (res)
                            {
                                if (x == 1 && data.Contains(":")) { } else
                                {
                                    return Json(new { status = status, message = "item name is invalid" });
                                }                               
                            }
                        }                                            
                }
                
                if (listModify.IndexOf(data) == listModify.Count - 1)
                {
                    Message = data;
                        listModify.RemoveAt(0);
                }
                if(listModify.Count != 0 && data != "")
                    {
                        az += data + ",";
                        listModify.RemoveAt(0);
                    }
                if(data != "") { ++x; }
                
            }
            if(total != x) { return Json(new { status = "false", message = "This field is required" }); }
            
            String message = "";
            using (var context = new DBModels())
            {
                try
                {                    
                    var resultA = context.test_scenario_table.First(a => a.test_scenario_id == tid);
                    if (resultA != null)
                    {
                        resultA.scenario_message = Message;
                        resultA.factors = az;
                        
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
            catch (Exception ex2)
            {
                return Json(new { status = "false", message = ex2.Message });
            }
            //return View();
        }
    }
}