namespace _2301010045_NguyenNgocVy_Buoi1.Models.Domain
{
    public class Books
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public string Description { get; set; }
        public bool IsRead { get; set; }
        public DateTime? DateRead { get; set; }
        public int? Rate { get; set; }
        public string Genre { get; set; }
        public string CoverUrl { get; set; }
        public DateTime DateAdded { get; set; }

        public int PublisherID { get; set; }
        public Publisher Publisher { get; set; }

        // ✅ Navigation property
        public ICollection<Book_Author> BookAuthors { get; set; }
    }
}
