using System.ComponentModel.DataAnnotations;

namespace _2301010045_NguyenNgocVy_Buoi1.Models.DTO
{
    public class AddBookRequestDTO
    {
        [Required(ErrorMessage = "Title is required")]
        [MinLength(10, ErrorMessage = "Title must be at least 10 characters long")]
        [RegularExpression(@"^[a-zA-Z0-9\s]+$", ErrorMessage = "Title cannot contain special characters")]
        public string? Title { get; set; }

        //[Required(ErrorMessage = "Description cannot be null")]
        public string? Description { get; set; }

        public bool IsRead { get; set; }
        public DateTime? DateRead { get; set; }

        //[Range(0, 5, ErrorMessage = "Rate cannot be less than 0 and more than 5")]
        public int? Rate { get; set; }

        public string? Genre { get; set; }
        public string? CoverUrl { get; set; }
        public DateTime DateAdded { get; set; }

        // Navigation Properties
        public int PublisherID { get; set; }
        public List<int> AuthorIds { get; set; }
    }
}
