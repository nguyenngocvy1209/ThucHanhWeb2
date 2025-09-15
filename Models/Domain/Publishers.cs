using _2301010045_NguyenNgocVy_Buoi1.Models.Domain;
using System.ComponentModel.DataAnnotations;

namespace _2301010045_NguyenNgocVy_Buoi1.Models.Domain
{
    public class Publisher
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        // Navigation Properties – One publisher has many books
        public List<Books> Books { get; set; }
    }
}