using _2301010045_NguyenNgocVy_Buoi1.Models.Domain;
using _2301010045_NguyenNgocVy_Buoi1.Models.DTO;

namespace _2301010045_NguyenNgocVy_Buoi1.Reponsitory
{
    public interface IBookRepository
    {
        // Lấy danh sách Books có filter, sort, phân trang
        List<BookDTO> GetAllBooks(
            string? filterOn = null,
            string? filterQuery = null,
            string? sortBy = null,
            bool isAscending = true,
            int pageNumber = 1,
            int pageSize = 1000
        );

        // Lấy Book theo Id
        BookDTO? GetBookById(int id);

        // Thêm Book
        Books AddBook(AddBookRequestDTO addBookRequestDTO);

        // Cập nhật Book
        Books? UpdateBookById(int id, AddBookRequestDTO bookDTO);

        // Xóa Book
        Books? DeleteBookById(int id);
    }
}
