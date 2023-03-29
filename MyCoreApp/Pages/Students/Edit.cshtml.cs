using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace MyCoreApp.Pages.Students
{
    public class EditModel : PageModel
    {
        public Student studentInfo = new Student();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
            String id = Request.Query["id"];

            try
            {
                String connectString = "Data Source=DESKTOP-KMNS09Q;Initial Catalog=QLSV;User ID=sa;Password=12345";
                SqlConnection connection = new SqlConnection(connectString);
                connection.Open();

                String sql = "select * from Student where StudentID = @id";
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@id", id);
                
                SqlDataReader reader = command.ExecuteReader(); 
                if(reader.Read())
                {
                    studentInfo.id = "" + reader.GetString(0);
                    studentInfo.fname = "" + reader.GetString(1);
                    studentInfo.lname = "" + reader.GetString(2);
                    studentInfo.enrollmentDate = reader.GetDateTime(3).ToString();
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }
        }

        public void OnPost() {
            String id = Request.Query["id"];

            studentInfo.id = Request.Form["id"];
            studentInfo.fname = Request.Form["fname"];
            studentInfo.lname = Request.Form["lname"];
            studentInfo.enrollmentDate = Request.Form["edate"];

            if (studentInfo.id.Length == 0 || studentInfo.fname.Length == 0 ||
                studentInfo.lname.Length == 0 || studentInfo.enrollmentDate.Length == 0)
            {
                errorMessage = "All the fields are required";
                return;
            }


            try
            {
                String connectString = "Data Source=DESKTOP-KMNS09Q;Initial Catalog=QLSV;User ID=sa;Password=12345";
                SqlConnection connection = new SqlConnection(connectString);
                connection.Open();

                String sql = "update Student " +
                    "set Fname = @fname ,Lname = @lname ,EnrollmentDate = @enrollmentDate " +
                    "where StudentID = @id";

                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@fname", studentInfo.fname);
                command.Parameters.AddWithValue("@lname", studentInfo.lname);
                command.Parameters.AddWithValue("@enrollmentDate", studentInfo.enrollmentDate);
                command.Parameters.AddWithValue("@id", id);

                command.ExecuteNonQuery();
                
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            Response.Redirect("/Students/Index");
        }    
    }
}
