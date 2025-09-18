using _2301010045_NguyenNgocVy_Buoi1.Models.Domain;
using _2301010045_NguyenNgocVy_Buoi1.Models.DTO;

namespace _2301010045_NguyenNgocVy_Buoi1.Reponsitory
{
    public interface IBookRepository
    {
        List<BookDTO> GetAllBooks();
        BookDTO GetBookById(int id);
        Books AddBook(AddBookRequestDTO addBookRequestDTO); // 🔥 sửa lại Books
        Books? UpdateBookById(int id, AddBookRequestDTO bookDTO); // 🔥 sửa lại Books
        Books? DeleteBookById(int id);
    }
}
