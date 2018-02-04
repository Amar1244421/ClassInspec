using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace SchoolManagement.Areas.AttendanceSection
{
    public class Attendancedblayer
    {
        public void ImportAttendance(DataSet ds)
        {
            for (int i = 8; i < ds.Tables[0].Rows.Count; i++)
            {
                using (SqlConnection myCon = new SqlConnection(WebConfigurationManager.ConnectionStrings["SqlDBClass"].ConnectionString))
                //SqlConnection myCon = new SqlConnection("Data Source=DESKTOP-BQRJ2ID;Initial Catalog=DB_Class;Integrated Security=True"))
                {
                    string query = "Insert into Student_presense(StudentID,Class_ScheduleID,present) Values('" + ds.Tables[0].Rows[i][0] + "','" + 3 + "','" + ds.Tables[0].Rows[i][2] + "')";

                    myCon.Open();
                    SqlCommand cmd = new SqlCommand(query, myCon);
                    cmd.ExecuteNonQuery();
                    myCon.Close();
                }
            }
        }
        public DataSet Student_Teacherdata(string a)
        {
            using (SqlConnection myCon = new SqlConnection(WebConfigurationManager.ConnectionStrings["SqlDBClass"].ConnectionString))
            {
                int class_ScheduledID = Int32.Parse(a);
                SqlCommand cmd = new SqlCommand("StudentInformation", myCon);
                cmd.Parameters.Add("@Class_ScheduledID", SqlDbType.Int).Value = class_ScheduledID;
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

    }
}