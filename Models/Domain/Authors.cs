using System.Collections.Generic;

namespace _2301010045_NguyenNgocVy_Buoi1.Models.Domain
{
    public class Author
    {
        public int Id { get; set; }
        public string FullName { get; set; }

        // Quan hệ nhiều-nhiều: 1 Author có thể viết nhiều Books
        public ICollection<Book_Author> BookAuthors { get; set; } = new List<Book_Author>();
    }
}
