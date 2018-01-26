using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Configuration;
using System.Data.OleDb;
using System.Xml;
using System.Data.SqlClient;

namespace WebApplication5.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            Models.TechnologyViewModel model = new Models.TechnologyViewModel();
            return View(model);
        }

        [HttpGet]
        public FileContentResult ExportToExcel()
        {
            DB_ClassEntities db = new DB_ClassEntities();
            BusinessLayer bl = new BusinessLayer();

            DataSet dt = bl.Student_Teacherdata();

            string[] columns = { "Name", "Project", "Developer" };
            byte[] filecontent = ExcelExportHelper.ExportExcel(dt, "Technology", true, columns);
            return File(filecontent, ExcelExportHelper.ExcelContentType, "Technologies.xlsx");

        }

        [HttpPost]
        public ActionResult Index(HttpPostedFileBase file)
        {
            DataSet ds = new DataSet();
            if (Request.Files["file"].ContentLength > 0)
            {
                string fileExtension =
                                     System.IO.Path.GetExtension(Request.Files["file"].FileName);

                if (fileExtension == ".xls" || fileExtension == ".xlsx")
                {
                    string fileLocation = Server.MapPath("~/Content/") + Request.Files["file"].FileName;
                    if (System.IO.File.Exists(fileLocation))
                    {

                        System.IO.File.Delete(fileLocation);
                    }
                    Request.Files["file"].SaveAs(fileLocation);
                    string excelConnectionString = string.Empty;
                    excelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
                    fileLocation + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                    //connection String for xls file format.
                    if (fileExtension == ".xls")
                    {
                        excelConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" +
                        fileLocation + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                    }
                    //connection String for xlsx file format.
                    else if (fileExtension == ".xlsx")
                    {
                        excelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
                        fileLocation + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                    }
                    //Create Connection to Excel work book and add oledb namespace
                    OleDbConnection excelConnection = new OleDbConnection(excelConnectionString);
                    excelConnection.Open();
                    DataTable dt = new DataTable();

                    dt = excelConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                    if (dt == null)
                    {
                        return null;
                    }

                    String[] excelSheets = new String[dt.Rows.Count];
                    int t = 0;
                    //excel data saves in temp file here.
                    foreach (DataRow row in dt.Rows)
                    {
                        excelSheets[t] = row["TABLE_NAME"].ToString();
                        t++;
                    }
                    OleDbConnection excelConnection1 = new OleDbConnection(excelConnectionString);


                    string query = string.Format("Select * from [{0}]", excelSheets[0]);
                    using (OleDbDataAdapter dataAdapter = new OleDbDataAdapter(query, excelConnection1))
                    {
                        dataAdapter.Fill(ds);
                    }
                }
                if (fileExtension.ToString().ToLower().Equals(".xml"))
                {
                    string fileLocation = Server.MapPath("~/Content/") + Request.Files["FileUpload"].FileName;
                    if (System.IO.File.Exists(fileLocation))
                    {
                        System.IO.File.Delete(fileLocation);
                    }

                    Request.Files["FileUpload"].SaveAs(fileLocation);
                    XmlTextReader xmlreader = new XmlTextReader(fileLocation);
                    // DataSet ds = new DataSet();
                    ds.ReadXml(xmlreader);
                    xmlreader.Close();
                }


                for (int i = 8; i < ds.Tables[0].Rows.Count; i++)
                {
                   // string conn = ConfigurationManager.ConnectionStrings["Data Source=DESKTOP-BQRJ2ID;Initial Catalog=DB_Class;Integrated Security=True"].ConnectionString;
                    using (SqlConnection myCon = new SqlConnection("Data Source=DESKTOP-BQRJ2ID;Initial Catalog=DB_Class;Integrated Security=True"))
                    {
                        //SqlConnection con = new SqlConnection(myCon);
                        //SqlCommand cmd = new SqlCommand("Student_Info", myCon);
                        string query = "Insert into Student_presense(StudentID) Values('" + ds.Tables[0].Rows[i][0] + "')";
                        //ds.Tables[0].Rows[i][0].ToString() + "','" + ds.Tables[0].Rows[i][1].ToString() + "')";
                        myCon.Open();
                        SqlCommand cmd = new SqlCommand(query, myCon);
                        cmd.ExecuteNonQuery();
                        myCon.Close();
                    }
                }
            }
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        /// <summary>
        /// For Storing the Student Information
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ActionName("Student_Info")]
        public ActionResult Student_InfoPost()
        {
            if (ModelState.IsValid)
            {
               Models.StudentInformation_Result st = new Models.StudentInformation_Result();
                UpdateModel(st);
                BusinessLayer businesslayer = new BusinessLayer();
                businesslayer.AddStudentInfo(st);
            }
                return View();
        }

        [HttpGet]
        [ActionName("Student_Info")]
        public ActionResult Student_InfoGet()
        {
            return View();
        }
        [HttpPost]
        [ActionName("Instructor_Info")]
        public ActionResult Instructor_InfoPost()
        {
            if (ModelState.IsValid)
            {
                Instructor it = new Instructor();
                UpdateModel(it);
                BusinessLayer businesslayer = new BusinessLayer();
                businesslayer.AddInstructorInfo(it);
            }
            return View();
        }
        [HttpGet]
        [ActionName("Instructor_Info")]
        public ActionResult Instructor_InfoGet()
        {
            return View();
        }
        public ActionResult Class_Details()
        {
            return View();
        }
        public ActionResult Class_Schedule()
        {
            return View();
        }
        /// <summary>
        /// For Storing the data Of Contact Person
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ActionName("ContactPerson_Info")]
        public ActionResult ContactPerson_InfoGet()
        {
            return View(); 
        }

        [HttpPost]
        [ActionName("ContactPerson_Info")]
        public ActionResult ContactPerson_InfoPost()
        {
            if (ModelState.IsValid)
            {
                Contact_Person cp = new Contact_Person();
                UpdateModel(cp);
                BusinessLayer businesslayer = new BusinessLayer();
                businesslayer.AddContact_Person_Info(cp);
            }
            return View();
        }
        /// <summary>
        /// For Attendance Uploading and Downloading
        /// </summary>
        /// <returns></returns>
        [ActionName ("Attendance")]
        public ActionResult Attendance_Excel()
        {
            return View();
        }
        [ActionName("Attendance")]
        public ActionResult Attendance_ToUpload()
        {
            return View();
        }
    }
}