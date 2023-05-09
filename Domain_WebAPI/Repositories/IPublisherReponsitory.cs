using Domain_WebAPI.Model.Domain;
using Domain_WebAPI.Model.DTO;

namespace Domain_WebAPI.Repositories
{
    public interface IPublisherReponsitory
    {
        List<PublisherDTO> GetAllPublisher(string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool isAcending = true, int pageNumber = 1, int pagesize = 1000);
        PublisherNoIdDTO GetPublisherById(int id);

        AddPublisherRequestDTO AddPublisher(AddPublisherRequestDTO addPublisherRequestDTO);
        PublisherNoIdDTO UpdatePublisherById(int id, PublisherNoIdDTO updatePublisherRequestDTO);

        Publisher? DeletePublisherById(int id);


    }
}
