using Microsoft.AspNetCore.Mvc;
using _2301010045_NguyenNgocVy_Buoi1.Models.DTO;
using _2301010045_NguyenNgocVy_Buoi1.Repository;

namespace _2301010045_NguyenNgocVy_Buoi1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PublishersController : ControllerBase
    {
        private readonly IPublisherRepository _publisherRepository;

        public PublishersController(IPublisherRepository publisherRepository)
        {
            _publisherRepository = publisherRepository;
        }

        // GET: api/publishers
        [HttpGet("get-all-books")]
        public IActionResult GetAllPublishers()
        {
            // Lấy danh sách publishers và sắp xếp theo Id tăng dần
            var publishers = _publisherRepository
                .GetAllPublishers()
                .OrderBy(p => p.Id) // sắp xếp theo Id tăng dần
                .ToList();

            return Ok(publishers);
        }


        // GET: api/publishers/{id}
        [HttpGet("get-publisher-by-id")]
        public IActionResult GetPublisherById(int id)
        {
            var publisher = _publisherRepository.GetPublisherById(id);
            if (publisher == null)
            {
                return NotFound(new { message = $"Publisher with Id = {id} not found" });
            }
            return Ok(publisher);
        }

        // POST: api/publishers
        [HttpPost("add-publishers")]
        public IActionResult AddPublisher([FromBody] AddPublisherRequestDTO dto)
        {
            if (dto == null || string.IsNullOrWhiteSpace(dto.Name))
            {
                return BadRequest(new { message = "Publisher name is required" });
            }

            // Lấy danh sách publishers hiện tại
            var publishers = _publisherRepository.GetAllPublishers();
            int newId = (publishers.Any() ? publishers.Max(p => p.Id) : 0) + 1;

            // Tạo PublisherDTO tạm thời với Id mới
            var publisherDto = new PublisherDTO
            {
                Id = newId,
                Name = dto.Name
            };

            // Thêm vào repository bằng dto gốc
            _publisherRepository.AddPublisher(dto);

            // Trả về PublisherDTO với Id đã tăng
            return CreatedAtAction(nameof(GetPublisherById),
                new { id = publisherDto.Id },
                publisherDto);
        }


        // PUT: api/publishers/{id}
        [HttpPut("update-publishers")]
        public IActionResult UpdatePublisherById(int id, [FromBody] PublisherNoIdDTO dto)
        {
            if (dto == null || string.IsNullOrWhiteSpace(dto.Name))
            {
                return BadRequest(new { message = "Publisher name is required" });
            }

            var updatedPublisher = _publisherRepository.UpdatePublisherById(id, dto);
            if (updatedPublisher == null)
            {
                return NotFound(new { message = $"Publisher with Id = {id} not found" });
            }

            return Ok(updatedPublisher);
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePublisherById(int id)
        {
            var deleted = _publisherRepository.DeletePublisherById(id);
            if (!deleted)
                return NotFound(new { message = $"Publisher with Id = {id} not found" });

            return Ok(new { message = "Publisher deleted successfully", Id = id });
        }

    }
}
