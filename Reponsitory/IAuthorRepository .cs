using _2301010045_NguyenNgocVy_Buoi1.Models.Domain;
using _2301010045_NguyenNgocVy_Buoi1.Models.DTO;

namespace _2301010045_NguyenNgocVy_Buoi1.Reponsitory
{
    public interface IAuthorRepository
    {
        // Lấy tất cả tác giả
        List<AuthorDTO> GetAllAuthors();

        // Lấy tác giả theo ID
        AuthorDTO? GetAuthorById(int id);

        // Thêm tác giả mới
        Author AddAuthor(AddAuthorRequestDTO addAuthorRequestDTO);

        // Cập nhật thông tin tác giả
        Author? UpdateAuthorById(int id, AuthorNoIdDTO authorNoIdDTO);

        // Xóa tác giả
        Author? DeleteAuthorById(int id);
    }
}
