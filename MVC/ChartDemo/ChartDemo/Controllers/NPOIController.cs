using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.Data;
using System.IO;
using System.Reflection;
using NPOI.SS.Formula.Functions;


namespace ChartDemo.Controllers
{
    /// <summary>
    /// 不知道為什麼 2.6.0 套件裝不起來，所以還是裝 2.5.5
    /// </summary>
    public class NPOIController : Controller
    {
        
        public ActionResult Export2()
        {
            string bb = Request["bb"];
            ViewBag.Msg = $"Export2 msg:{bb}";
            // GetIWorkbook
            string filename = "test1.xlsx";
            IWorkbook wb = GetIWorkbook( filename);
            wb.GetSheetAt(0).GetRow(0).CreateCell(0).SetCellValue($"輸入:{bb}");

            using (MemoryStream stream = new MemoryStream())
            {
                //wb.SaveAs(stream);
                wb.Write(stream);

                //System.Web.Mvc.FileContentResult
                return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", filename);
                //return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "customer.xlsx");
            }
        }
        public ActionResult Export()
        {

            // 改用 npoi 來寫
            //List<EQP_DATA_TABLE> EQP_DATA_List = context.EQP_DATA_TABLE.ToList();
            DataTable dt1 = CreateDataTable();
            string filename = "test1.xlsx";
            IWorkbook wb = DataTableToIWorkbook(dt1, filename);
            //return View();
            using (MemoryStream stream = new MemoryStream())
            {
                //wb.SaveAs(stream);
                wb.Write(stream);

                //System.Web.Mvc.FileContentResult
                return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", filename);
                //return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "customer.xlsx");
            }
        }
        public static NPOI.SS.UserModel.IWorkbook DataTableToIWorkbook(DataTable dt, string optionalstr = "xlsx")
        {
            //建立Excel 2003檔案
            ISheet ws; NPOI.SS.UserModel.IWorkbook wb;
            if (optionalstr.EndsWith("s"))
            {
                wb = new HSSFWorkbook(); // D:\devs\Test\MyWebApplicationTest1\MyWebApplicationTest1\dll\NPOI.dll
            }
            else
            {
                //建立Excel 2007檔案
                wb = new XSSFWorkbook(); // D:\devs\Test\MyWebApplicationTest1\MyWebApplicationTest1\dll\NPOI.OOXML.dll
                //ISheet ws;
            }

            if (dt.TableName != string.Empty)
            {
                ws = wb.CreateSheet(dt.TableName);
            }
            else
            {
                ws = wb.CreateSheet("Sheet1");
            }

            ws.CreateRow(0);//第一 Row 為欄位名稱
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                ws.GetRow(0).CreateCell(i).SetCellValue(dt.Columns[i].ColumnName);
                //ws.GetRow(0).GetCell(i).CellStyle = helper.TitleStyle;
            }

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ws.CreateRow(i + 1);
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    ws.GetRow(i + 1).CreateCell(j).SetCellValue(dt.Rows[i][j].ToString());
                    //ws.GetRow(i + 1).GetCell(j).CellStyle = helper.DefaultStyle;
                }
            }

            return wb;
            //FileStream file = new FileStream(@"d:\tmp\npoi.xls", FileMode.Create);//產生檔案
            //wb.Write(file);
            //file.Close();
        }
        public static DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);
            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }
        private DataTable CreateDataTable()
        {
            DataTable dt = new DataTable();
            dt.Clear();
            dt.Columns.Add("Name");
            dt.Columns.Add("Marks");


            DataRow _ravi = dt.NewRow();
            _ravi["Name"] = "ravi";
            _ravi["Marks"] = "500";
            dt.Rows.Add(_ravi);

            _ravi = dt.NewRow();
            _ravi["Name"] = "ravibb";
            _ravi["Marks"] = "5001";
            dt.Rows.Add(_ravi);
            return dt;
        }

        public static NPOI.SS.UserModel.IWorkbook GetIWorkbook(string optionalstr = "xlsx")
        {
            //建立Excel 2003檔案
            ISheet ws; NPOI.SS.UserModel.IWorkbook wb;
            if (optionalstr.EndsWith("s"))
            {
                wb = new HSSFWorkbook(); // D:\devs\Test\MyWebApplicationTest1\MyWebApplicationTest1\dll\NPOI.dll
            }
            else
            {
                //建立Excel 2007檔案
                wb = new XSSFWorkbook(); // D:\devs\Test\MyWebApplicationTest1\MyWebApplicationTest1\dll\NPOI.OOXML.dll
                //ISheet ws;
            }

            ws = wb.CreateSheet("Sheet1");

            ws.CreateRow(0);
            

            return wb;
            //FileStream file = new FileStream(@"d:\tmp\npoi.xls", FileMode.Create);//產生檔案
            //wb.Write(file);
            //file.Close();
        }

    }
}