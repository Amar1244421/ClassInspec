using SchoolManagement.Areas.School_Registrar.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace SchoolManagement.Areas.School_Registrar
{
    public class RegistrarDBLayer
    {
        public DataSet GetStudentInfo(SchoolManagement.Areas.School_Registrar.Models.StudentInformation std)
        {
            List<StudentInformation> students = new List<StudentInformation>();
            using (SqlConnection myCon = new SqlConnection(WebConfigurationManager.ConnectionStrings["SqlDBClass"].ConnectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("GetStudentDetails", myCon);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.Add("@ClassId", SqlDbType.Int).Value = Int32.Parse(std.ClassTypeId.ToString());


                    DataSet dt = new DataSet();
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        //DataSet dt = new DataSet();
                        sda.Fill(dt);
                    }
                    return dt;
                }

                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    myCon.Close();
                    myCon.Dispose();
                }


            }
        }
        public DataSet GetStudentRecord(SchoolManagement.Areas.School_Registrar.Models.StudentInformation std)
        {
            List<StudentInformation> students = new List<StudentInformation>();
            using (SqlConnection myCon = new SqlConnection(WebConfigurationManager.ConnectionStrings["SqlDBClass"].ConnectionString))
            {
                DataSet dsCustomers = new DataSet();
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Student where StudentID = " + std.StudentID))
                {
                    cmd.Connection = myCon;
                    myCon.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        //Create a new DataSet.

                        dsCustomers.Tables.Add("Customers");

                        //Load DataReader into the DataTable.
                        dsCustomers.Tables[0].Load(sdr);
                    }
                    myCon.Close();
                }
                return dsCustomers;
            }
        }
        public DataSet GetParentId(int std)
        {
            List<StudentInformation> students = new List<StudentInformation>();
            using (SqlConnection myCon = new SqlConnection(WebConfigurationManager.ConnectionStrings["SqlDBClass"].ConnectionString))
            {
                DataSet dsCustomers = new DataSet();
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Contact_Person_Student where StudentID = " + std))
                {
                    cmd.Connection = myCon;
                    myCon.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        //Create a new DataSet.

                        dsCustomers.Tables.Add("Customers1");

                        //Load DataReader into the DataTable.
                        dsCustomers.Tables[0].Load(sdr);
                    }
                    myCon.Close();
                }
                return dsCustomers;
            }
        }
        public DataSet GetParentRecord(int std)
        {
            List<SchoolManagement.Areas.School_Registrar.Models.StudentInformation> students = new List<SchoolManagement.Areas.School_Registrar.Models.StudentInformation>();
            using (SqlConnection myCon = new SqlConnection(WebConfigurationManager.ConnectionStrings["SqlDBClass"].ConnectionString))
            {
                DataSet dsCustomers = new DataSet();
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Contact_Person where Contact_PersonID = " + std))
                {
                    cmd.Connection = myCon;
                    myCon.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        //Create a new DataSet.

                        dsCustomers.Tables.Add("Customers1");

                        //Load DataReader into the DataTable.
                        dsCustomers.Tables[0].Load(sdr);
                    }
                    myCon.Close();
                }
                return dsCustomers;
            }
        }

        public void DeleteStudentRecord(SchoolManagement.Areas.School_Registrar.Models.StudentInformation studentcontactperson)
        {
            //List<StudentInformation_Result> students = new List<StudentInformation_Result>();
            //using (SqlConnection myCon = new SqlConnection("Data Source=DESKTOP-50L3KE8\\SQLEXPRESS;Initial Catalog=DB_Class;Integrated Security=True"))
            //{

            //    SqlCommand cmd = new SqlCommand("Delete FROM Student where StudentID = " + studentcontactperson.StudentID);

            //        cmd.Connection = myCon;
            //        myCon.Open();
            //        cmd.ExecuteNonQuery();                      
            //        myCon.Close();


            //}
            using (SqlConnection myCon = new SqlConnection(WebConfigurationManager.ConnectionStrings["SqlDBClass"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("Student_Record_Delete", myCon);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@StudentID", SqlDbType.VarChar).Value = studentcontactperson.StudentID;
                cmd.Parameters.Add("@Contact_PersonID", SqlDbType.VarChar).Value = studentcontactperson.Contact_PersonID;


                try
                {
                    myCon.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }

        }

        public void updateStudentInfo(StudentInformation studentcontactperson)
        {
            using (SqlConnection myCon = new SqlConnection(WebConfigurationManager.ConnectionStrings["SqlDBClass"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("Student_Record_update", myCon);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@StudentID", SqlDbType.VarChar).Value = studentcontactperson.StudentID;
                cmd.Parameters.Add("@first_name", SqlDbType.VarChar).Value = studentcontactperson.first_names;
                cmd.Parameters.Add("@middle_name", SqlDbType.VarChar).Value = studentcontactperson.middle_names;
                cmd.Parameters.Add("@last_name", SqlDbType.VarChar).Value = studentcontactperson.last_names;
                cmd.Parameters.Add("@birthdate", SqlDbType.Date).Value = studentcontactperson.birthdate;
                cmd.Parameters.Add("@contact", SqlDbType.VarChar).Value = studentcontactperson.contact;
                cmd.Parameters.Add("@Gender", SqlDbType.VarChar).Value = studentcontactperson.Gender;
                cmd.Parameters.Add("@contact_email", SqlDbType.VarChar).Value = studentcontactperson.contact_emails;
                cmd.Parameters.Add("@LocalAddress", SqlDbType.VarChar).Value = studentcontactperson.LocalAddress;
                cmd.Parameters.Add("@PermanentAddress", SqlDbType.VarChar).Value = studentcontactperson.PermanentAddress;
                cmd.Parameters.Add("@Adharcard", SqlDbType.BigInt).Value = studentcontactperson.AdharNumber;
                cmd.Parameters.Add("@Contact_PersonID", SqlDbType.VarChar).Value = studentcontactperson.Contact_PersonID;
                cmd.Parameters.Add("@Parent_first_name", SqlDbType.VarChar).Value = studentcontactperson.first_name;
                cmd.Parameters.Add("@Parent_last_name", SqlDbType.VarChar).Value = studentcontactperson.last_name;
                cmd.Parameters.Add("@Parent_middle_name", SqlDbType.VarChar).Value = studentcontactperson.middle_name;
                cmd.Parameters.Add("@contact_number1", SqlDbType.VarChar).Value = studentcontactperson.contact_number1;
                cmd.Parameters.Add("@contact_number2", SqlDbType.VarChar).Value = studentcontactperson.contact_number2;
                cmd.Parameters.Add("@Parent_contact_email", SqlDbType.VarChar).Value = studentcontactperson.contact_email;
                cmd.Parameters.Add("@RelationWith_Student", SqlDbType.VarChar).Value = studentcontactperson.RelationWith_Student;
                cmd.Parameters.Add("@Address", SqlDbType.VarChar).Value = studentcontactperson.Address;

                try
                {
                    myCon.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                //SqlCommand cmd1 = new SqlCommand("Contact_Person_Info", myCon);
                //cmd1.CommandType = System.Data.CommandType.StoredProcedure;

                //cmd1.Parameters.Add("@first_name", SqlDbType.VarChar).Value = studentcontactperson.first_name;
                //cmd1.Parameters.Add("@last_name", SqlDbType.VarChar).Value = studentcontactperson.last_name;
                //cmd1.Parameters.Add("@middle_name", SqlDbType.VarChar).Value = studentcontactperson.middle_name;
                //cmd1.Parameters.Add("@contact_number1", SqlDbType.VarChar).Value = studentcontactperson.contact_number1;
                //cmd1.Parameters.Add("@contact_number2", SqlDbType.VarChar).Value = studentcontactperson.contact_number2;
                //cmd1.Parameters.Add("@contact_email", SqlDbType.VarChar).Value = studentcontactperson.contact_email;
                //cmd1.Parameters.Add("@RelationWith_Student", SqlDbType.VarChar).Value = studentcontactperson.RelationWith_Student;
                //cmd1.Parameters.Add("@Address", SqlDbType.VarChar).Value = studentcontactperson.Address;
                //try
                //{
                //    //myCon.Open();
                //    cmd1.ExecuteNonQuery();
                //}
                //catch (Exception ex)
                //{
                //    throw ex;
                //}

                finally
                {
                    myCon.Close();
                    myCon.Dispose();
                }

            }
        }
    }
}