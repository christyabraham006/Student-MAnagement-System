using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Student_Management_System.Models
{
    public class StudentDBHandler
    {
        private SqlConnection con;
        private void connection()
        {
            con = new SqlConnection("Data Source=.;Initial Catalog=Student;Integrated Security=True");
        }
        private void connectionManage()
        {
            if (con.State == ConnectionState.Open)
                con.Close();
            else
                con.Open();
        }




        // **************** ADD NEW STUDENT *********************
        public bool AddNewStudent(StudentModel student)
        {
            student.AdmissionNo = $"SA000{ GetId()}";
            connection();
            SqlCommand cmd = new SqlCommand("AddStudent", con);
            cmd.CommandType = CommandType.StoredProcedure;

           

            cmd.Parameters.AddWithValue("@name", student.Name);
            cmd.Parameters.AddWithValue("@AdmissionNo", student.AdmissionNo);
            cmd.Parameters.AddWithValue("@DOB", student.DOB);
            cmd.Parameters.AddWithValue("@Address", student.Address);
            cmd.Parameters.AddWithValue("@FatherName", student.FatherName);
            cmd.Parameters.AddWithValue("@Contact", student.Contact);
            cmd.Parameters.AddWithValue("@Standard", student.Standard);
            connectionManage();
            int i = cmd.ExecuteNonQuery();
            connectionManage();



            if (i >= 1)
                return true;
            else
                return false;
        }

        // **************** Get Students *********************

        public List<StudentModel> GetStudents()
        {
            connection();
            List<StudentModel> students = new List<StudentModel>();

            SqlCommand cmd = new SqlCommand("GetStudents", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sd = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            connectionManage();
            sd.Fill(dt);
            connectionManage();

            foreach (DataRow dr in dt.Rows)
            {
                students.Add(
                    new StudentModel
                    {
                        
                        ID = Convert.ToInt32(dr["Id"]),
                        Name = Convert.ToString(dr["Name"]),
                        AdmissionNo = Convert.ToString(dr["AdmissionNo"]),
                        DOB = Convert.ToDateTime(dr["DOB"]),
                        Address = Convert.ToString(dr["Address"]),
                        FatherName = Convert.ToString(dr["FatherName"]),
                        Contact = Convert.ToString(dr["Contact"]),
                        Standard = Convert.ToInt16(dr["Standard"])
                    });
            }

            dt.Dispose();
            return students;

        }

         //**************** Delete Students *********************

        public bool DeleteStudent(int id)
        {
            connection();
            SqlCommand cmd = new SqlCommand("DeleteStudent", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Id", id);

            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i >= 1)
                return true;
            else
                return false;
        }

        private int GetId()
        {
            int tempInt = 0;
            connection();
            SqlCommand cmd = new SqlCommand("GetId", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Id", SqlDbType.Int).Direction = ParameterDirection.Output;
            connectionManage();
            int i = cmd.ExecuteNonQuery();
            connectionManage();
            tempInt = Convert.ToInt32(cmd.Parameters["@Id"].Value);
            return tempInt;



        }

        //**************** Update Students *********************
        public StudentModel GetById(int studentId)
        {
            connection();
            StudentModel student = new StudentModel();



            SqlCommand cmd = new SqlCommand("GetbyId", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sd = new SqlDataAdapter(cmd);
            cmd.Parameters.AddWithValue("@Id", studentId);
            DataTable dt = new DataTable();
            connectionManage();
            sd.Fill(dt);
            connectionManage();
            if (dt.Rows.Count != 0)
            {
                student.ID = Convert.ToInt32(dt.Rows[0]["ID"]);
                student.Name = Convert.ToString(dt.Rows[0]["Name"]);
                student.AdmissionNo = Convert.ToString(dt.Rows[0]["AdmissionNo"]);
                student.DOB = Convert.ToDateTime(dt.Rows[0]["DOB"]);
                student.Address = Convert.ToString(dt.Rows[0]["Address"]);
                student.FatherName = Convert.ToString(dt.Rows[0]["FatherName"]);
                student.Contact = Convert.ToString(dt.Rows[0]["Contact"]);
                student.Standard = Convert.ToInt16(dt.Rows[0]["Standard"]);

                dt.Dispose();
                return student;
            }
            dt.Dispose();
            return null;



        }
        public bool UpdateDetails(StudentModel student)
        {
            connection();
            SqlCommand cmd = new SqlCommand("UpdateStudent", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@StdId", student.ID);
            cmd.Parameters.AddWithValue("@name", student.Name);
            cmd.Parameters.AddWithValue("@AdmissionNo", student.AdmissionNo);
            cmd.Parameters.AddWithValue("@DOB", student.DOB);
            cmd.Parameters.AddWithValue("@Address", student.Address);
            cmd.Parameters.AddWithValue("@FatherName", student.FatherName);
            cmd.Parameters.AddWithValue("@Contact", student.Contact);
            cmd.Parameters.AddWithValue("@Standard", student.Standard);

            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i >= 1)
                return true;
            else
                return false;
        }
    }
}