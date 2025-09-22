using System.ComponentModel.DataAnnotations;

namespace _2301010045_NguyenNgocVy_Buoi1.Models.DTO
{
    public class AddPublisherRequestDTO
    {
        [Required(ErrorMessage = "Publisher name is required")]
        public string Name { get; set; }
    }
}
