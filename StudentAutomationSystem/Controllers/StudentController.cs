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
        public string CreateStudent(Student student)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            
            connection.Open();
            
            string sql = "INSERT INTO Students (StudentCode, StudentName,StudentPhone,StudentEmail,StudentAddress) " +
                "VALUES('"+student.StudentCode+"', '"+student.StudentName+"', '"+student.StudentPhone+"', '"+student.StudentEmail+"', '"+student.StudentAddress+"')";
            
            SqlCommand cmd = new SqlCommand(sql, connection); 

            if(cmd.ExecuteNonQuery() > 0)
            {
                connection.Close();
                return "Success";
            }
            connection.Close();
            return "Failed";
        }

        public ActionResult AllStudents()
        {
            return View();
        }

        public List<Student> GetStudents()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string sql = "SELECT * FROM Students";
            SqlCommand cmd = new SqlCommand(sql, connection);
        }
    }
}
