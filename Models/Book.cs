namespace LMS.API.Models
{
    public class Book
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public string Category { get; set; }
        public string Genre { get; set; }
        public string ShelfNumber { get; set; }
        public int Quantity { get; set; }
        public int Stock { get; set; }
    }
}