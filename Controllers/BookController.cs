using _2301010045_NguyenNgocVy_Buoi1.CustomActionFilter;
using _2301010045_NguyenNgocVy_Buoi1.Models.Domain;
using _2301010045_NguyenNgocVy_Buoi1.Models.DTO;
using _2301010045_NguyenNgocVy_Buoi1.Reponsitory;
using Microsoft.AspNetCore.Mvc;

namespace _2301010045_NguyenNgocVy_Buoi1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;

        public BooksController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        // ================================
        // GET ALL with Filter, Sort, Paging
        // ================================
        [HttpGet("get-all-books")]
        public IActionResult GetAll(
            [FromQuery] string? filterOn,
            [FromQuery] string? filterQuery,
            [FromQuery] string? sortBy,
            [FromQuery] bool isAscending = true,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 100)   // mặc định lấy 100 bản ghi
        {
            var allBooks = _bookRepository.GetAllBooks(filterOn, filterQuery, sortBy, isAscending, pageNumber, pageSize);
            return Ok(allBooks);
        }

        // ================================
        // GET BY ID
        // ================================
        [HttpGet("get-book-by-id/{id}")]
        public IActionResult GetBookById([FromRoute] int id)
        {
            var bookWithIdDTO = _bookRepository.GetBookById(id);
            if (bookWithIdDTO == null)
                return NotFound(new { Message = $"Book with Id {id} not found." });

            return Ok(bookWithIdDTO);
        }

        // ================================
        // ADD
        // ================================
        [HttpPost("add-book")]
        [ValidateModel]
        public IActionResult AddBook([FromBody] AddBookRequestDTO addBookRequestDTO)
        {
            if (!ValidateAddBook(addBookRequestDTO))
                return BadRequest(ModelState);

            var bookAdd = _bookRepository.AddBook(addBookRequestDTO);
            return Ok(new { Message = "Book added successfully!", Data = bookAdd });
        }

        // ================================
        // UPDATE
        // ================================
        [HttpPut("update-book-by-id/{id}")]
        public IActionResult UpdateBookById(int id, [FromBody] AddBookRequestDTO bookDTO)
        {
            var updateBook = _bookRepository.UpdateBookById(id, bookDTO);
            if (updateBook == null)
                return NotFound(new { Message = $"Book with Id {id} not found." });

            return Ok(new { Message = "Book updated successfully!", Data = updateBook });
        }

        // ================================
        // DELETE
        // ================================
        [HttpDelete("delete-book-by-id/{id}")]
        public IActionResult DeleteBookById(int id)
        {
            var deleteBook = _bookRepository.DeleteBookById(id);
            if (deleteBook == null)
                return NotFound(new { Message = $"Book with Id {id} not found." });

            return Ok(new { Message = "Book deleted successfully!", Data = deleteBook });
        }

        // ================================
        // Validation Method
        // ================================
        private bool ValidateAddBook(AddBookRequestDTO addBookRequestDTO)
        {
            if (addBookRequestDTO == null)
            {
                ModelState.AddModelError(nameof(addBookRequestDTO), "Please provide book data");
                return false;
            }

            if (string.IsNullOrEmpty(addBookRequestDTO.Description))
            {
                ModelState.AddModelError(nameof(addBookRequestDTO.Description),
                    "Description cannot be null or empty");
            }

            if (addBookRequestDTO.Rate < 0 || addBookRequestDTO.Rate > 5)
            {
                ModelState.AddModelError(nameof(addBookRequestDTO.Rate),
                    "Rate must be between 0 and 5");
            }

            return ModelState.ErrorCount == 0;
        }
    }
}
