using _2301010045_NguyenNgocVy_Buoi1.Data;
using _2301010045_NguyenNgocVy_Buoi1.Models.Domain;
using _2301010045_NguyenNgocVy_Buoi1.Models.DTO;

namespace _2301010045_NguyenNgocVy_Buoi1.Reponsitory
{
    public class SQLAuthorRepository : IAuthorRepository
    {
        private readonly AppDbContext _context;

        public SQLAuthorRepository(AppDbContext context)
        {
            _context = context;
        }

        // Lấy tất cả tác giả
        public List<AuthorDTO> GetAllAuthors()
        {
            return _context.Authors.Select(a => new AuthorDTO
            {
                Id = a.Id,
                FullName = a.FullName
            }).ToList();
        }

        // Lấy tác giả theo Id
        public AuthorDTO? GetAuthorById(int id)
        {
            var author = _context.Authors.FirstOrDefault(a => a.Id == id);
            if (author == null) return null;

            return new AuthorDTO
            {
                Id = author.Id,
                FullName = author.FullName
            };
        }

        // Thêm tác giả mới
        public Author AddAuthor(AddAuthorRequestDTO addAuthorRequestDTO)
        {
            var author = new Author
            {
                FullName = addAuthorRequestDTO.FullName
            };

            _context.Authors.Add(author);
            _context.SaveChanges();
            return author;
        }

        // Cập nhật tác giả theo Id
        public Author? UpdateAuthorById(int id, AuthorNoIdDTO authorNoIdDTO)
        {
            var existingAuthor = _context.Authors.FirstOrDefault(a => a.Id == id);
            if (existingAuthor == null) return null;

            existingAuthor.FullName = authorNoIdDTO.FullName;

            _context.SaveChanges();
            return existingAuthor;
        }

        // Xóa tác giả theo Id
        public Author? DeleteAuthorById(int id)
        {
            var author = _context.Authors.FirstOrDefault(a => a.Id == id);
            if (author == null) return null;

            _context.Authors.Remove(author);
            _context.SaveChanges();
            return author;
        }
    }
}
