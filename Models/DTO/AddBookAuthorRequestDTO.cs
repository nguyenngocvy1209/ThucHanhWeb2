using System.ComponentModel.DataAnnotations;

namespace _2301010045_NguyenNgocVy_Buoi1.Models.DTO
{
    public class AddBookAuthorRequestDTO
    {
        [Required(ErrorMessage = "BookID is required")]
        public int BookID { get; set; }

        [Required(ErrorMessage = "AuthorID is required")]
        public int AuthorID { get; set; }
    }
}
