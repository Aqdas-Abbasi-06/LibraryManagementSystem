using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using LMS.API.Models;
using LMS.API.DAL;

namespace LMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookIssuesController : ControllerBase
    {

        [HttpGet]
        public ActionResult<List<BookIssue>> GetBookIssues()
        {
            List<BookIssue> issues = new();

            using var conn = Database.GetConnection();

            string query = @"SELECT IssueId, BookId, StudentId,
                            IssueDate, ReturnDate, Status
                            FROM BookIssues";

            using var cmd = new SqliteCommand(query, conn);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                issues.Add(new BookIssue
                {
                    IssueId = reader.GetInt32(0),
                    BookId = reader.GetInt32(1),
                    StudentId = reader.GetInt32(2),

                    IssueDate = DateTime.Parse(reader.GetString(3)),

                    ReturnDate = reader.IsDBNull(4)
                        ? null
                        : DateTime.Parse(reader.GetString(4)),

                    Status = reader.GetString(5)
                });
            }

            return issues;
        }


        [HttpGet("{id}")]
        public ActionResult<BookIssue> GetBookIssue(int id)
        {
            using var conn = Database.GetConnection();
            using var cmd = conn.CreateCommand();

            cmd.CommandText = @"SELECT IssueId, BookId, StudentId,
                               IssueDate, ReturnDate, Status
                               FROM BookIssues
                               WHERE IssueId = $id";

            cmd.Parameters.AddWithValue("$id", id);

            using var reader = cmd.ExecuteReader();

            if (!reader.Read())
                return NotFound();

            return new BookIssue
            {
                IssueId = reader.GetInt32(0),
                BookId = reader.GetInt32(1),
                StudentId = reader.GetInt32(2),

                IssueDate = DateTime.Parse(reader.GetString(3)),

                ReturnDate = reader.IsDBNull(4)
                    ? null
                    : DateTime.Parse(reader.GetString(4)),

                Status = reader.GetString(5)
            };
        }


        [HttpPost]
        public ActionResult<int> PostBookIssue(BookIssue issue)
        {
            using var conn = Database.GetConnection();
            using var cmd = conn.CreateCommand();

            cmd.CommandText = @"INSERT INTO BookIssues
                (BookId, StudentId, IssueDate, ReturnDate, Status)
                VALUES ($bookid, $studentid, $issuedate, $returndate, $status)";

            cmd.Parameters.AddWithValue("$bookid", issue.BookId);
            cmd.Parameters.AddWithValue("$studentid", issue.StudentId);

            cmd.Parameters.AddWithValue("$issuedate",
                issue.IssueDate.ToString("yyyy-MM-dd"));

            cmd.Parameters.AddWithValue("$returndate",
                issue.ReturnDate?.ToString("yyyy-MM-dd"));

            cmd.Parameters.AddWithValue("$status", issue.Status);

            int result = cmd.ExecuteNonQuery();

            return result;
        }


        [HttpPut("{id}")]
        public IActionResult PutBookIssue(int id, BookIssue issue)
        {
            if (id != issue.IssueId)
                return BadRequest();

            using var conn = Database.GetConnection();
            using var cmd = conn.CreateCommand();

            cmd.CommandText = @"UPDATE BookIssues SET
                BookId = $bookid,
                StudentId = $studentid,
                IssueDate = $issuedate,
                ReturnDate = $returndate,
                Status = $status
                WHERE IssueId = $id";

            cmd.Parameters.AddWithValue("$id", id);

            cmd.Parameters.AddWithValue("$bookid", issue.BookId);
            cmd.Parameters.AddWithValue("$studentid", issue.StudentId);

            cmd.Parameters.AddWithValue("$issuedate",
                issue.IssueDate.ToString("yyyy-MM-dd"));

            cmd.Parameters.AddWithValue("$returndate",
                issue.ReturnDate?.ToString("yyyy-MM-dd"));

            cmd.Parameters.AddWithValue("$status", issue.Status);

            int rows = cmd.ExecuteNonQuery();

            if (rows == 0)
                return NotFound();

            return NoContent();
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteBookIssue(int id)
        {
            using var conn = Database.GetConnection();
            using var cmd = conn.CreateCommand();

            cmd.CommandText = "DELETE FROM BookIssues WHERE IssueId = $id";

            cmd.Parameters.AddWithValue("$id", id);

            int rows = cmd.ExecuteNonQuery();

            if (rows == 0)
                return NotFound();

            return NoContent();
        }
    }
}