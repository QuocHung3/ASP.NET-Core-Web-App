using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.VisualBasic;
using System;
using System.Data.SqlClient;

namespace MyCoreApp.Pages.Students
{
    public class IndexModel : PageModel
    {
        public List<Student> listStudents = new List<Student>();

        public void OnGet()
        {
                String conn = "Data Source=DESKTOP-KMNS09Q;Initial Catalog=QLSV;User ID=sa;Password=12345";
                SqlConnection connection = new SqlConnection(conn);
                connection.Open();

                String sql = "SELECT * FROM Student";
                SqlCommand command = new SqlCommand(sql, connection);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Student stu = new Student();
                    stu.id =  reader.GetString(0);
                    stu.fname = reader.GetString(1);
                    stu.lname = reader.GetString(2);
                    stu.enrollmentDate = reader.GetDateTime(3).ToString();

                    listStudents.Add(stu);
                }
        }

    }

    public class Student {
        public String id;
        public String fname;
        public String lname;
        public String enrollmentDate;
    }
}
