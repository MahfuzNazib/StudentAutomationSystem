using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using StudentAutomationSystem.Models;

namespace StudentAutomationSystem.Controllers
{
    public class StudentController : Controller
    {
        private readonly IConfiguration _configuration;
        string connectionString;

        public StudentController(IConfiguration configuration)
        {
            _configuration = configuration;
            connectionString = _configuration.GetConnectionString("DefaultConnection");
        }


        public IActionResult Index()
        {
            return View();
        }

        // Create New Student
        public ActionResult CreateStudent(Student student)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            
            connection.Open();
            
            string sql = "INSERT INTO Students (StudentCode, StudentName,StudentPhone,StudentEmail,StudentAddress) " +
                "VALUES('"+student.StudentCode+"', '"+student.StudentName+"', '"+student.StudentPhone+"', '"+student.StudentEmail+"', '"+student.StudentAddress+"')";
            
            SqlCommand cmd = new SqlCommand(sql, connection); 

            if(cmd.ExecuteNonQuery() > 0)
            {
                connection.Close();
                return RedirectToAction("AllStudents");
            }
            return View();
        }

        public ActionResult AllStudents()
        {
            List<Student> students = GetStudents();
            return View(students);
        }

        public ActionResult AddNewStudent()
        {
            
            return View();
        }

        public List<Student> GetStudents()
        {
            List<Student> students = new List<Student>();
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string sql = "SELECT * FROM Students";
            using (SqlCommand cmd = new SqlCommand(sql, connection))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        Student student = new Student();
                        student.StudentCode = reader["StudentCode"].ToString();
                        student.StudentName = reader["StudentName"].ToString();
                        student.StudentEmail = reader["StudentEmail"].ToString();
                        student.StudentPhone = reader["StudentPhone"].ToString();
                        student.StudentAddress = reader["StudentAddress"].ToString();

                        students.Add(student);  
                    }
                }
            }

            return students;
        }
    }
}
