using _2301010045_NguyenNgocVy_Buoi1.Data;
using _2301010045_NguyenNgocVy_Buoi1.Models.Domain;
using _2301010045_NguyenNgocVy_Buoi1.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace _2301010045_NguyenNgocVy_Buoi1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookAuthorsController : ControllerBase
    {
        private readonly AppDbContext _dbContext;

        public BookAuthorsController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // POST: api/bookauthors/add-book-author
        [HttpPost("add-book-author")]
        public IActionResult AddBookAuthor([FromBody] AddBookAuthorRequestDTO requestDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Kiểm tra BookID có tồn tại
            var bookExists = _dbContext.Books.Any(b => b.Id == requestDTO.BookID);
            if (!bookExists)
            {
                return BadRequest(new { Message = $"BookID {requestDTO.BookID} does not exist" });
            }

            // Kiểm tra AuthorID có tồn tại
            var authorExists = _dbContext.Authors.Any(a => a.Id == requestDTO.AuthorID);
            if (!authorExists)
            {
                return BadRequest(new { Message = $"AuthorID {requestDTO.AuthorID} does not exist" });
            }

            // Thêm bản ghi vào Book_Author
            var bookAuthor = new Book_Author
            {
                BookId = requestDTO.BookID,
                AuthorId = requestDTO.AuthorID
            };

            _dbContext.BookAuthors.Add(bookAuthor);
            _dbContext.SaveChanges();


            return Ok(new { Message = "Book_Author created successfully", Data = bookAuthor });
        }
    }
}
