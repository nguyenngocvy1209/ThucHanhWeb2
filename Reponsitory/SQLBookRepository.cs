using _2301010045_NguyenNgocVy_Buoi1.Data;
using _2301010045_NguyenNgocVy_Buoi1.Models.Domain;
using _2301010045_NguyenNgocVy_Buoi1.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace _2301010045_NguyenNgocVy_Buoi1.Reponsitory
{
    public class SQLBookRepository : IBookRepository
    {
        private readonly AppDbContext _context;

        public SQLBookRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<BookDTO> GetAllBooks()
        {
            return _context.Books
                .Include(b => b.Publisher)
                .Include(b => b.BookAuthors)  // ✅ tên mới đồng bộ
                    .ThenInclude(ba => ba.Author)
                .Select(b => new BookDTO
                {
                    Id = b.Id,
                    Title = b.Title,
                    Description = b.Description,
                    IsRead = b.IsRead,
                    DateRead = b.DateRead,
                    Rate = b.Rate,
                    Genre = b.Genre,
                    CoverUrl = b.CoverUrl,
                    DateAdded = b.DateAdded,
                    PublisherName = b.Publisher.Name,
                    AuthorNames = b.BookAuthors.Select(x => x.Author.FullName).ToList()
                })
                .ToList();
        }


        public BookDTO? GetBookById(int id)
        {
            var book = _context.Books
                .Include(b => b.Publisher)
                .Include(b => b.BookAuthors)
                    .ThenInclude(ba => ba.Author)
                .FirstOrDefault(b => b.Id == id);

            if (book == null) return null;

            return new BookDTO
            {
                Id = book.Id,
                Title = book.Title,
                Description = book.Description,
                IsRead = book.IsRead,
                DateRead = book.DateRead,
                Rate = book.Rate,
                Genre = book.Genre,
                CoverUrl = book.CoverUrl,
                DateAdded = book.DateAdded,
                PublisherName = book.Publisher.Name,
                AuthorNames = book.BookAuthors.Select(x => x.Author.FullName).ToList()
            };
        }

        public Books AddBook(AddBookRequestDTO addBookRequestDTO)
        {
            var book = new Books
            {
                Title = addBookRequestDTO.Title,
                Description = addBookRequestDTO.Description,
                IsRead = addBookRequestDTO.IsRead,
                DateRead = addBookRequestDTO.IsRead ? addBookRequestDTO.DateRead : null,
                Rate = addBookRequestDTO.IsRead ? addBookRequestDTO.Rate : null,
                Genre = addBookRequestDTO.Genre,
                CoverUrl = addBookRequestDTO.CoverUrl,
                DateAdded = addBookRequestDTO.DateAdded,
                PublisherID = addBookRequestDTO.PublisherID
            };

            _context.Books.Add(book);
            _context.SaveChanges();

            // thêm quan hệ Book - Author
            if (addBookRequestDTO.AuthorIds != null && addBookRequestDTO.AuthorIds.Any())
            {
                foreach (var authorId in addBookRequestDTO.AuthorIds)
                {
                    var bookAuthor = new Book_Author
                    {
                        BookId = book.Id,
                        AuthorId = authorId
                    };
                    _context.BookAuthors.Add(bookAuthor);
                }
                _context.SaveChanges();
            }

            return book;
        }

        public Books? UpdateBookById(int id, AddBookRequestDTO bookDTO)
        {
            var existingBook = _context.Books.FirstOrDefault(b => b.Id == id);
            if (existingBook == null) return null;

            existingBook.Title = bookDTO.Title;
            existingBook.Description = bookDTO.Description;
            existingBook.IsRead = bookDTO.IsRead;
            existingBook.DateRead = bookDTO.IsRead ? bookDTO.DateRead : null;
            existingBook.Rate = bookDTO.IsRead ? bookDTO.Rate : null;
            existingBook.Genre = bookDTO.Genre;
            existingBook.CoverUrl = bookDTO.CoverUrl;
            existingBook.DateAdded = bookDTO.DateAdded;
            existingBook.PublisherID = bookDTO.PublisherID;

            _context.SaveChanges();

            // cập nhật lại quan hệ Book - Author
            var oldAuthors = _context.BookAuthors.Where(ba => ba.BookId == id).ToList();
            if (oldAuthors.Any())
            {
                _context.BookAuthors.RemoveRange(oldAuthors);
                _context.SaveChanges();
            }

            if (bookDTO.AuthorIds != null && bookDTO.AuthorIds.Any())
            {
                foreach (var authorId in bookDTO.AuthorIds)
                {
                    var bookAuthor = new Book_Author
                    {
                        BookId = id,
                        AuthorId = authorId
                    };
                    _context.BookAuthors.Add(bookAuthor);
                }
                _context.SaveChanges();
            }

            return existingBook;
        }

        public Books? DeleteBookById(int id)
        {
            var book = _context.Books.FirstOrDefault(b => b.Id == id);
            if (book == null) return null;

            var authors = _context.BookAuthors.Where(ba => ba.BookId == id).ToList();
            if (authors.Any())
            {
                _context.BookAuthors.RemoveRange(authors);
                _context.SaveChanges();
            }

            _context.Books.Remove(book);
            _context.SaveChanges();
            return book;
        }
    }
}
