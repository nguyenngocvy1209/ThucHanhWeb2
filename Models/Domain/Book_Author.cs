using _2301010045_NguyenNgocVy_Buoi1.Models.Domain;

namespace _2301010045_NguyenNgocVy_Buoi1.Models.Domain
{
    public class Book_Author
    {
        public int BookId { get; set; }
        public Books Book { get; set; }

        public int AuthorId { get; set; }
        public Author Author { get; set; }
    }

}