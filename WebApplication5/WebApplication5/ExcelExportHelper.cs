using Microsoft.Office.Interop.Excel;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;

namespace WebApplication5
{
    public class ExcelExportHelper
    {
        public static string ExcelContentType
        {
            get
            { return "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"; }
        }

        //public static DataTable ListToDataTable<T>(List<T> data)
        //{
        //    PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
        //    DataTable dataTable = new DataTable();

        //    for (int i = 0; i < properties.Count; i++)
        //    {
        //        PropertyDescriptor property = properties[i];
        //        dataTable.Columns.Add(property.Name, Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType);
        //    }

        //    object[] values = new object[properties.Count];
        //    foreach (T item in data)
        //    {
        //        for (int i = 0; i < values.Length; i++)
        //        {
        //            values[i] = properties[i].GetValue(item);
        //        }

        //        dataTable.Rows.Add(values);
        //    }
        //    return dataTable;
        //}

        public static byte[] ExportExcel(DataSet dataset, string heading = "", bool showSrNo = false, params string[] columnsToTake)
        {
            System.Data.DataTable dt0 = dataset.Tables[0];
            System.Data.DataTable dt1 = dataset.Tables[1];
            System.Data.DataTable dt2 = dataset.Tables[2];


            System.Data.DataTable dataTable = new System.Data.DataTable();
            byte[] result = null;
            using (ExcelPackage package = new ExcelPackage())
            {
                ExcelWorksheet workSheet = package.Workbook.Worksheets.Add(String.Format("{0} Data", heading));
                // add the content into the Excel file  

                workSheet.Cells[10,5].LoadFromDataTable(dt0, false); //students data
                workSheet.Cells[4, 6].LoadFromDataTable(dt1,false);  // tearchers
                workSheet.Cells[3, 6].LoadFromDataTable(dt2, false);
                workSheet.Cells[3, 5].Value = "ClassName";
                workSheet.Cells[9, 5].Value = "Student ID";
                workSheet.Cells[9, 6].Value = "Student Name";
                workSheet.Cells[4, 5].Value="Teachers Name";
                workSheet.Cells[5, 5].Value = "Date";
                workSheet.Cells[6, 5].Value = "Time";
                workSheet.Cells[9, 7].Value = "Present/Absent";
                workSheet.Column(5).AutoFit();
                workSheet.Column(6).AutoFit();
                workSheet.Column(7).AutoFit();
                

                using (ExcelRange s = workSheet.Cells[4, 5, 6, 6])
                {
                    s.Style.Font.Color.SetColor(System.Drawing.Color.White);
                    s.Style.Font.Bold = true;
                    s.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    s.Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#1fb5ad"));                   
                }

                using (ExcelRange s = workSheet.Cells[2, 5, 2, 6])
                {
                    s.Style.Font.Color.SetColor(System.Drawing.Color.White);
                    s.Style.Font.Bold = true;
                    s.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    s.Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#1fb5ad"));
                }


                using (ExcelRange r = workSheet.Cells[3, 5, 6, 6])
                {
                    r.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    r.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    r.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    r.Style.Border.Right.Style = ExcelBorderStyle.Thin;

                    r.Style.Border.Top.Color.SetColor(System.Drawing.Color.Black);
                    r.Style.Border.Bottom.Color.SetColor(System.Drawing.Color.Black);
                    r.Style.Border.Left.Color.SetColor(System.Drawing.Color.Black);
                    r.Style.Border.Right.Color.SetColor(System.Drawing.Color.Black);
                }
                
                // format header - bold, yellow on black  
                using (ExcelRange r = workSheet.Cells[9,5,9,7])
                {
                    r.Style.Font.Color.SetColor(System.Drawing.Color.White);
                    r.Style.Font.Bold = true;
                    r.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    r.Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#1fb5ad"));
                }
              

                // format cells -add borders
                using (ExcelRange r = workSheet.Cells[9, 5, 9 + dt0.Rows.Count, 7])
                {
                    r.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    r.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    r.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    r.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    

                    r.Style.Border.Top.Color.SetColor(System.Drawing.Color.Black);
                    r.Style.Border.Bottom.Color.SetColor(System.Drawing.Color.Black);
                    r.Style.Border.Left.Color.SetColor(System.Drawing.Color.Black);
                    r.Style.Border.Right.Color.SetColor(System.Drawing.Color.Black);
                }
                ExcelWorksheet isMilitryddList = package.Workbook.Worksheets.Add("IsMilitry");
                var attendance = new string[]
                 {
                     "Present","Absent"
                   };
                var vallistattendance = workSheet.DataValidations.AddListValidation(workSheet.Cells[10, 7, dt0.Rows.Count, 7].Address);
                for (int index = 1; index <= attendance.Length; index++)
                {
                    isMilitryddList.Cells[1, index].Value = attendance[index - 1];
                }
                var addressattendance = isMilitryddList.Cells[1, 1, 1, attendance.Count()].Address;
                var arrismilitry = addressattendance.Split(':');
                var ismilitrychar1 = arrismilitry[0][0];
                var ismilitrynum1 = arrismilitry[0].Trim(ismilitrychar1);
                var ismilitrychar2 = arrismilitry[1][0];
                var ismilitrynum2 = arrismilitry[1].Trim(ismilitrychar2);
                vallistattendance.Formula.ExcelFormula = string.Format("=IsMilitry!${0}${1}:${2}${3}", ismilitrychar1, ismilitrynum1, ismilitrychar2, ismilitrynum2);
                vallistattendance.ShowErrorMessage = true;
                vallistattendance.Error = "Select from List of Values ...";

                //using (ExcelRange r = workSheet.Cells[startRowFromT + 1, 1, startRowFromT + dt1.Rows.Count, dt1.Columns.Count])
                //{
                //    r.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                //    r.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                //    r.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                //    r.Style.Border.Right.Style = ExcelBorderStyle.Thin;

                //    r.Style.Border.Top.Color.SetColor(System.Drawing.Color.Black);
                //    r.Style.Border.Bottom.Color.SetColor(System.Drawing.Color.Black);
                //    r.Style.Border.Left.Color.SetColor(System.Drawing.Color.Black);
                //    r.Style.Border.Right.Color.SetColor(System.Drawing.Color.Black);
                //}

                // removed ignored columns  
                //for (int i = dataTable.Columns.Count - 1; i >= 0; i--)
                //{
                //    if (i == 0 && showSrNo)
                //    {
                //        continue;
                //    }
                //    if (!columnsToTake.Contains(dataTable.Columns[i].ColumnName))
                //    {
                //        workSheet.DeleteColumn(i + 1);
                //    }
                //}

                //if (!String.IsNullOrEmpty(heading))
                //{
                //    workSheet.Cells["A1"].Value = heading;
                //    workSheet.Cells["A1"].Style.Font.Size = 20;

                //    workSheet.InsertColumn(1, 1);
                //    workSheet.InsertRow(1, 1);
                //    workSheet.Column(1).Width = 5;
                //}

                result = package.GetAsByteArray();
            }

            return result;
        }

        //public static byte[] ExportExcel<T>(List<T> data, string Heading = "", bool showSlno = false, params string[] ColumnsToTake)
        //{
        //    return ExportExcel(ListToDataTable<T>(data), Heading, showSlno, ColumnsToTake);
        //}

    }
}