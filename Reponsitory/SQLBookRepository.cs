using _2301010045_NguyenNgocVy_Buoi1.Data;
using _2301010045_NguyenNgocVy_Buoi1.Models.Domain;
using _2301010045_NguyenNgocVy_Buoi1.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace _2301010045_NguyenNgocVy_Buoi1.Reponsitory
{
    public class SQLBookRepository : IBookRepository
    {
        private readonly AppDbContext _dbContext;

        public SQLBookRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // ================================
        // Get All Books
        // ================================
        public List<BookDTO> GetAllBooks(
            string? filterOn = null,
            string? filterQuery = null,
            string? sortBy = null,
            bool isAscending = true,
            int pageNumber = 1,
            int pageSize = 1000)
        {
            var allBooks = _dbContext.Books
                .Include(b => b.Publisher)
                .Include(b => b.BookAuthors).ThenInclude(ba => ba.Author)
                .Select(b => new BookDTO
                {
                    Id = b.Id,
                    Title = b.Title,
                    Description = b.Description,
                    IsRead = b.IsRead,
                    DateRead = b.IsRead ? b.DateRead : null,
                    Rate = b.IsRead ? b.Rate : null,
                    Genre = b.Genre,
                    CoverUrl = b.CoverUrl,
                    PublisherName = b.Publisher.Name,
                    AuthorNames = b.BookAuthors.Select(a => a.Author.FullName).ToList()
                })
                .AsQueryable();

            // Filtering
            if (!string.IsNullOrWhiteSpace(filterOn) && !string.IsNullOrWhiteSpace(filterQuery))
            {
                if (filterOn.Equals("title", StringComparison.OrdinalIgnoreCase))
                {
                    allBooks = allBooks.Where(x => x.Title.Contains(filterQuery));
                }
            }

            // Sorting
            if (!string.IsNullOrWhiteSpace(sortBy))
            {
                if (sortBy.Equals("title", StringComparison.OrdinalIgnoreCase))
                {
                    allBooks = isAscending
                        ? allBooks.OrderBy(x => x.Title)
                        : allBooks.OrderByDescending(x => x.Title);
                }
            }

            // Pagination
            var skipResults = (pageNumber - 1) * pageSize;
            return allBooks.Skip(skipResults).Take(pageSize).ToList();
        }

        // ================================
        // Get Book by Id
        // ================================
        public BookDTO? GetBookById(int id)
        {
            var book = _dbContext.Books
                .Include(b => b.Publisher)
                .Include(b => b.BookAuthors).ThenInclude(ba => ba.Author)
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
                PublisherName = book.Publisher.Name,
                AuthorNames = book.BookAuthors.Select(a => a.Author.FullName).ToList()
            };
        }

        // ================================
        // Add Book
        // ================================
        public Books AddBook(AddBookRequestDTO addBookRequestDTO)
        {
            var bookDomainModel = new Books
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

            _dbContext.Books.Add(bookDomainModel);
            _dbContext.SaveChanges();

            if (addBookRequestDTO.AuthorIds != null && addBookRequestDTO.AuthorIds.Any())
            {
                foreach (var id in addBookRequestDTO.AuthorIds)
                {
                    _dbContext.BookAuthors.Add(new Book_Author
                    {
                        BookId = bookDomainModel.Id,
                        AuthorId = id
                    });
                }
                _dbContext.SaveChanges();
            }

            return bookDomainModel;
        }

        // ================================
        // Update Book
        // ================================
        public Books? UpdateBookById(int id, AddBookRequestDTO bookDTO)
        {
            var bookDomain = _dbContext.Books.FirstOrDefault(n => n.Id == id);
            if (bookDomain == null) return null;

            // update fields
            bookDomain.Title = bookDTO.Title;
            bookDomain.Description = bookDTO.Description;
            bookDomain.IsRead = bookDTO.IsRead;
            bookDomain.DateRead = bookDTO.IsRead ? bookDTO.DateRead : null;
            bookDomain.Rate = bookDTO.IsRead ? bookDTO.Rate : null;
            bookDomain.Genre = bookDTO.Genre;
            bookDomain.CoverUrl = bookDTO.CoverUrl;
            bookDomain.DateAdded = bookDTO.DateAdded;
            bookDomain.PublisherID = bookDTO.PublisherID;
            _dbContext.SaveChanges();

            // update authors
            var oldAuthors = _dbContext.BookAuthors.Where(a => a.BookId == id).ToList();
            if (oldAuthors.Any())
            {
                _dbContext.BookAuthors.RemoveRange(oldAuthors);
                _dbContext.SaveChanges();
            }

            if (bookDTO.AuthorIds != null && bookDTO.AuthorIds.Any())
            {
                foreach (var authorId in bookDTO.AuthorIds)
                {
                    _dbContext.BookAuthors.Add(new Book_Author
                    {
                        BookId = id,
                        AuthorId = authorId
                    });
                }
                _dbContext.SaveChanges();
            }

            return bookDomain;
        }

        // ================================
        // Delete Book
        // ================================
        public Books? DeleteBookById(int id)
        {
            var bookDomain = _dbContext.Books.FirstOrDefault(n => n.Id == id);
            if (bookDomain == null) return null;

            var authors = _dbContext.BookAuthors.Where(a => a.BookId == id).ToList();
            if (authors.Any())
            {
                _dbContext.BookAuthors.RemoveRange(authors);
                _dbContext.SaveChanges();
            }

            _dbContext.Books.Remove(bookDomain);
            _dbContext.SaveChanges();

            return bookDomain;
        }
    }
}
