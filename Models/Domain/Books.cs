using System;
using System.Collections.Generic;

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

        // Quan hệ 1 - nhiều: Publisher có nhiều Books
        public int PublisherID { get; set; }
        public Publisher Publisher { get; set; }

        // Quan hệ nhiều - nhiều: Book có nhiều Author
        public ICollection<Book_Author> BookAuthors { get; set; } = new List<Book_Author>();
        // 👆 Khởi tạo List mặc định để tránh null reference
    }
}
