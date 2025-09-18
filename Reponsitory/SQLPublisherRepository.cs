using _2301010045_NguyenNgocVy_Buoi1.Data;
using _2301010045_NguyenNgocVy_Buoi1.Models.Domain;
using _2301010045_NguyenNgocVy_Buoi1.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace _2301010045_NguyenNgocVy_Buoi1.Repository
{
    public class SQLPublisherRepository : IPublisherRepository
    {
        private readonly AppDbContext _dbContext;

        public SQLPublisherRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<PublisherDTO> GetAllPublishers()
        {
            return _dbContext.Publishers
                .OrderBy(p => p.Id) // sắp xếp theo Id tăng dần
                .Select(p => new PublisherDTO
                {
                    Id = p.Id,
                    Name = p.Name
                }).ToList();
        }


        public PublisherDTO? GetPublisherById(int id)
        {
            var publisher = _dbContext.Publishers.FirstOrDefault(p => p.Id == id);
            if (publisher == null) return null;

            return new PublisherDTO
            {
                Id = publisher.Id,
                Name = publisher.Name
            };
        }

        public PublisherDTO AddPublisher(AddPublisherRequestDTO dto)
        {
            var publisher = new Publisher
            {
                Name = dto.Name
            };

            _dbContext.Publishers.Add(publisher);
            _dbContext.SaveChanges();

            return new PublisherDTO
            {
                Id = publisher.Id,
                Name = publisher.Name
            };
        }

        public PublisherDTO? UpdatePublisherById(int id, PublisherNoIdDTO dto)
        {
            var publisher = _dbContext.Publishers.FirstOrDefault(p => p.Id == id);
            if (publisher == null) return null;

            publisher.Name = dto.Name;
            _dbContext.SaveChanges();

            return new PublisherDTO
            {
                Id = publisher.Id,
                Name = publisher.Name
            };
        }

        public bool DeletePublisherById(int id)
        {
            var publisher = _dbContext.Publishers.FirstOrDefault(p => p.Id == id);
            if (publisher == null) return false;

            _dbContext.Publishers.Remove(publisher);
            _dbContext.SaveChanges();

            return true;
        }
    }
}
