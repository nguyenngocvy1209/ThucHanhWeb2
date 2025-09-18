namespace _2301010045_NguyenNgocVy_Buoi1.Models.DTO
{
    public class PublisherDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    // DTO để update (không cần Id)
    public class PublisherNoIdDTO
    {
        public string Name { get; set; }
    }

  
}
