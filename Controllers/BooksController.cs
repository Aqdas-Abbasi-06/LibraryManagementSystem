using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using LMS.API.Models;
using LMS.API.DAL;

namespace LMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        
        [HttpGet]
        public ActionResult<List<Book>> GetBooks()
        {
            List<Book> books = new();
            using var conn = Database.GetConnection();
            string query = "SELECT BookId, Title, Author, ISBN, Category, Genre, ShelfNumber, Quantity FROM Books";
            using var cmd = new SqliteCommand(query, conn);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                books.Add(new Book
                {
                    BookId = reader.GetInt32(0),
                    Title = reader.GetString(1),
                    Author = reader.GetString(2),
                    ISBN = reader.GetString(3),
                    Category = reader.GetString(4),
                    Genre = reader.GetString(5),
                    ShelfNumber = reader.GetString(6),
                    Quantity = reader.GetInt32(7)
                });
            }
            return books;
        }

        
        [HttpGet("{id}")]
        public ActionResult<Book> GetBook(int id)
        {
            using var conn = Database.GetConnection();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT BookId, Title, Author, ISBN, Category, Genre, ShelfNumber, Quantity FROM Books WHERE BookId = $id";
            cmd.Parameters.AddWithValue("$id", id);
            using var reader = cmd.ExecuteReader();
            if (!reader.Read())
                return NotFound();
            return new Book
            {
                BookId = reader.GetInt32(0),
                Title = reader.GetString(1),
                Author = reader.GetString(2),
                ISBN = reader.GetString(3),
                Category = reader.GetString(4),
                Genre = reader.GetString(5),
                ShelfNumber = reader.GetString(6),
                Quantity = reader.GetInt32(7)
            };
        }

        
        [HttpPost]
        public ActionResult<int> PostBook(Book book)
        {
            using var conn = Database.GetConnection();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = @"INSERT INTO Books 
                (Title, Author, ISBN, Category, Genre, ShelfNumber, Quantity) 
                VALUES ($title, $author, $isbn, $category, $genre, $shelf, $qty)";
            cmd.Parameters.AddWithValue("$title", book.Title);
            cmd.Parameters.AddWithValue("$author", book.Author);
            cmd.Parameters.AddWithValue("$isbn", book.ISBN);
            cmd.Parameters.AddWithValue("$category", book.Category);
            cmd.Parameters.AddWithValue("$genre", book.Genre);
            cmd.Parameters.AddWithValue("$shelf", book.ShelfNumber);
            cmd.Parameters.AddWithValue("$qty", book.Quantity);
            int result = cmd.ExecuteNonQuery();
            return result;
        }

        // PUT: api/Books/5 - Update a book
        [HttpPut("{id}")]
        public IActionResult PutBook(int id, Book book)
        {
            if (id != book.BookId)
                return BadRequest();
            using var conn = Database.GetConnection();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = @"UPDATE Books SET 
                Title=$title, Author=$author, ISBN=$isbn,
                Category=$category, Genre=$genre,
                ShelfNumber=$shelf, Quantity=$qty
                WHERE BookId=$id";
            cmd.Parameters.AddWithValue("$id", book.BookId);
            cmd.Parameters.AddWithValue("$title", book.Title);
            cmd.Parameters.AddWithValue("$author", book.Author);
            cmd.Parameters.AddWithValue("$isbn", book.ISBN);
            cmd.Parameters.AddWithValue("$category", book.Category);
            cmd.Parameters.AddWithValue("$genre", book.Genre);
            cmd.Parameters.AddWithValue("$shelf", book.ShelfNumber);
            cmd.Parameters.AddWithValue("$qty", book.Quantity);
            int rows = cmd.ExecuteNonQuery();
            if (rows == 0)
                return NotFound();
            return NoContent();
        }

        // DELETE: api/Books/5 - Delete a book
        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            using var conn = Database.GetConnection();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "DELETE FROM Books WHERE BookId = $id";
            cmd.Parameters.AddWithValue("$id", id);
            int rows = cmd.ExecuteNonQuery();
            if (rows == 0)
                return NotFound();
            return NoContent();
        }
    }
}