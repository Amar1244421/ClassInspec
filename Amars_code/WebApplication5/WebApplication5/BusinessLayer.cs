using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;


namespace WebApplication5
{
    public class BusinessLayer
    {
        public DataSet Student_Teacherdata()
        {
            using (SqlConnection myCon = new SqlConnection("Data Source =DESKTOP-BQRJ2ID;Initial Catalog =DB_Class;Integrated Security=True"))
            {
                SqlCommand cmd = new SqlCommand("StudentInformation", myCon);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                DataSet dt = new DataSet();
                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    //DataSet dt = new DataSet();
                    sda.Fill(dt);
                }
                return dt;
            }
        }

        public void AddStudentInfo(Models.StudentInformation_Result studentcontactperson)
        {
            using (SqlConnection myCon = new SqlConnection("Data Source=DESKTOP-BQRJ2ID;Initial Catalog=DB_Class;Integrated Security=True"))
            {
                SqlCommand cmd = new SqlCommand("Student_Info", myCon);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

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

                try
                {
                    myCon.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                SqlCommand cmd1 = new SqlCommand("Contact_Person_Info", myCon);
                cmd1.CommandType = System.Data.CommandType.StoredProcedure;

                cmd1.Parameters.Add("@first_name", SqlDbType.VarChar).Value = studentcontactperson.first_name;
                cmd1.Parameters.Add("@last_name", SqlDbType.VarChar).Value = studentcontactperson.last_name;
                cmd1.Parameters.Add("@middle_name", SqlDbType.VarChar).Value = studentcontactperson.middle_name;
                cmd1.Parameters.Add("@contact_number1", SqlDbType.VarChar).Value = studentcontactperson.contact_number1;
                cmd1.Parameters.Add("@contact_number2", SqlDbType.VarChar).Value = studentcontactperson.contact_number2;
                cmd1.Parameters.Add("@contact_email", SqlDbType.VarChar).Value = studentcontactperson.contact_email;
                cmd1.Parameters.Add("@RelationWith_Student", SqlDbType.VarChar).Value = studentcontactperson.RelationWith_Student;
                cmd1.Parameters.Add("@Address", SqlDbType.VarChar).Value = studentcontactperson.Address;
                try
                {
                    //myCon.Open();
                    cmd1.ExecuteNonQuery();
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

        public DataSet GetStudentInfo(WebApplication5.Models.StudentInformation_Result std)
        {
            List<StudentInformation_Result> students = new List<StudentInformation_Result>();
            using (SqlConnection myCon = new SqlConnection("Data Source=DESKTOP-50L3KE8\\SQLEXPRESS;Initial Catalog=DB_Class;Integrated Security=True"))
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
        public DataSet GetStudentRecord(WebApplication5.Models.StudentInformation_Result std)
        {
            List<StudentInformation_Result> students = new List<StudentInformation_Result>();
            using (SqlConnection myCon = new SqlConnection("Data Source=DESKTOP-50L3KE8\\SQLEXPRESS;Initial Catalog=DB_Class;Integrated Security=True"))
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
            List<StudentInformation_Result> students = new List<StudentInformation_Result>();
            using (SqlConnection myCon = new SqlConnection("Data Source=DESKTOP-50L3KE8\\SQLEXPRESS;Initial Catalog=DB_Class;Integrated Security=True"))
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
            List<StudentInformation_Result> students = new List<StudentInformation_Result>();
            using (SqlConnection myCon = new SqlConnection("Data Source=DESKTOP-50L3KE8\\SQLEXPRESS;Initial Catalog=DB_Class;Integrated Security=True"))
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



        public void AddInstructorInfo(Instructor instructor)
        {
            using (SqlConnection myCon = new SqlConnection("Data Source=DESKTOP-BQRJ2ID;Initial Catalog=DB_Class;Integrated Security=True"))
            {
                SqlCommand cmd = new SqlCommand("Instructor_Info", myCon);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add("@first_name", SqlDbType.VarChar).Value = instructor.first_name;
                cmd.Parameters.Add("@last_name", SqlDbType.VarChar).Value = instructor.last_name;
                cmd.Parameters.Add("@title", SqlDbType.VarChar).Value = instructor.title;
                cmd.Parameters.Add("@birthdate", SqlDbType.Date).Value = instructor.birthdate;
                cmd.Parameters.Add("@contact", SqlDbType.VarChar).Value = instructor.contact;
                cmd.Parameters.Add("@contact_email", SqlDbType.VarChar).Value = instructor.contact_email;

                try
                {
                    myCon.Open();
                    cmd.ExecuteNonQuery();
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

        public void AddContact_Person_Info(Contact_Person contact_person)
        {
            using (SqlConnection myCon = new SqlConnection("Data Source=DESKTOP-BQRJ2ID;Initial Catalog=DB_Class;Integrated Security=True"))
            {
                SqlCommand cmd = new SqlCommand("Contact_Person_Info", myCon);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add("@first_name", SqlDbType.VarChar).Value = contact_person.first_name;
                cmd.Parameters.Add("@last_name", SqlDbType.VarChar).Value = contact_person.last_name;
                cmd.Parameters.Add("@title", SqlDbType.VarChar).Value = contact_person.title;
                cmd.Parameters.Add("@contact_number1", SqlDbType.VarChar).Value = contact_person.contact_number1;
                cmd.Parameters.Add("@contact_number2", SqlDbType.VarChar).Value = contact_person.contact_number2;
                cmd.Parameters.Add("@contact_email", SqlDbType.VarChar).Value = contact_person.contact_email;
                cmd.Parameters.Add("@RelationWith_Student", SqlDbType.VarChar).Value = contact_person.RelationWith_Student;

                try
                {
                    myCon.Open();
                    cmd.ExecuteNonQuery();
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


        public void updateStudentInfo(Models.StudentInformation_Result studentcontactperson)
        {
            using (SqlConnection myCon = new SqlConnection("Data Source=DESKTOP-50L3KE8\\SQLEXPRESS;Initial Catalog=DB_Class;Integrated Security=True"))
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
        public void DeleteStudentRecord(Models.StudentInformation_Result studentcontactperson)
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
            using (SqlConnection myCon = new SqlConnection("Data Source=DESKTOP-50L3KE8\\SQLEXPRESS;Initial Catalog=DB_Class;Integrated Security=True"))
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
    }    


    
}