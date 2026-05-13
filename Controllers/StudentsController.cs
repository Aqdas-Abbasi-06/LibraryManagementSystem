using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using LMS.API.Models;
using LMS.API.DAL;

namespace LMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
       
        [HttpGet]
        public ActionResult<List<Student>> GetStudents()
        {
            List<Student> students = new();
            using var conn = Database.GetConnection();
            string query = "SELECT StudentId, Name, RegNumber, Semester, Department, Email, PhoneNum FROM Students";
            using var cmd = new SqliteCommand(query, conn);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                students.Add(new Student
                {
                    StudentId = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    RegNumber = reader.GetString(2),
                    Semester = reader.GetInt32(3),
                    Department = reader.GetString(4),
                    Email = reader.GetString(5),
                    PhoneNum = reader.GetString(6)
                });
            }
            return students;
        }

     
        [HttpGet("{id}")]
        public ActionResult<Student> GetStudent(int id)
        {
            using var conn = Database.GetConnection();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT StudentId, Name, RegNumber, Semester, Department, Email, PhoneNum FROM Students WHERE StudentId = $id";
            cmd.Parameters.AddWithValue("$id", id);
            using var reader = cmd.ExecuteReader();
            if (!reader.Read())
                return NotFound();
            return new Student
            {
                StudentId = reader.GetInt32(0),
                Name = reader.GetString(1),
                RegNumber = reader.GetString(2),
                Semester = reader.GetInt32(3),
                Department = reader.GetString(4),
                Email = reader.GetString(5),
                PhoneNum = reader.GetString(6)
            };
        }

        
        [HttpPost]
        public ActionResult<int> PostStudent(Student student)
        {
            using var conn = Database.GetConnection();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = @"INSERT INTO Students 
                (Name, RegNumber, Semester, Department, Email, PhoneNum) 
                VALUES ($name, $regnum, $semester, $dept, $email, $phone)";
            cmd.Parameters.AddWithValue("$name", student.Name);
            cmd.Parameters.AddWithValue("$regnum", student.RegNumber);
            cmd.Parameters.AddWithValue("$semester", student.Semester);
            cmd.Parameters.AddWithValue("$dept", student.Department);
            cmd.Parameters.AddWithValue("$email", student.Email);
            cmd.Parameters.AddWithValue("$phone", student.PhoneNum);
            int result = cmd.ExecuteNonQuery();
            return result;
        }

        
        [HttpPut("{id}")]
        public IActionResult PutStudent(int id, Student student)
        {
            if (id != student.StudentId)
                return BadRequest();
            using var conn = Database.GetConnection();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = @"UPDATE Students SET 
                Name=$name, RegNumber=$regnum, Semester=$semester,
                Department=$dept, Email=$email, PhoneNum=$phone
                WHERE StudentId=$id";
            cmd.Parameters.AddWithValue("$id", student.StudentId);
            cmd.Parameters.AddWithValue("$name", student.Name);
            cmd.Parameters.AddWithValue("$regnum", student.RegNumber);
            cmd.Parameters.AddWithValue("$semester", student.Semester);
            cmd.Parameters.AddWithValue("$dept", student.Department);
            cmd.Parameters.AddWithValue("$email", student.Email);
            cmd.Parameters.AddWithValue("$phone", student.PhoneNum);
            int rows = cmd.ExecuteNonQuery();
            if (rows == 0)
                return NotFound();
            return NoContent();
        }

       
        [HttpDelete("{id}")]
        public IActionResult DeleteStudent(int id)
        {
            using var conn = Database.GetConnection();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "DELETE FROM Students WHERE StudentId = $id";
            cmd.Parameters.AddWithValue("$id", id);
            int rows = cmd.ExecuteNonQuery();
            if (rows == 0)
                return NotFound();
            return NoContent();
        }
    }
}