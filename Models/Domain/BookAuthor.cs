namespace _2301010045_NguyenNgocVy_Buoi1.Models.Domain
{
    public class BookAuthor
    {
        public int BookID { get; set; }
        public Books Book { get; set; }

        public int AuthorID { get; set; }
        public Author Author { get; set; }
    }
}
