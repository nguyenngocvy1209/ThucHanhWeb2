using _2301010045_NguyenNgocVy_Buoi1.Models.DTO;
using _2301010045_NguyenNgocVy_Buoi1.Reponsitory;
using Microsoft.AspNetCore.Mvc;

namespace _2301010045_NguyenNgocVy_Buoi1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorRepository _authorRepository;

        public AuthorsController(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        // GET: api/authors/get-all-authors
        [HttpGet("get-all-authors")]
        public IActionResult GetAllAuthors()
        {
            var authors = _authorRepository.GetAllAuthors();
            return Ok(authors);
        }

        // GET: api/authors/get-author-by-id/1
        [HttpGet("get-author-by-id/{id}")]
        public IActionResult GetAuthorById(int id)
        {
            var author = _authorRepository.GetAuthorById(id);
            if (author == null)
            {
                return NotFound(new { Message = $"Không tìm thấy tác giả với ID = {id}" });
            }
            return Ok(author);
        }

        // POST: api/authors/add-author
        [HttpPost("add-author")]
        public IActionResult AddAuthor([FromBody] AddAuthorRequestDTO addAuthorRequestDTO)
        {
            if (addAuthorRequestDTO == null)
            {
                return BadRequest(new { Message = "Dữ liệu không hợp lệ" });
            }

            var author = _authorRepository.AddAuthor(addAuthorRequestDTO);
            return Ok(author);
        }

        // PUT: api/authors/update-author-by-id/1
        [HttpPut("update-author-by-id/{id}")]
        public IActionResult UpdateAuthorById(int id, [FromBody] AuthorNoIdDTO authorNoIdDTO)
        {
            if (authorNoIdDTO == null)
            {
                return BadRequest(new { Message = "Dữ liệu không hợp lệ" });
            }

            var updatedAuthor = _authorRepository.UpdateAuthorById(id, authorNoIdDTO);
            if (updatedAuthor == null)
            {
                return NotFound(new { Message = $"Không tìm thấy tác giả với ID = {id}" });
            }

            return Ok(updatedAuthor);
        }

        // DELETE: api/authors/delete-author-by-id/1
        [HttpDelete("delete-author-by-id/{id}")]
        public IActionResult DeleteAuthorById(int id)
        {
            var deletedAuthor = _authorRepository.DeleteAuthorById(id);
            if (deletedAuthor == null)
            {
                return NotFound(new { Message = $"Không tìm thấy tác giả với ID = {id}" });
            }

            return Ok(new { Message = $"Đã xóa thành công tác giả với ID = {id}" });
        }
    }
}
