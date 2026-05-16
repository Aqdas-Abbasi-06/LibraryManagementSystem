namespace LMS.API.Models
{
    public class BookIssue
    {
        public int IssueId { get; set; }

        public int BookId { get; set; }

        public int StudentId { get; set; }

        public DateTime IssueDate { get; set; }

        public DateTime? ReturnDate { get; set; }

        public string? Status { get; set; }
    }
}