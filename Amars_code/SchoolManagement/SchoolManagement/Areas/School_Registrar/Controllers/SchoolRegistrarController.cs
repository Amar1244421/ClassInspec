using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolManagement.Areas.School_Registrar.Controllers
{
    public class SchoolRegistrarController : Controller
    {
        // GET: School_Registrar/SchoolRegistrar
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Edit()
        {
            string studentId = Request.QueryString["stuId"].ToString();
            DB_ClassEntities objentity = new DB_ClassEntities();
            

            SchoolManagement.Areas.School_Registrar.Models.StudentInformation objstudentdetail = new SchoolManagement.Areas.School_Registrar.Models.StudentInformation();
            objstudentdetail.StudentID = Int32.Parse(studentId.ToString());
            RegistrarDBLayer bl = new RegistrarDBLayer();
            DataSet dt = bl.GetStudentRecord(objstudentdetail);


            foreach (DataRow dr in dt.Tables[0].Rows)
            {
                objstudentdetail.StudentID = Int32.Parse(dr["StudentID"].ToString());
                objstudentdetail.first_names = dr["first_name"].ToString();
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
            int ContactId = 0;
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
                objstudentdetail.contact_number1 = (dr["contact_number1"].ToString());
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
        [ActionName("Edit")]
        public ActionResult Edit_Post()
        {
            if (ModelState.IsValid)
            {
                string studentId = Request.QueryString["stuId"].ToString();


                SchoolManagement.Areas.School_Registrar.Models.StudentInformation st = new SchoolManagement.Areas.School_Registrar.Models.StudentInformation();

                UpdateModel(st);
                st.StudentID = Int32.Parse(studentId);
                RegistrarDBLayer bl = new RegistrarDBLayer();
                DataSet dt2 = bl.GetParentId(st.StudentID);

                foreach (DataRow dr in dt2.Tables[0].Rows)
                {
                    st.Contact_PersonID = Int32.Parse(dr["Contact_PersonID"].ToString());
                }
                RegistrarDBLayer businesslayer = new RegistrarDBLayer();
                businesslayer.updateStudentInfo(st);
            }
            return View();

        }

        public ActionResult Delete()
        {
            string studentId = Request.QueryString["stuId"].ToString();
            DB_ClassEntities objentity = new DB_ClassEntities();
             SchoolManagement.Areas.School_Registrar.Models.StudentInformation objstudentdetail = new SchoolManagement.Areas.School_Registrar.Models.StudentInformation();
            objstudentdetail.StudentID = Int32.Parse(studentId.ToString());
            RegistrarDBLayer bl = new RegistrarDBLayer();
            DataSet dt2 = bl.GetParentId(objstudentdetail.StudentID);

            foreach (DataRow dr in dt2.Tables[0].Rows)
            {
                objstudentdetail.Contact_PersonID = Int32.Parse(dr["Contact_PersonID"].ToString());
            }

            bl.DeleteStudentRecord(objstudentdetail);
            return Redirect("/School_Registrar/SchoolRegistrar/Contact");

        }
        public ActionResult Contact(string text)
        {
            RegistrarDBLayer businesslayer = new RegistrarDBLayer();
             SchoolManagement.Areas.School_Registrar.Models.StudentInformation std = new  SchoolManagement.Areas.School_Registrar.Models.StudentInformation();
            std.ClassTypeId = text;
            DataSet dt = businesslayer.GetStudentInfo(std);

            var plist = dt.Tables[0].AsEnumerable().Select(x => new  SchoolManagement.Areas.School_Registrar.Models.StudentInformation
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
    }
}
