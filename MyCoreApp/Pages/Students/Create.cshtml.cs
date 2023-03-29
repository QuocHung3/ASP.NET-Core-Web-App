using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace MyCoreApp.Pages.Students
{
    public class CreateModel : PageModel
    {
        public Student studentInfo = new Student();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
        }

        public void OnPost() {
            studentInfo.id = Request.Form["id"];
            studentInfo.fname = Request.Form["fname"];
            studentInfo.lname = Request.Form["lname"];
            studentInfo.enrollmentDate = Request.Form["edate"];

            if(studentInfo.id.Length==0 || studentInfo.fname.Length == 0 || studentInfo.lname.Length == 0 || 
                    studentInfo.enrollmentDate.Length == 0)
            {
                errorMessage = "All the field are required";
                return;
            }

            //save new student into database
            try
            {
                String connectString = "Data Source=DESKTOP-KMNS09Q;Initial Catalog=QLSV;User ID=sa;Password=12345";
                SqlConnection connection = new SqlConnection(connectString);
                connection.Open();

                String sql = "insert into Student "
                    + "values(@id,@fname,@lname,@enrollmentDate);";
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@id", studentInfo.id);
                command.Parameters.AddWithValue("@fname", studentInfo.fname);
                command.Parameters.AddWithValue("@lname", studentInfo.lname);
                command.Parameters.AddWithValue("@enrollmentDate", studentInfo.enrollmentDate);

                command.ExecuteNonQuery();
                Response.Redirect("/Students/Index");
            } catch (Exception ex) {
                errorMessage = ex.Message;
                return;
            }
            studentInfo.id = "";
            studentInfo.fname = "";
            studentInfo.lname = "";
            studentInfo.enrollmentDate = "";
            successMessage = "New student added correctly";
        }
    }
}
