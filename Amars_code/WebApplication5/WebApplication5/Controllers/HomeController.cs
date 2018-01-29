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
using System.Dynamic;

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
        [HttpPost]
        public ActionResult Contact(string text)
        { 
            BusinessLayer businesslayer = new BusinessLayer();
            WebApplication5.Models.StudentInformation_Result std = new WebApplication5.Models.StudentInformation_Result();
            std.ClassTypeId = text;
            DataSet dt = businesslayer.GetStudentInfo(std);

            var plist = dt.Tables[0].AsEnumerable().Select(x => new WebApplication5.Models.StudentInformation_Result
            {
                StudentID = x.Field<int>("StudentID"),
                first_names = x.Field<string>("first_name"),
                last_names = x.Field<string>("last_name"),
                birthdate = x.Field<DateTime>("birthdate"),
                contact = x.Field<string>("contact"),
                contact_emails = x.Field<string>("contact_email"),
                Gender = x.Field<string>("Gender"),
                AdharNumber = x.Field<Int64>("Adharcard"),
                middle_names = x.Field<string>("middle_name"),
                LocalAddress = x.Field<string>("LocalAddress"),
                PermanentAddress = x.Field<string>("PermanentAddress"),                
                ClassTypeId = x.Field<string>("ClassTypeId"),

            });
            return View(plist);
        }
        public ActionResult Student_registrar()
        {
            ViewBag.Message = "Your application description page.";

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





        ///////////////////////////////////////////////////////////edit////////////////////////////////////////////////

        public ActionResult Edit()
        {
            string studentId = Request.QueryString["stuId"].ToString();
            DB_ClassEntities objentity = new DB_ClassEntities();
            WebApplication5.Models.StudentInformation_Result objstudentdetail = new WebApplication5.Models.StudentInformation_Result();
            objstudentdetail.StudentID = Int32.Parse(studentId.ToString());
            BusinessLayer bl = new BusinessLayer();
            DataSet dt = bl.GetStudentRecord(objstudentdetail);
            
            
            foreach (DataRow dr in dt.Tables[0].Rows)
            {
                objstudentdetail.StudentID = Int32.Parse(dr["StudentID"].ToString());
                objstudentdetail.first_names =dr["first_name"].ToString();
                objstudentdetail.last_names = dr["last_name"].ToString();
                objstudentdetail.birthdate = dr.Field<System.DateTime>("birthdate");
                objstudentdetail.contact = dr["contact"].ToString();
                objstudentdetail.contact_emails = dr["contact_email"].ToString();
                objstudentdetail.Gender = dr["Gender"].ToString();
                objstudentdetail.AdharNumber = Int32.Parse(dr["Adharcard"].ToString());
                objstudentdetail.middle_names = dr["middle_name"].ToString();
                objstudentdetail.LocalAddress = dr["LocalAddress"].ToString();
                objstudentdetail.PermanentAddress = dr["PermanentAddress"].ToString();
                objstudentdetail.ClassTypeId = dr["ClassTypeId"].ToString();
                

            }
            DataSet dt2 = bl.GetParentId(objstudentdetail.StudentID);
            int ContactId=0;
            foreach (DataRow dr in dt2.Tables[0].Rows)
            {
                 ContactId = Int32.Parse(dr["Contact_PersonID"].ToString());
            }
                DataSet dt3 = bl.GetParentRecord(ContactId);
            foreach (DataRow dr in dt3.Tables[0].Rows)
            {
                objstudentdetail.Contact_PersonID = Int32.Parse(dr["Contact_PersonID"].ToString());
                objstudentdetail.first_name = dr["first_name"].ToString();
                objstudentdetail.last_name = dr["last_name"].ToString();
                objstudentdetail.contact_number1 =(dr["contact_number1"].ToString());
                objstudentdetail.contact_number2 = dr["contact_number2"].ToString();
                objstudentdetail.contact_email = dr["contact_email"].ToString();
                objstudentdetail.RelationWith_Student = dr["RelationWith_Student"].ToString();               
                objstudentdetail.middle_name = dr["middle_name"].ToString();
                objstudentdetail.Address = dr["Address"].ToString();
              


            }
            return View(objstudentdetail);
            
        }
        ///



        /// This method edit function
        ///
        [HttpPost]
        [ActionName ("Edit")]
        public ActionResult Edit_Post()
        {
            if (ModelState.IsValid)
            {
                string studentId = Request.QueryString["stuId"].ToString();
                

                Models.StudentInformation_Result st = new Models.StudentInformation_Result();
                
                UpdateModel(st);
                st.StudentID = Int32.Parse(studentId);
                BusinessLayer bl = new BusinessLayer();
                DataSet dt2 = bl.GetParentId(st.StudentID);

                foreach (DataRow dr in dt2.Tables[0].Rows)
                {
                    st.Contact_PersonID = Int32.Parse(dr["Contact_PersonID"].ToString());
                }
                BusinessLayer businesslayer = new BusinessLayer();
                businesslayer.updateStudentInfo(st);
            }
            return View();
            
        }

        public ActionResult Delete()
        {
            string studentId = Request.QueryString["stuId"].ToString();
            DB_ClassEntities objentity = new DB_ClassEntities();
            WebApplication5.Models.StudentInformation_Result objstudentdetail = new WebApplication5.Models.StudentInformation_Result();
            objstudentdetail.StudentID = Int32.Parse(studentId.ToString());
            BusinessLayer bl = new BusinessLayer();
            DataSet dt2 = bl.GetParentId(objstudentdetail.StudentID);
            
            foreach (DataRow dr in dt2.Tables[0].Rows)
            {
                objstudentdetail.Contact_PersonID = Int32.Parse(dr["Contact_PersonID"].ToString());
            }
        
            bl.DeleteStudentRecord(objstudentdetail);
            return Redirect("/Home/Student_registrar");

        }
    }
}