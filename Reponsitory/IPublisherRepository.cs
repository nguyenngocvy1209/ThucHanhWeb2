using _2301010045_NguyenNgocVy_Buoi1.Models.DTO;

namespace _2301010045_NguyenNgocVy_Buoi1.Repository
{
    public interface IPublisherRepository
    {
        List<PublisherDTO> GetAllPublishers();
        PublisherDTO? GetPublisherById(int id);
        PublisherDTO AddPublisher(AddPublisherRequestDTO dto);
        PublisherDTO? UpdatePublisherById(int id, PublisherNoIdDTO dto);
        bool DeletePublisherById(int id); // <-- chỉ cần Id, trả về true/false
    }
}
