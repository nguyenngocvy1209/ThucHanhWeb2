namespace _2301010045_NguyenNgocVy_Buoi1.Models.Domain
{
    public class Author
    {
        public int Id { get; set; }
        public string FullName { get; set; }

        // ✅ Navigation property
        public ICollection<Book_Author> BookAuthors { get; set; }
    }
}
