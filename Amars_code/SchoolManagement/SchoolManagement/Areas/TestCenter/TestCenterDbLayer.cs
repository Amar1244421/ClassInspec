using SchoolManagement.Areas.TestCenter.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace SchoolManagement.Areas.TestCenter
{
    public class TestCenterDbLayer
    {
        public DataSet GetTestInfo(SchoolManagement.Areas.TestCenter.Models.TestCenter std)
        {
            List<SchoolManagement.Areas.TestCenter.Models.TestCenter> students = new List<SchoolManagement.Areas.TestCenter.Models.TestCenter>();
            using (SqlConnection myCon = new SqlConnection(WebConfigurationManager.ConnectionStrings["SqlDBClass"].ConnectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("GetTestDetails", myCon);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.Add("@ClassId", SqlDbType.Int).Value = Int32.Parse(std.ClassID.ToString());


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



        public void AddTestInfo(SchoolManagement.Areas.TestCenter.Models.TestCenter studentcontactperson)
        {
            using (SqlConnection myCon = new SqlConnection(WebConfigurationManager.ConnectionStrings["SqlDBClass"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("AddTestDetails", myCon);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

               
                cmd.Parameters.Add("@ClassID", SqlDbType.Int).Value = studentcontactperson.ClassID;
                cmd.Parameters.Add("@Description", SqlDbType.VarChar).Value = studentcontactperson.Description;
                cmd.Parameters.Add("@TestName", SqlDbType.VarChar).Value = studentcontactperson.TestName;
                cmd.Parameters.Add("@outOFF", SqlDbType.Int).Value = studentcontactperson.outOFF;

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
    }
}